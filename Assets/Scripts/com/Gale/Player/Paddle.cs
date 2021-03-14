using System;
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
    public class Paddle : MonoBehaviour, InputController.IPlayerActions
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
                
                _inputController.Player.SetCallbacks(this);
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
