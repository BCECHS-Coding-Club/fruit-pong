﻿using System;
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
        private float _currInput = 0f;

        private void OnEnable()
        {
            if (_inputController == null)
            {
                _inputController = new InputController();
                
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
            
            _inputController.Enable();
        }

        private void OnDisable()
        {
            _inputController?.Disable();
        }

        private void FixedUpdate()
        {
            transform.position += new Vector3(0, _currInput * speed);
        }


        /*
        private void OnCollisionEnter2D(Collision2D other)
        {
            // Collide with objects
            throw new NotImplementedException();
        }
        */
        public void OnVerticalMovement(InputAction.CallbackContext context)
        {
            _currInput = context.ReadValue<float>();
        }
    }
}
