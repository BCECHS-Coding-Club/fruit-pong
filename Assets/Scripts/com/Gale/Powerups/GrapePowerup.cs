using System;
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

        private Collider2D _collider2D;
        
        private void Start()
        {
            _collider2D = GetComponent<Collider2D>();
        }

        public Vector2? CalculateBallVelocity(Rigidbody2D rb)
        {
            // Don't change the velocity of the ball.
            return null;
        }

        public void OnCollectPowerup()
        {
            _collider2D.enabled = false;
            enabled = false;
            
            // Split the ball into many smaller balls.
            for (var i = 0; i < ballSplitCount; i++)
            {
                var newBall = Instantiate(ballObject);
                newBall.GetComponent<Ball>()?.ChangeToRandomVelocity();
            }

            Destroy(gameObject);
        }

        public Vector2? OnBallCollision(BallCollisionDetails details)
        {
            if (details.GameObject.CompareTag("Paddle"))
            {
                _ballCollideCount++;
            }

            if (_ballCollideCount >= maxBallCollisions)
            {
                details.Ball.DestroyPowerup();
                Destroy(details.Ball.gameObject);
            }

            return null;
        }
    }
}
