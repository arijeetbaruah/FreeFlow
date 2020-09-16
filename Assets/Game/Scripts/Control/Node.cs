using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Puzzle.Control
{
    [RequireComponent(typeof(Image))]
    public class Node : MonoBehaviour, IPointerEnterHandler
    {
        public delegate void OnMouseAction(OnGridValueChangedEventArgs args);
        public event OnMouseAction OnHover;
        public event OnMouseAction OnClick;
        public Color startColor = Color.white;
        Color previousColor;

        int x, y;
        public bool isStartPoint = false;

        private void Awake()
        {
            SetColor(startColor);
            previousColor = startColor;
        }

        public void SetXY(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void GetXY(out int x, out int y)
        {
            x = this.x;
            y = this.y;
        }

        public void SetColor(Color color)
        {
            Image img = GetComponent<Image>();
            previousColor = img.color;
            img.color = color;
        }

        public Color GetColor()
        {
            return GetComponent<Image>().color;
        }

        public void ResetColor()
        {
            Image img = GetComponent<Image>();
            previousColor = startColor;
            img.color = previousColor;
        }

        public void OnMouseClick()
        {
            OnClick(new OnGridValueChangedEventArgs { x = x, y = y });
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnHover(new OnGridValueChangedEventArgs { x = x, y = y });
        }
    }
}
