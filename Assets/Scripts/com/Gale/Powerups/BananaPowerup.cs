using System;
using System.Linq;
using UnityEngine;

namespace com.Gale.Powerups
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class BananaPowerup : MonoBehaviour, IPowerup
    {
        // Used to adjust the movement of the banana.
        [SerializeField] private float sineWaveAmplitudeMultiplier = 1f;
        [SerializeField] private float sineWavePeriodMultiplier = 1f;

        [Space]
        
        // The speed of the powerup.
        [SerializeField] private float speed = 1f;
        
        [Space]
        
        // The max angle the banana can reflect off of a paddle.
        [Range(0f, 90f)]
        [SerializeField] private float degPaddleMaxHitAngle = 45f;

        [Space]
        
        // The maximum amount of times that the banana should hit a paddle before it is removed from active play.
        [SerializeField] private uint maxPaddleHitsBeforeDestroy = 3;
        private uint _paddleHits = 0;
        
        private float _lastHitTime;
        private Vector2 _hitAngle;

        private Rigidbody2D _ballRigidbody;

        public Vector2? CalculateBallVelocity(Rigidbody2D rb)
        {
            var sine = sineWaveAmplitudeMultiplier * Mathf.Sin((Time.fixedTime - _lastHitTime) * sineWavePeriodMultiplier);

            // Facing to the right
            var changeVector = new Vector2(speed, sine);

            // Get the angles of the vectors
            var radSineAngle = Vector2.SignedAngle(Vector2.right, changeVector) * Mathf.Deg2Rad;
            var radHitAngle = Vector2.SignedAngle(Vector2.right, _hitAngle) * Mathf.Deg2Rad;

            // Combine the two so that the ball heads in the right direction.
            var finalAngle = radHitAngle + radSineAngle;
            
            return new Vector2(changeVector.magnitude * Mathf.Cos(finalAngle), changeVector.magnitude * Mathf.Sin(finalAngle));
        }

        public void OnCollectPowerup(Ball ball)
        {
            _ballRigidbody = ball.GetComponent<Rigidbody2D>();
            _lastHitTime = Time.fixedTime;
            _hitAngle = _ballRigidbody.velocity.normalized;

            // TODO: Start any animations.
            
            Destroy(gameObject);
        }

        public Vector2? OnBallCollision(BallCollisionDetails details)
        {
            _lastHitTime = Time.fixedTime;

            var normal = details.AggregateNormal;

            if (details.GameObject.CompareTag("Paddle"))
            {
                _paddleHits++;

                if (_paddleHits >= maxPaddleHitsBeforeDestroy)
                {
                    details.Ball.DestroyPowerup();
                    return null;
                }
                
                var percentDistance = Mathf.Clamp(
                    2 * (details.Ball.transform.position.y - details.GameObject.transform.position.y) / details
                        .GameObject.transform.localScale.y,
                    -1f, 1f
                );

                var angle = percentDistance * degPaddleMaxHitAngle * Mathf.Deg2Rad;
                _hitAngle = new Vector2(Mathf.Cos(angle) * Mathf.Sign(normal.x), Mathf.Sin(angle));

                return null;
            }

            _hitAngle = Vector2.Reflect(_hitAngle, normal).normalized;

            return _hitAngle * _ballRigidbody.velocity.magnitude;
        }

        public bool ShouldDieOnGoal()
        {
            return false;
        }
    }
}
