using UnityEngine;

namespace Puzzle.FileInput
{
    [System.Serializable]
    public class Mappings
    {
        public SaveableCoords startPoint;
        public SaveableCoords endPoint;
        public Color color;
    }

    [System.Serializable]
    public class SaveableColor
    {
        public int r;
        public int g;
        public int b;
    }

    [System.Serializable]
    public class SaveableCoords
    {
        public int x;
        public int y;
    }

    [System.Serializable]
    public class MappingData
    {
        public SaveableCoords startPoint;
        public SaveableCoords endPoint;
        public SaveableColor color;
    }
}
