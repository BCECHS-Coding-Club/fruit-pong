using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace com.Gale
{
    public class StartScreen : MonoBehaviour
    {
        public void OnClickQuitButton()
        {
            Application.Quit();
        }

        public void OnClickStartGameButton()
        {
            SceneManager.LoadScene("Game");
        }
    }
}
