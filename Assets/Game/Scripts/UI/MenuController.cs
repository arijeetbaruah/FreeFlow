using Puzzle.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Puzzle.UI
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] GameObject mainMenu = null;
        [SerializeField] GameObject gotoLevelMenu = null;

        void Start()
        {
            mainMenu.SetActive(true);
            gotoLevelMenu.SetActive(false);
        }

        IEnumerator LoadLevel(int level)
        {
            GameManager.manager.level = level;
            Fader fader = Fader.fader;
            DontDestroyOnLoad(gameObject);
            yield return fader.FadeIn();

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);

            while (!asyncLoad.isDone)
            {
                print("loading");
                yield return null;
            }
            yield return fader.FadeOut();
            Destroy(gameObject);
        }

        public void StartGame()
        {
            StartCoroutine(LoadLevel(0));
        }

        public void OpenGotoLevelMenu()
        {
            gotoLevelMenu.SetActive(true);
            mainMenu.SetActive(false);
        }

        public void StartGameAtLevel(int level)
        {
            StartCoroutine(LoadLevel(level));
        }
    }
}
