using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

namespace com.Gale {

    public interface IPowerup
    {
        Vector2 CalculateBallVelocity(Ball ball);
        void OnBallHit(Ball ball);
    }

    public class WatermelonPowerup : IPowerup
    {
        private Ball _ball;
        private bool _isTouchingFloor = false;

        public int direction;

        public void OnBallHit(Ball ball)
        {
            _ball = ball;
        }

        public Vector2 CalculateBallVelocity(Ball ball)
        {
            if (_isTouchingFloor)
            {
                return Vector2.right * direction;
            }
            else
            {
                // Calculate based on the ball's velocity
                return Vector2.zero;
            }
        }
    }
}