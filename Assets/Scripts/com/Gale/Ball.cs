using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using com.Gale.Player;
using com.Gale.Powerups;
using UnityEngine;
using Random = UnityEngine.Random;

namespace com.Gale
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class Ball : MonoBehaviour
    {
        
        public float speed = 5f;

        public IPowerup Powerup { get;  set; }

        private Rigidbody2D _rigidbody2D;
        private CircleCollider2D _circleCollider2D;

        private List<ContactPoint2D> _collisions = new List<ContactPoint2D>();

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _circleCollider2D = GetComponent<CircleCollider2D>();
            
            _rigidbody2D.velocity = Random.insideUnitCircle.normalized * speed;
        }

        public void ChangeToRandomVelocity()
        {
            // Null check is necessary for when the ball is instantiated by powerups.
            if (!_rigidbody2D)
            {
                _rigidbody2D = GetComponent<Rigidbody2D>();
            }
            _rigidbody2D.velocity = Random.insideUnitCircle.normalized * speed;
        }

        public void OnGoal()
        {
            transform.position = Vector3.zero;
            _rigidbody2D.velocity = Random.insideUnitCircle.normalized * speed;
            Powerup = null;
        }

        public void DestroyPowerup()
        {
            Powerup = null;
        }

        private void FixedUpdate()
        {
            // var velocity = CalculateVelocity();
            
            // transform.position += new Vector3(velocity.x, velocity.y) * 0.001f;
            _rigidbody2D.velocity = CalculateVelocity();
            // _rigidbody2D.AddForce(velocity * 0.001f);
        }

        private Vector2 CalculateVelocity()
        {
            // TODO: Don't set rigidbody velocity and figure out if the velocity needs to be mutated here.
            return (Powerup?.CalculateBallVelocity(_rigidbody2D)).GetValueOrDefault(_rigidbody2D.velocity);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Powerup"))
            {
                var collidedPowerup = other.gameObject.GetComponent<IPowerup>();
                Powerup = collidedPowerup;

                Powerup.OnCollectPowerup();
                
                Debug.Log("Collided with a powerup!\n" + Powerup);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            // Maybe make this an array?
            // For some reason clearing the list doesn't actually do anything.
            // _collisions.Clear();
            var contactCount = other.GetContacts(_collisions);

            // TODO: Put all the relevant information inside of a struct so there isn't unnecessary copying.
            var vector = Powerup?.OnBallCollision(new BallCollisionDetails
            {
                Ball = this,
                Contacts = _collisions,
                GameObject = other.gameObject
            });
            var vectorVal = vector.GetValueOrDefault(Vector2.zero);

            // Note: I initially used LinQ but it didn't work because GetContacts always returns 128 contacts.
            var aggregateNormal = Vector2.zero;
            for (var i = 0; i < contactCount; i++)
            {
                aggregateNormal += _collisions[i].normal / contactCount;
            }
            // aggregateNormal = aggregateNormal.normalized
            
            var reflectVector = Vector2.Reflect(_rigidbody2D.velocity, aggregateNormal);
            
            if (vectorVal != Vector2.zero)
            {
                // The VERY obvious bug in this is that the powerup may not handle collisions at all.
                _rigidbody2D.velocity = vectorVal;
                return;
            }
            
            if (other.gameObject.CompareTag("Paddle"))
            {
                var paddle = other.gameObject.GetComponent<Paddle>();
                var position = transform.position;
                var otherPosition = other.transform.position;

                // var angleBetweenBallAndPaddle = Vector2.Angle(otherPosition, position) * Mathf.Deg2Rad;

                // TODO change all this to clamp the angle instead of being a difference in position.
                var percentFromPaddleCenter =
                    Mathf.Clamp(2 * (position.y - otherPosition.y) / other.transform.localScale.y,
                        -1.0f, 1.0f);
                Debug.Log("Percent from center: " + percentFromPaddleCenter);

                var reflectAngle = paddle.maxPaddleHitAngle * Mathf.Deg2Rad * percentFromPaddleCenter;
                var newReflectVector = new Vector2(Mathf.Cos(reflectAngle) * reflectVector.magnitude * Mathf.Sign(aggregateNormal.x),
                    Mathf.Sin(reflectAngle) * reflectVector.magnitude);

                Debug.Log($"Reflect Angle: {reflectAngle * Mathf.Rad2Deg}\nNew Reflect Vector: {newReflectVector}");

                _rigidbody2D.velocity = newReflectVector;
                return;
            } 
            // Do normal physics
            _rigidbody2D.velocity = reflectVector;
        }

        // Just used for when the ball gets stuck in a wall.
        private void OnCollisionStay2D(Collision2D other)
        {
            var distance = _circleCollider2D.Distance(other.collider);
            if (!distance.isOverlapped) return;
            
            var translation = distance.distance * distance.normal;
            transform.position += new Vector3(translation.x, translation.y);
        }
    }
}
