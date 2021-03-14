using com.Gale.Player;
using UnityEngine;

namespace com.Gale.Powerups
{
    public interface IPowerup
    {
        // Whenever this is called, use this instead of the calculated velocity.
        Vector2 CalculateBallVelocity(Rigidbody2D rb);
        void OnDestroy();
        void OnPaddleHit(Rigidbody2D rb, Paddle paddle);
    }
}