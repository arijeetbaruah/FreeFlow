using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle.Core
{
    public class GameManager : MonoBehaviour
    {
        public int level = 0;

        public static GameManager manager = null;

        public void Awake()
        {
            if (manager == null)
            {
                manager = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(this);
            }
        }
    }
}
