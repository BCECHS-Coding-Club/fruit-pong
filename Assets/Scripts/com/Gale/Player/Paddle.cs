using System;
using UnityEngine;
using com.Gale.Physics;

namespace com.Gale.Player
{
    public class Paddle : MonoBehaviour
    {
        // Used for whenever the ball hits an area of the paddle that is not at the center,
        // So that it can be reflected away at an angle.
        [Header("Paddle Influence on Ball")]
        [Range(0f, 90f)]
        [Tooltip("The angle that the ball will be reflected off of.")]
        public float maxPaddleHitAngle = 0f;
        
        private void Update()
        {
            var inp = Input.GetAxisRaw("Vertical");
            transform.position += new Vector3(0, inp * Time.deltaTime * 5, 0);
        }

        /*
        private void OnCollisionEnter2D(Collision2D other)
        {
            // Collide with objects
            throw new NotImplementedException();
        }
        private void FixedUpdate()
        {
            // Move the paddle
            throw new NotImplementedException();
        }
        */
    }
}
