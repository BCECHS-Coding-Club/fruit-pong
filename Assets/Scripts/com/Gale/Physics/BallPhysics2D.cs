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
            //
            //  This is basically a repeat of this.CastRays for debugging.
            //
            var rayTotal = 2 * rayCount;
            var position = transform.position;

            var velocityAngle = Vector2.Angle(Vector2.right, _velocity) * Mathf.Deg2Rad;
           
            Gizmos.color = Color.green;
            Gizmos.DrawLine(position, new Vector3(position.x + Mathf.Cos(velocityAngle) * collider.radius * 2,
                position.y + Mathf.Sin(velocityAngle) * collider.radius * 2));
 
            // TODO switch to Vector2.Perpendicular
            var perpendicularAngle = Vector2.Angle(Vector2.right, _velocity) * Mathf.Deg2Rad + Mathf.PI / 2;
            Gizmos.DrawLine(position, new Vector3(position.x + Mathf.Cos(perpendicularAngle) * collider.radius,
                position.y + Mathf.Sin(perpendicularAngle) * collider.radius));
            
            for (var i = 0; i <= rayTotal; i++)
            {
                var radDirection = Vector2.Angle(Vector2.up, _velocity) * Mathf.Deg2Rad;
                var angle =  (Mathf.PI / rayTotal * i) - radDirection;

                
                var startPoint = new Vector2(Mathf.Cos(angle) * collider.radius + position.x,
                    Mathf.Sin(angle) * collider.radius + position.y);

                Gizmos.color = Color.red;
                Gizmos.DrawLine(new Vector3(startPoint.x, startPoint.y, 0), new Vector3(startPoint.x + _velocity.x, startPoint.y +_velocity.y, 0));

            }
        }

        // TODO radDirection should be phased out for Vector2.Direction
        private float CastRays(float radDirection)
        {
            // Equation for raycast origin:
            // Vector2(cos(rayAngle - velocityAngle), sin(rayAngle - velocityAngle)) * radius + skin
            var rayTotal = 2 * rayCount + 1;
            var position = transform.position;

            var hitDistance = -1.0f;
            
            for (var i = 1; i <= rayTotal; i++)
            {
                var rayAngle =  Mathf.PI / i;

                var startPoint = new Vector2(Mathf.Cos(rayAngle + radDirection) * collider.radius + skinWidth + position.x,
                    Mathf.Sin(rayAngle - radDirection) * collider.radius + skinWidth + position.y);

                var ray = Physics2D.Raycast(startPoint, _velocity.normalized, _velocity.magnitude, collisionLayer);
            }

            return hitDistance;
        }
    }
}