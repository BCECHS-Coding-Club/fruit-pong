using System;
using com.Gale.Player;
using UnityEngine;

namespace com.Gale.Powerups {
    [RequireComponent(typeof(Collider2D))]
    public class WatermelonPowerup : MonoBehaviour, IPowerup
    {
        private Ball _ball;
        private bool _isTouchingFloor = false;

        public int direction;
        
        // START: IPowerup interface functions
        public Vector2 CalculateBallVelocity(Rigidbody2D rb)
        {
            return rb.velocity * new Vector2(1, 1.001f);
            
            if (_isTouchingFloor)
            {
                return Vector2.right * direction;
            }

            // Calculate based on the ball's velocity
            return Vector2.zero;
        }

        public void OnDestroy()
        {
            // TODO: Start any animations.
            Destroy(gameObject);
            
            // WatermelonPowerup still stays alive because ball holds the reference.
        }

        public void OnPaddleHit(Rigidbody2D rb, Paddle paddle)
        {
            // TODO: Stop the powerup entirely.
            // TODO: Play any hit animations.
            throw new NotImplementedException();
        }

        // END: IPowerup interface functions
    }
}