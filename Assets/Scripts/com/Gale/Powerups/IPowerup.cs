using System.Collections.Generic;
using com.Gale.Player;
using UnityEngine;

namespace com.Gale.Powerups
{
    public interface IPowerup
    {
        // Whenever this is called, use this instead of the calculated velocity.
        Vector2 CalculateBallVelocity(Rigidbody2D rb);
        void OnDestroy();
        void OnPaddleHit(Rigidbody2D ballRigidbody2D, Paddle paddle);
 
        // Velocity may need to be changed based on this.
        void OnBallCollision2D(List<ContactPoint2D> contacts);
    }
}