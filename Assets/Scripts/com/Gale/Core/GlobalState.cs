using System;
using System.Collections;
using System.Collections.Generic;
using com.Gale.Input;
using com.Gale.Player;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace com.Gale.Core
{
    public class GlobalState : MonoBehaviour
    {
        public InputController InputController { get; private set; }

        [SerializeField] private GameObject defaultBall;
        [SerializeField] private List<GameObject> powerups = new List<GameObject>();
        
        [SerializeField] private float powerupTimeBase = 5f;
        [SerializeField] private float powerupRandomGeneratedAddedTime = 10f;
        [SerializeField] private Bounds powerupPlacementBounds;
        // TODO use this to speed up the powerup generation over time.
        private float _startTime;
        private Coroutine _randomPowerupCoroutine;

        [SerializeField] private ScoreCounter player1Score;
        [SerializeField] private ScoreCounter player2Score;

        private void Awake()
        {
            InputController = new InputController();
        }

        private void Start()
        {
            OnGameStart();
        }

        private void OnEnable()
        {
            InputController.Enable();
        }

        private void OnDisable()
        {
            InputController.Disable();
        }

        private void OnGameStart()
        {
            InstantiateNewBall();
            
            // Start the powerup Coroutine
            _randomPowerupCoroutine = StartCoroutine(RandomlyCreatePowerups());

            _startTime = Time.time;
        }

        private void InstantiateNewBall()
        {
            Instantiate(defaultBall, Vector3.zero, Quaternion.identity);
        }

        private IEnumerator RandomlyCreatePowerups()
        {
            while (true)
            {
                var randomAddedTime = Random.value * powerupRandomGeneratedAddedTime;
                yield return new WaitForSeconds(randomAddedTime + powerupTimeBase);

                var randomPowerup = powerups[Random.Range(0, powerups.Count)];
                var boxSize = powerupPlacementBounds.size / 4;
                var randomPlacement = Random.insideUnitSphere;
                var placement = new Vector3(randomPlacement.x * boxSize.x, randomPlacement.y * boxSize.y, randomPlacement.z * boxSize.z);
                Instantiate(randomPowerup, placement + powerupPlacementBounds.center, Quaternion.identity);
            }
            // ReSharper disable once IteratorNeverReturns
        }

        public void OnGoal(Ball ball, PlayerNumber playerNumber)
        {
            ScoreCounter counter;
            
            switch (playerNumber)
            {
                case PlayerNumber.Player1:
                {
                    counter = player1Score;
                    break;
                }
                case PlayerNumber.Player2:
                {
                    counter = player2Score;
                    break;
                }
                case PlayerNumber.None:
                default:
                    throw new Exception("The goalpost did not have a valid playerNumber assigned.");
            }
            
            counter.score++;
            counter.uiElement.text = counter.score.ToString();
            
            var shouldDie = (ball.Powerup?.ShouldDieOnGoal()).GetValueOrDefault(false);
            if (!shouldDie)
            {
                InstantiateNewBall();
            }
        }

        public void OnPause()
        {
            // Display the Menu
            StopCoroutine(_randomPowerupCoroutine);
            
            // For now, just exit.
            // TODO create a pause menu
            Application.Quit();
        }

        public void OnResume()
        {
            _randomPowerupCoroutine = StartCoroutine(RandomlyCreatePowerups());
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(powerupPlacementBounds.center, powerupPlacementBounds.extents);
        }
    }

    [Serializable]
    public struct ScoreCounter
    {
         public Text uiElement;
         public uint score;
    }
}
