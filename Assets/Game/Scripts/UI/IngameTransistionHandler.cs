using Puzzle.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Puzzle.UI
{
    public class IngameTransistionHandler : MonoBehaviour
    {
        IEnumerator LoadLevel()
        {
            yield return LoadRoom(1);
        }

        IEnumerator LoadMain()
        {
            yield return LoadRoom(0);
        }

        IEnumerator LoadRoom(int room)
        {
            Fader fader = Fader.fader;
            DontDestroyOnLoad(gameObject);
            yield return fader.FadeIn();

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(room, LoadSceneMode.Single);

            while (!asyncLoad.isDone)
            {
                print("loading");
                yield return null;
            }
            yield return fader.FadeOut();
            Destroy(gameObject);
        }

        public void ReturnToMain()
        {
            StartCoroutine(LoadMain());
        }

        public void NextLevel()
        {
            GameManager.manager.level++;
            StartCoroutine(LoadLevel());
        }
    }
}
