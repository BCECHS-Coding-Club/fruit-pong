using System;
using System.Collections.Generic;
using System.Linq;
using com.Gale.Core;
using com.Gale.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace com.Gale.Player
{
    public enum PlayerNumber
    {
        Player1,
        Player2,
        None
    }

    [RequireComponent(typeof(Collider2D))]
    public class Paddle : MonoBehaviour, InputController.IPlayer1Actions, InputController.IPlayer2Actions
    {
        // Used for whenever the ball hits an area of the paddle that is not at the center,
        // So that it can be reflected away at an angle.
        [Header("Paddle Influence on Ball")]
        [Range(0f, 90f)]
        [Tooltip("The angle that the ball will be reflected off of.")]
        public float maxPaddleHitAngle = 0f;

        [Space] public float speed = 0.5f;

        [SerializeField]
        private PlayerNumber playerNumber = PlayerNumber.None;
        
        
        private InputController _inputController;
        private Collider2D _collider;
        private float _currInput = 0f;

        private void Start()
        {
            _collider = GetComponent<Collider2D>();
            _inputController = FindObjectOfType<GlobalState>().InputController;

            switch (playerNumber)
            {
                case PlayerNumber.Player1:
                    _inputController.Player1.SetCallbacks(this);
                    break;
                case PlayerNumber.Player2:
                    _inputController.Player2.SetCallbacks(this);
                    break;
                case PlayerNumber.None:
                default:
                    throw new Exception("Did not assign the paddle a player.");
            }
        }

        private void FixedUpdate()
        {
            transform.position += new Vector3(0, _currInput * speed);
        }

        public void OnVerticalMovement(InputAction.CallbackContext context)
        {
            _currInput = context.ReadValue<float>();
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            FindObjectOfType<GlobalState>().OnPause();
        }



        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Ball")) return;
            var distance = _collider.Distance(other.collider);
            var translation = distance.distance * distance.normal;
            transform.position += new Vector3(0, translation.y);
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            // Make sure we don't move toward other collider.
            if (other.gameObject.CompareTag("Ball")) return;
            var distance = _collider.Distance(other.collider);
            var translation = distance.distance * distance.normal;
            transform.position += new Vector3(0, translation.y);
        }
    }
}
