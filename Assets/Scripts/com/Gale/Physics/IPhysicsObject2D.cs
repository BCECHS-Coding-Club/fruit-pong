using UnityEngine;

namespace com.Gale.Physics
{
    // Used so that the game can be paused more easily.
    public interface IPhysicsObject2D
    {
        void Move(Vector2 translation);

        void OnPhysicsUpdate();
        
        void AddVelocity(Vector2 translation);
    
    }

}