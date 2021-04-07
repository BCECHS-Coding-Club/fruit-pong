using System.Linq;
using UnityEngine;

namespace com.Gale.Powerups
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class OrangePowerup : MonoBehaviour, IPowerup
    {
        public float speed = 0.5f;
        public float maxSpeed = 5f;
        public float gravity = 9.8f;
        public float maxHitAngleDeg = 85f;

        public uint paddleHitsUntilDestroyed = 5;
        private uint _paddleHits = 0;

        private Rigidbody2D _rigidbody2D;
        
        public Vector2? CalculateBallVelocity(Rigidbody2D rb)
        {
            var velocity = rb.velocity + Vector2.down * gravity;
            velocity.y = Mathf.Clamp(velocity.y, -maxSpeed, maxSpeed);
            return velocity;
        }

        public void OnCollectPowerup(Ball ball)
        {
            _rigidbody2D = ball.GetComponent<Rigidbody2D>();
            
            Destroy(gameObject);
        }

        public Vector2? OnBallCollision(BallCollisionDetails details)
        {
            if (details.GameObject.CompareTag("Paddle"))
            {
                _paddleHits++;
                if (paddleHitsUntilDestroyed <= _paddleHits)
                {
                    details.Ball.DestroyPowerup();
                    return null;
                }
                
                var position = details.Ball.transform.position;
                var otherPosition = details.GameObject.transform.position;
                
                // TODO change all this to clamp the angle instead of being a difference in position.
                var percentFromPaddleCenter = 
                    Mathf.Clamp(2 * (position.y - otherPosition.y) / details.GameObject.transform.localScale.y,
                                        -1.0f, 1.0f);
                
                var reflectAngle = maxHitAngleDeg * Mathf.Deg2Rad * percentFromPaddleCenter;
                var newReflectVector = new Vector2(Mathf.Cos(reflectAngle) * speed  * -Mathf.Sign(_rigidbody2D.velocity.x),
                    Mathf.Sin(reflectAngle) * speed);
                return newReflectVector;
            }
            
            var reflectVector = Vector2.Reflect(_rigidbody2D.velocity,  details.AggregateNormal);
            return reflectVector;
        }

        public bool ShouldDieOnGoal()
        {
            return false;
        }
    }
}
