using System.Collections.Generic;
using com.Gale.Player;
using UnityEngine;

namespace com.Gale.Powerups
{
    public struct BallCollisionDetails
    {
        public ContactPoint2D[] Contacts;
        public GameObject GameObject;
        public Ball Ball;
        public Vector2 AggregateNormal;
    }
    
    public interface IPowerup
    {
        // Whenever this is called, use this instead of the calculated velocity.
        Vector2? CalculateBallVelocity(Rigidbody2D rb);
        // Called whenever the ball collects the powerup.
        void OnCollectPowerup(Ball ball);
        // Called whenever the ball collides with anything when the powerup is attached to it.
        Vector2? OnBallCollision(BallCollisionDetails details);
        
        // TODO
        // void OnBallReset();
    }
}