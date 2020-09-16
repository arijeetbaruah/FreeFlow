using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle.UI
{
    public class GoToButton : MonoBehaviour
    {
        public void GotoLevel(int level)
        {
            FindObjectOfType<MenuController>().StartGameAtLevel(level);
        }
    }
}
