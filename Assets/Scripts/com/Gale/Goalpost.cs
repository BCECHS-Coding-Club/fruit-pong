using System;
using UnityEngine;

namespace com.Gale
{
    [RequireComponent(typeof(Collider2D))]
    public class Goalpost : MonoBehaviour
    {
        [SerializeField]
        private Collider2D collider;

        // TODO: Turn into a separate class.
        public uint points = 0;

        private void Start()
        {
            if (collider == null)
            {
                throw new Exception("Goalpost will not work unless assigned a collider.\n" + this);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Ball"))
            {
                points++;

                other.GetComponent<Ball>()?.OnGoal();
            }
        }
    }
}