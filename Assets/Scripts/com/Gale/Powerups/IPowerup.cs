using UnityEngine;

namespace com.Gale
{
    public interface IPowerup
    {
        // Whenever this is called, use this instead of the calculated velocity.
        Vector2 CalculateBallVelocity(Rigidbody2D rb);
    }
}