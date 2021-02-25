using System;
using System.Collections.Generic;
using System.Linq;
using com.Gale.Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace com.Gale
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class Ball : MonoBehaviour
    {
        
        public float speed = 5f;

        

        private Rigidbody2D _rigidbody2D;
        private CircleCollider2D _circleCollider2D;

        // For optimization in OnCollisionEnter2D, so that the list isn't destroyed every collision.
        private List<ContactPoint2D> collisions = new List<ContactPoint2D>();

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _circleCollider2D = GetComponent<CircleCollider2D>();
            
            _rigidbody2D.velocity = Random.insideUnitCircle.normalized * speed;
        }

        private void FixedUpdate()
        {
            var velocity = _rigidbody2D.velocity;
            transform.position += new Vector3(velocity.x, velocity.y) * 0.001f;
            // _rigidbody2D.AddForce(velocity * 0.001f);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            collisions.Clear();
            other.GetContacts(collisions);

            // C# Reduce function
            var aggregateNormal = collisions.Aggregate(Vector2.zero, (acc, norm) => acc + norm.normal ).normalized;
            var reflectVector = Vector2.Reflect(_rigidbody2D.velocity, aggregateNormal);

            var paddle = other.gameObject.GetComponent<Paddle>();
            if (paddle)
            {
                var angleBetweenBallAndPaddle = Vector2.Angle(other.transform.position, transform.position) * Mathf.Deg2Rad;
                
                // TODO change all this to clamp the angle instead of being a difference in position.
                var percentFromPaddleCenter =  (other.transform.position.y - transform.position.y) / other.transform.localScale.y;
                Debug.Log("Percent from center: " + percentFromPaddleCenter);

                var reflectAngle = paddle.maxPaddleHitAngle * Mathf.Deg2Rad * percentFromPaddleCenter;
                var newReflectVector = new Vector2(Mathf.Cos(reflectAngle), Mathf.Sin(reflectAngle));
                
                Debug.Log($"Reflect Angle: {reflectAngle * Mathf.Rad2Deg}\nNew Reflect Vector: {newReflectVector}");
            }
            else
            {
                // Do normal physics
            }
            
            _rigidbody2D.velocity = reflectVector;
        }
    }
}
