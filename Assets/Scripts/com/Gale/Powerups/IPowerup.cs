using System.Collections.Generic;
using com.Gale.Player;
using UnityEngine;

namespace com.Gale.Powerups
{
    public interface IPowerup
    {
        // Whenever this is called, use this instead of the calculated velocity.
        Vector2 CalculateBallVelocity(Rigidbody2D rb);
        void OnCollectPowerup();
 
        // Velocity may need to be changed based on this.
        Vector2? OnBallCollision(List<ContactPoint2D> contacts, GameObject obj, Ball ball);
        
    }
}