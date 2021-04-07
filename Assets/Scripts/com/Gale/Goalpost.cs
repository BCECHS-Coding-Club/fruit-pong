using System;
using com.Gale.Core;
using com.Gale.Player;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace com.Gale
{
    [RequireComponent(typeof(Collider2D))]
    public class Goalpost : MonoBehaviour
    {
        [SerializeField]
        private Collider2D collider;


        private GlobalState _globalState;
        [SerializeField] private PlayerNumber playerNumber = PlayerNumber.None;

        private void Start()
        {
            if (collider == null)
            {
                throw new Exception("Goalpost will not work unless assigned a collider.\n" + this);
            }

            _globalState = FindObjectOfType<GlobalState>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Ball"))
            {
                var ball = other.gameObject.GetComponent<Ball>();
                Assert.IsNotNull(ball);
                _globalState.OnGoal(ball, playerNumber);
                Destroy(ball.gameObject);
            }
        }
    }
}
