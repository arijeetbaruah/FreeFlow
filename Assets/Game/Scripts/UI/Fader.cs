using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzle.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Fader : MonoBehaviour
    {
        CanvasGroup group;
        public static Fader fader;

        void Start()
        {
            if (fader == null)
            {
                fader = this;
                DontDestroyOnLoad(fader);
            }
            else
            {
                Destroy(gameObject);
            }

            group = GetComponent<CanvasGroup>();
        }

        public IEnumerator FadeIn()
        {
            while (group.alpha != 1)
            {
                yield return null;
                group.alpha+=0.01f;
            }
        }

        public IEnumerator FadeOut()
        {
            while (group.alpha != 0)
            {
                yield return null;
                group.alpha -= 0.01f;
            }
        }
    }
}
