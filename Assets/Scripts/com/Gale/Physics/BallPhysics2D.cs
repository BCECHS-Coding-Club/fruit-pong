using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NUnit.Framework;
using UnityEngine;

namespace com.Gale.Physics
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class BallPhysics2D : MonoBehaviour, IPhysicsObject
    {
        // Arbitrary number so that the ball doesn't collide with itself
        private static float skinWidth = 0.05f;

        public float moveSpeed = 0.0f;

        // This is better of only used internally, maybe should be private
        public float direction;

        // Extra rays. 1 ray are always guaranteed to be cast.
        [SerializeField] private int rayCount = 1;

        [SerializeField] private LayerMask collisionLayer;

        private Vector2 _velocity = new Vector2(1f, 1f);

        [SerializeField] CircleCollider2D collider;

        private void Start()
        {
            collider = GetComponent<CircleCollider2D>();
        }

        public void Move(Vector2 translation)
        {
            throw new System.NotImplementedException();
        }

        public void OnPhysicsUpdate()
        {
            throw new System.NotImplementedException();
        }

        private void OnDrawGizmos()
        {
            var rayTotal = 2 * rayCount;
            var position = transform.position;

            var velocityAngle = Vector2.Angle(Vector2.right, _velocity);
           
            Gizmos.color = Color.green;
            Gizmos.DrawLine(position, new Vector3(position.x + Mathf.Acos(_velocity.normalized.x) * collider.radius * 2,
                position.y + Mathf.Asin(_velocity.normalized.y) * collider.radius * 2));
            
            var perpendicularAngle = Vector2.Angle(Vector2.right, _velocity) + Mathf.PI / 2;
            Gizmos.DrawLine(position, new Vector3(position.x + Mathf.Cos(perpendicularAngle) * collider.radius,
                position.y + Mathf.Sin(perpendicularAngle) * collider.radius));
            
            Assert.AreEqual(velocityAngle + Mathf.PI / 2, perpendicularAngle );
            
            for (var i = 0; i <= rayTotal; i++)
            {
                var radDirection = Vector2.Angle(Vector2.up, _velocity);
                var angle =  (Mathf.PI / rayTotal * i) - radDirection;

                
                var startPoint = new Vector2(Mathf.Cos(angle) * collider.radius + position.x,
                    Mathf.Sin(angle) * collider.radius + position.y);

                Gizmos.color = Color.red;
                Gizmos.DrawLine(new Vector3(startPoint.x, startPoint.y, 0), new Vector3(startPoint.x + _velocity.x, startPoint.y +_velocity.y, 0));

            }
        }

        // TODO radDirection should be phased out for Vector2.Direction
        private List<RaycastHit2D> CastRays(float radDirection)
        {
            var rayTotal = 2 * rayCount + 1;
            var position = transform.position;

            var ret = new List<RaycastHit2D>(rayTotal);
            
            for (var i = 1; i <= rayTotal; i++)
            {
                var angle =  Mathf.PI / i;

                var startPoint = new Vector2(Mathf.Cos(angle + radDirection) * collider.radius * skinWidth + position.x,
                    Mathf.Sin(angle - radDirection) * collider.radius * skinWidth + position.y);

                var ray = Physics2D.Raycast(startPoint, _velocity.normalized, _velocity.magnitude, collisionLayer);

                if (ray)
                {
                    ret.Add(ray);
                }
            }

            return ret;
        }
    }
}