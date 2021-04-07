using System;
using System.Collections;
using UnityEngine;

namespace com.Gale.Powerups
{
    
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class GrapePowerup : MonoBehaviour, IPowerup
    {
        public int ballSplitCount = 5;
        [SerializeField] private GameObject ballObject;
        
        private uint _ballCollideCount = 0;
        // The amount of times the ball should collide before being destroyed.
        public uint maxBallCollisions = 5;

        public float totalAliveTime = 10f;
        public float aliveTimeVariation = 0.5f;

        private static GrapePowerup _primaryBall = null;

        private Collider2D _collider2D;

        private void Start()
        {
            _collider2D = GetComponent<Collider2D>();
        }

        public void SetTimeout(float secs)
        {
            StartCoroutine(KillSelf(secs));
        }

        private void OnDestroy()
        {
            if (_primaryBall == this)
            {
                _primaryBall = null;
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator KillSelf(float time)
        {
            yield return new WaitForSeconds(time);

            if (_primaryBall == this || _primaryBall == null)
            {
                var grapes = FindObjectsOfType<GrapePowerup>();
                if (grapes.Length > 0)
                {
                    _primaryBall = grapes[0];
                    Destroy(gameObject);
                    Destroy(GetComponent<Ball>());
                    yield return null;
                }
                
                Destroy(GetComponent<GrapePowerup>());
                try
                {
                    GetComponent<Ball>().DestroyPowerup();
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
                yield return null;
            } 

            Destroy(GetComponent<Ball>());
            Destroy(gameObject);

            yield return null;
        }

        public Vector2? CalculateBallVelocity(Rigidbody2D rb)
        {
            // Don't change the velocity of the ball.
            return null;
        }

        public void OnCollectPowerup(Ball ball)
        {
            _collider2D.enabled = false;
            enabled = false;
            
            // Split the ball into many smaller balls.
            for (var i = 0; i < ballSplitCount; i++)
            {
                var newBall = Instantiate(ballObject, ball.transform.position, transform.rotation);
                var grapePowerup = newBall.AddComponent<GrapePowerup>();
                newBall.GetComponent<Ball>().Powerup = grapePowerup;
                grapePowerup.SetTimeout(totalAliveTime + aliveTimeVariation * i);
                if (i == 0) 
                    _primaryBall = grapePowerup;
                
                newBall.GetComponent<Ball>()?.ChangeToRandomVelocity();
            }

            Destroy(ball.gameObject);
            Destroy(gameObject);
        }

        public Vector2? OnBallCollision(BallCollisionDetails details)
        {
            return null;
        }

        public bool ShouldDieOnGoal()
        {
            var powerups = FindObjectsOfType<GrapePowerup>();
            if (powerups.Length > 0)
            {
                _primaryBall = powerups[0];
                return true;
            }
            
            return _primaryBall != this;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Paddle"))
            {
                _ballCollideCount++;
            }
            
            if (_ballCollideCount >= maxBallCollisions)
            {
                var ball = GetComponent<Ball>();
                ball.DestroyPowerup();
                if (this != _primaryBall)
                {
                    Destroy(ball.gameObject);
                    Destroy(gameObject);
                } else if (_primaryBall == null)
                {
                    _primaryBall = this;
                }
            }
        }
    }
}
