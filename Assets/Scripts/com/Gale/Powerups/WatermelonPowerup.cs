﻿using System;
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
            
        [SerializeField]
        private Collider2D collider2D;

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

        public void OnCollectPowerup()
        {
            collider2D.enabled = false;
            enabled = false;
            _hasTouchedFloor = false;
            try
            {
                GetComponent<MeshRenderer>().enabled = false;
            }
            catch
            {
                // ignored
            }

            // TODO: Start any destroyed animations.
            
            Destroy(gameObject);
        }

        public Vector2? OnBallCollision(BallCollisionDetails details)
        {
            var obj = details.GameObject;
            var contacts = details.Contacts;
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

                var direction = Mathf.Sign(contacts.Aggregate(Vector2.zero, (acc, contact) => acc += contact.normal).x);
                
                return new Vector2(Mathf.Cos(paddle.maxPaddleHitAngle * Mathf.Deg2Rad) * ball.speed * direction, Mathf.Sin(paddle.maxPaddleHitAngle * Mathf.Deg2Rad) * ball.speed);
            }

            var contactPoint2Ds = contacts as ContactPoint2D[] ?? contacts.ToArray();
            if (!contactPoint2Ds.Any()) return null;
            foreach (var contact in contactPoint2Ds)
            {
                _hasTouchedFloor = _hasTouchedFloor || contact.normal == Vector2.up;
            }
            
            return null;
        }

        // END: IPowerup interface functions
    }
}