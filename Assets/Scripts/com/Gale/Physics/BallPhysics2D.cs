using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace com.Gale.Physics
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class BallPhysics2D : MonoBehaviour, IPhysicsObject2D
    {
        // Arbitrary number so that the ball doesn't collide with itself
        private static float skinWidth = 0.05f;

        [SerializeField]
        private float _moveSpeed = 0.0f;
        
        public float MoveSpeed
        {
            get => _moveSpeed;
            set
            {
                _moveSpeed = value;
                velocity = velocity.normalized * value;
            }
        }

        // Extra rays. 1 ray are always guaranteed to be cast.
        [SerializeField] private int rayCount = 1;

        [SerializeField] private LayerMask collisionLayer;

        [SerializeField]
        public Vector2 velocity = Vector2.zero;

        [SerializeField] private CircleCollider2D circleCol;

        private void Start()
        {
            circleCol = GetComponent<CircleCollider2D>();
            velocity = GetRandomAngle() * MoveSpeed;
        }

        public void Move(Vector2 translation)
        {
            throw new System.NotImplementedException();
        }

        private void FixedUpdate()
        {
            OnPhysicsUpdate();
        }

        public void OnPhysicsUpdate()
        {
            var angle = Mathf.Deg2Rad * Vector2.Angle(Vector2.right, velocity.normalized);
            var (hitDistance, hitNormal) = CastRays();

            if (hitDistance >= 0.0f)
            {
                // We've hit an object.
                
                // The distance to the object
                var tempDistance = new Vector2(Mathf.Cos(angle) * hitDistance, Mathf.Sin(angle) * hitDistance);
                // The velocity after we've hit the object.
                var tempVelocity = velocity - tempDistance;
                
                var reflectVector = Vector2.Reflect(tempVelocity, hitNormal);
                // Move to the collided object, then by the tempVelocity
                transform.position += new Vector3(tempDistance.x, tempDistance.y) + new Vector3(reflectVector.x, reflectVector.y);

                velocity = reflectVector.normalized * MoveSpeed;
            }
            else
            {
                // We haven't hit anything.
                transform.position += new Vector3(velocity.x, velocity.y);
            }
        }

        public void AddVelocity(Vector2 translation)
        {
            throw new NotImplementedException();
        }

        private static Vector2 GetRandomAngle()
        {
            // Normalizing the Vector might be redundant.
            // TODO clamp this within a certain bound so that the ball doesn't travel straight up or down.
            return new Vector2(Mathf.Cos(Random.Range(-2f * Mathf.PI, 2f * Mathf.PI)),
                Mathf.Sin(Random.Range(-2f * Mathf.PI, 2f * Mathf.PI))).normalized;
        }

        private void OnDrawGizmos()
        {
            //
            //  This is basically a repeat of this.CastRays for debugging.
            //
            var rayTotal = 2 * rayCount;
            var position = transform.position;

            var velocityAngle = Vector2.Angle(Vector2.right, velocity) * Mathf.Deg2Rad;
           
            Gizmos.color = Color.green;
            Gizmos.DrawLine(position, new Vector3(position.x + Mathf.Cos(velocityAngle) * circleCol.radius * 2,
                position.y + Mathf.Sin(velocityAngle) * circleCol.radius * 2));
 
            // TODO switch to Vector2.Perpendicular
            var perpendicularAngle = Vector2.Angle(Vector2.right, velocity) * Mathf.Deg2Rad + Mathf.PI / 2;
            Gizmos.DrawLine(position, new Vector3(position.x + Mathf.Cos(perpendicularAngle) * circleCol.radius,
                position.y + Mathf.Sin(perpendicularAngle) * circleCol.radius));
            
            for (var i = 0; i <= rayTotal; i++)
            {
                var radDirection = Vector2.Angle(Vector2.up, velocity) * Mathf.Deg2Rad;
                var angle =  (Mathf.PI / rayTotal * i) - radDirection;

                var startPoint = new Vector2(Mathf.Cos(angle) * circleCol.radius + position.x,
                    Mathf.Sin(angle) * circleCol.radius + position.y);

                Gizmos.color = Color.red;
                Gizmos.DrawLine(new Vector3(startPoint.x, startPoint.y, 0), new Vector3(startPoint.x + velocity.x, startPoint.y +velocity.y, 0));
            }
        }
        
        // the distance to the object and the normal (if hit)
        private (float, Vector2) CastRays()
        {
            // Equation for raycast origin:
            // Vector2(cos(rayAngle - velocityAngle), sin(rayAngle - velocityAngle)) * radius + skin
            var rayTotal = 2 * rayCount + 1;
            var position = transform.position;
            var radDirection = Vector2.Angle(Vector2.right, velocity) * Mathf.Deg2Rad;

            var hitDistance = -1.0f;
            var hitNormal = Vector2.zero;
            
            for (var i = 1; i <= rayTotal; i++)
            {
                var rayAngle =  Mathf.PI / i;

                var startPoint = new Vector2(Mathf.Cos(rayAngle + radDirection) * circleCol.radius + skinWidth + position.x,
                    Mathf.Sin(rayAngle - radDirection) * circleCol.radius + skinWidth + position.y);

                var ray = Physics2D.Raycast(startPoint, velocity.normalized, velocity.magnitude, collisionLayer);

                if (ray.collider && hitDistance < ray.distance)
                {
                    hitDistance = ray.distance;
                    hitNormal = ray.normal;
                }
            }

            return (hitDistance, hitNormal);
        }
    }
}