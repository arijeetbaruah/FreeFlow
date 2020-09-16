using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle.UI
{
    public class GameOverHandler : MonoBehaviour
    {
        [SerializeField] GameObject gameOverText = null;
        [SerializeField] GameObject returnButton = null;

        public void Start()
        {
            gameOverText.SetActive(false);
            returnButton.SetActive(true);
        }

        public void GameOver()
        {
            gameOverText.SetActive(true);
            returnButton.SetActive(false);
        }
    }
}
