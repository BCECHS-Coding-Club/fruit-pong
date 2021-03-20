using System;
using UnityEngine;
using UnityEngine.UI;

namespace com.Gale.Core
{
    public class GameUI : MonoBehaviour
    {
        private Goalpost _player1Score;
        private Goalpost _player2Score;

        [SerializeField] private Text player1Text;
        [SerializeField] private Text player2Text;

        private void Start()
        {
            _player1Score = GameObject.Find("Left Goal").GetComponent<Goalpost>();
            _player2Score = GameObject.Find("Right Goal").GetComponent<Goalpost>();
        }

        private void OnGUI()
        {
            player1Text.text = _player1Score.points.ToString();
            player2Text.text = _player2Score.points.ToString();
        }
    }
}
