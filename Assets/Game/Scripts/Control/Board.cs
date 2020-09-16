using Puzzle.Core;
using Puzzle.FileInput;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Puzzle.Control
{
    public class OnGridValueChangedEventArgs : EventArgs
    {
        public int x;
        public int y;
    }

    public class Board : MonoBehaviour
    {
        [SerializeField] int width = 5;
        [SerializeField] int heigth = 5;
        [SerializeField] Node nodePrefab = null;
        [SerializeField] Node[,] nodes;
        [SerializeField] List<Node> route;
        [SerializeField] Color currentColor = Color.red;
        Dictionary<Color, Node[]> colorRoute;

        [SerializeField] UnityEvent OnGameOver = null;

        public void Start()
        {
            CreateGrid(GameManager.manager.level);
            route = new List<Node>();
            colorRoute = new Dictionary<Color, Node[]>();
        }

        void OnNodeClick(OnGridValueChangedEventArgs args, Node node)
        {
            if (route.Count == 0 && node.isStartPoint)
            {
                currentColor = node.GetColor();
                route.Add(node);                
            }
            else if (!node.isStartPoint)
            {
                if (node.GetColor() != Color.white && route.Count == 0 && colorRoute.ContainsKey(node.GetColor()))
                {
                    foreach (Node n in colorRoute[node.GetColor()])
                    {
                        n.ResetColor();
                    }
                }
                foreach (Node n in route)
                {
                    n.ResetColor();
                }
                route.Clear();
            }
            else
            {
                if (!colorRoute.ContainsKey(currentColor))
                    colorRoute.Add(currentColor, route.ToArray());
                route.Clear();

                if (IsGameOver())
                {
                    OnGameOver.Invoke();
                    gameObject.SetActive(false);
                }
            }
        }

        void OnNodeHover(OnGridValueChangedEventArgs args, Node node)
        {
            if (route.Count != 0)
            {
                int index = route.FindIndex((Node n) => n == node);
                if (index != -1)
                {
                    for(int i = route.Count - 1; i >= index; i--)
                    {
                        route[i].ResetColor();
                        route.RemoveAt(i);
                    }
                }
                
                if (node.GetColor() == Color.white || node.GetColor() == currentColor)
                {
                    route.Add(node);
                    node.SetColor(currentColor);
                }
                else
                {
                    if (colorRoute.ContainsKey(node.GetColor()))
                    {
                        Node[] cr = colorRoute[node.GetColor()];
                        colorRoute.Remove(node.GetColor());
                        foreach (Node n in cr)
                        {
                            n.ResetColor();
                        }
                    }                    
                    else if (node.isStartPoint)
                    {
                        foreach (Node n in route)
                        {
                            n.ResetColor();
                        }
                        route.Clear();
                        return;
                    }

                    route.Add(node);
                    node.SetColor(currentColor);
                }
            }
        }

        bool IsGameOver()
        {
            foreach(Node n in nodes)
            {
                if (n.GetColor() == Color.white)
                    return false;
            }

            return true;
        }

        void CreateGrid(int level)
        {
            Mappings[] mapping = LevelLoader.ReadFile(level);

            nodes = new Node[5, 5];
            for(int x = 0; x < width; x++)
            {
                for (int y = 0; y < heigth; y++)
                {
                    Node node = Instantiate(nodePrefab);
                    node.transform.SetParent(transform);
                    RectTransform nodeTransform = node.GetComponent<RectTransform>();
                    
                    node.SetXY(x, y);

                    node.OnClick += (OnGridValueChangedEventArgs args) => OnNodeClick(args, node);
                    node.OnHover += (OnGridValueChangedEventArgs args) => OnNodeHover(args, node);

                    foreach(Mappings map in mapping)
                    {
                        if ((map.startPoint.x == x && map.startPoint.y == y)||(map.endPoint.x == x && map.endPoint.y == y))
                        {
                            node.isStartPoint = true;
                            node.startColor = map.color;
                            node.ResetColor();
                        }
                    }

                    nodes[x, y] = node;
                }
            }
        }
    }
}
