using System;
using System.Collections.Generic;
using System.Linq;
using com.Gale.Player;
using UnityEngine;

namespace com.Gale.Powerups {
    [RequireComponent(typeof(Collider2D))]
    public class WatermelonPowerup : MonoBehaviour, IPowerup
    {
        private bool _hasTouchedFloor = false;

        public float speed = 1.05f;
        public float maxSpeed = 2f;
        public float acceleration = 1.05f;

        public Sprite watermelonBallSprite;
            
        [SerializeField]
        private Collider2D collider2D;

        // The amount that we scale the ball on contact.
        [SerializeField] private Vector3 watermelonBallTransform = new Vector3(2.0f, 2.0f, 1.0f);

        private void Start()
        {
            collider2D = GetComponent<Collider2D>();
        }

        // START: IPowerup interface functions
        public Vector2? CalculateBallVelocity(Rigidbody2D rb)
        {
            var clampedSpeed = Mathf.Clamp(rb.velocity.magnitude * speed, 0f, maxSpeed);
            if (_hasTouchedFloor)
                return new Vector2(clampedSpeed * Mathf.Sign(rb.velocity.x), 0);

            return rb.velocity - new Vector2(0, acceleration);
        }

        public void OnCollectPowerup(Ball ball)
        {
            collider2D.enabled = false;
            enabled = false;
            _hasTouchedFloor = false;
            try
            {
                GetComponent<SpriteRenderer>().enabled = false;
                
            }
            catch
            {
                // ignored
            }

            // TODO: Start any destroyed animations
            ball.spriteRenderer.sprite = watermelonBallSprite;
            ball.spriteRenderer.flipX = !(Mathf.Sign(ball.GetComponent<Rigidbody2D>().velocity.x) >= 0);

            ball.transform.localScale = watermelonBallTransform;
            
            // FIXME: The ball's box collider doesn't adjust when the scale is adjusted.

            Destroy(gameObject);
        }

        public Vector2? OnBallCollision(BallCollisionDetails details)
        {
            var obj = details.GameObject;
            var ball = details.Ball;
            if (obj.CompareTag("Paddle") && _hasTouchedFloor)
            {
                // Remove the powerup.
                details.Ball.DestroyPowerup();

                var paddle = obj.GetComponent<Paddle>();
                if (!paddle)
                {
                    throw new Exception("For some reason the object passed into " + name + " did not collide with a paddle.\n" + this);
                }

                var direction = Mathf.Sign(details.AggregateNormal.x);
                
                return new Vector2(Mathf.Cos(paddle.maxPaddleHitAngle * Mathf.Deg2Rad) * ball.speed * direction, Mathf.Sin(paddle.maxPaddleHitAngle * Mathf.Deg2Rad) * ball.speed);
            }


            foreach (var contact in details.Contacts)
            {
                _hasTouchedFloor = _hasTouchedFloor || contact.normal == Vector2.up;
            }
            
            return null;
        }

        public bool ShouldDieOnGoal()
        {
            return false;
        }

        // END: IPowerup interface functions
    }
}