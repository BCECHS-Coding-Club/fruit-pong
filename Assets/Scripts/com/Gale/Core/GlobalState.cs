using System;
using System.Collections;
using System.Collections.Generic;
using com.Gale.Input;
using UnityEngine;
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
            // Create a ball
            Instantiate(defaultBall, Vector3.zero, Quaternion.identity);
            
            // Start the powerup Coroutine
            _randomPowerupCoroutine = StartCoroutine(RandomlyCreatePowerups());

            _startTime = Time.time;
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

        public void OnGoal(Ball ball)
        {
            
        }

        public void OnPause()
        {
            // Display the Menu
            StopCoroutine(_randomPowerupCoroutine);
            
            // For now, just exit.
            // TODO don't do this
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
}
