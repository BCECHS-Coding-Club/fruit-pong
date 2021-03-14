using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using com.Gale.Input;
using com.Gale.Player;
using UnityEngine;

namespace com.Gale {
    [RequireComponent(typeof(Collider2D))]
    public class WatermelonPowerup : MonoBehaviour, IPowerup
    {
        private Ball _ball;
        private bool _isTouchingFloor = false;

        public int direction;
        
        // START: IPowerup interface functions
        public Vector2 CalculateBallVelocity(Rigidbody2D rb)
        {
            return rb.velocity * new Vector2(1, 1.001f);
            
            if (_isTouchingFloor)
            {
                return Vector2.right * direction;
            }

            // Calculate based on the ball's velocity
            return Vector2.zero;
        }
        // END: IPowerup interface functions
    }
}