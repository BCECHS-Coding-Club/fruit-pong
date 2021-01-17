using UnityEngine;

namespace com.Gale.Physics
{
    // Used so that the game can be paused more easily.
    public interface IPhysicsObject
    {
        void Move(Vector2 translation);

        void OnPhysicsUpdate();
    
    }

}