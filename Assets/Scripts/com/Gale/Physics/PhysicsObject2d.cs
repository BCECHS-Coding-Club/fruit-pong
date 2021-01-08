using System;
using System.Collections.Generic;
using UnityEngine;

namespace com.Gale.Physics
{

    // Used so that the game can be paused more easily.
    public interface IPhysicsObject
    {
        void Move(Vector2 translation);

        void OnPhysicsUpdate();
    }

    [RequireComponent(typeof(CircleCollider2D))]
    public class CirclePhysicsObject2D : MonoBehaviour, IPhysicsObject
    {
        public float Speed { get; private set; }
        
        // How many rays are cast on each side of the center
        public uint RayCount { get; private set; }

        private CircleCollider2D _collider;
        
        public void Move(Vector2 translation)
        {
            throw new NotImplementedException();
        }

        public void OnPhysicsUpdate()
        {
            throw new NotImplementedException();
        }

        // Please note, the rays are cast based upon where the origin of the collider is.
        // List should be ordered from left to right.
        private List<RaycastHit2D> CastRays(Vector2 translation)
        {
            var ret = new List<RaycastHit2D> {Capacity = 2 * (int) RayCount + 1};

            var position = _collider.transform.position;
            var origin = new Vector2(position.x, position.y);

            var angle = Vector2.Angle(origin, translation) * Mathf.Deg2Rad;
            
            for (var i = -RayCount; i < RayCount; i++)
            {
               /*
                * NONE OF THIS LOGIC IS RIGHT
                *
                * The ray will always be the direction of the vector with this current code.
                *
                * TODO: Split the diameter into 2*RayCount+1 and get the point from that slice to the edge of the circle,
                * TODO: making sure that the edge of the circle is calculated in the direction of the translation vector.
                */
                
                // Somewhere at the edge of the circle, cast a ray distance of the translation vector
                var normalizedDistanceFromOrigin = i / RayCount;
                
                // Some basic trig to get the position where the ray should be calculated.
                // TODO get the angle between the origin and the rayOrigin, then extend that
                // vector so that it sits on the radius of the circle.
                var rayOrigin = new Vector2( 
                    origin.x + Mathf.Cos(angle) * _collider.radius * normalizedDistanceFromOrigin,
                    origin.y + Mathf.Sin(angle) * _collider.radius * normalizedDistanceFromOrigin);
                
                var hit = Physics2D.Raycast(rayOrigin, translation, translation.magnitude);
                
                if (hit)
                {
                    ret.Add(hit);
                }
            }

            return ret;
        }

        private void OnDrawGizmos()
        {
            throw new NotImplementedException();
        }
    }

    
}