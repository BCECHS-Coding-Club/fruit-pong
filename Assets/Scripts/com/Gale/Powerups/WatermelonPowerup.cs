using System;
using System.Collections.Generic;
using com.Gale.Player;
using UnityEngine;

namespace com.Gale.Powerups {
    [RequireComponent(typeof(Collider2D))]
    public class WatermelonPowerup : MonoBehaviour, IPowerup
    {
        private Ball _ball;
        private bool _isTouchingFloor = false;

        public int direction;
        public float speed = 1.05f;
        public float maxSpeed = 2f;

        [SerializeField]
        private Collider2D collider2D;

        ~WatermelonPowerup()
        {
            Destroy(gameObject);
        }
        
        private void Start()
        {
            collider2D = GetComponent<Collider2D>();
        }

        // START: IPowerup interface functions
        public Vector2 CalculateBallVelocity(Rigidbody2D rb)
        {
            var clampedSpeed = Mathf.Clamp(rb.velocity.magnitude * speed, 0f, maxSpeed);
            if (_isTouchingFloor)
                return new Vector2(clampedSpeed * Mathf.Sign(rb.velocity.x), 0);

            // Calculate based on the ball's velocity
            return rb.velocity.normalized * clampedSpeed;
        }

        public void OnDestroy()
        {
            collider2D.enabled = false;
            enabled = false;
            try
            {
                GetComponent<MeshRenderer>().enabled = false;
            }
            catch
            {
                // ignored
            }

            // TODO: Start any animations.
            // Maybe not destroy it? Ball still holds on to the reference.
            // Destroy(gameObject);

            // WatermelonPowerup still stays alive because ball holds the reference.
        }

        public void OnPaddleHit(Rigidbody2D rb, Paddle paddle)
        {
            // TODO: Stop the powerup entirely.
            // TODO: Play any hit animations.
            throw new NotImplementedException();
        }

        public void OnBallCollision2D(List<ContactPoint2D> contacts)
        {
            if (contacts.Count <= 0) return;
            foreach (var contact in contacts)
            {
                _isTouchingFloor = _isTouchingFloor || contact.normal == Vector2.up;
            }
        }

        // END: IPowerup interface functions
    }
}