using System;
using System.Collections;
using System.Security.Cryptography;
using UnityEditor;
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

        public bool isPrimaryBall = false;

        private Collider2D _collider2D;
        
        private void Start()
        {
            _collider2D = GetComponent<Collider2D>();
        }

        public void SetTimeout(float secs)
        {
            StartCoroutine(KillSelf(secs));
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator KillSelf(float time)
        {
            yield return new WaitForSeconds(time);

            if (isPrimaryBall)
            {
                Destroy(GetComponent<GrapePowerup>());
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
                var grapePowerup =  newBall.AddComponent<GrapePowerup>();
                grapePowerup.SetTimeout(totalAliveTime + aliveTimeVariation * i);
                if (i != 0) 
                    grapePowerup.isPrimaryBall = true;
                
                newBall.GetComponent<Ball>()?.ChangeToRandomVelocity();
            }

            Destroy(ball.gameObject);
            Destroy(gameObject);
        }

        public Vector2? OnBallCollision(BallCollisionDetails details)
        {
            return null;
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
                Destroy(ball.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
