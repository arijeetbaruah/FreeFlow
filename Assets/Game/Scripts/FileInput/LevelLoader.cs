using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Puzzle.FileInput
{
    public class LevelLoader
    {
        const string path = "Assets/Game/Resources/test{0}.json";

        [SerializeField]
        public class LevelData
        {
            public List<MappingData> levels = new List<MappingData>();
        }

        public static Mappings[] ReadFile(int level)
        {
            StreamReader reader = new StreamReader(string.Format(path, level));
            LevelData data = new LevelData();
            data = JsonUtility.FromJson<LevelData>(reader.ReadToEnd());

            Mappings[] mappings = new Mappings[data.levels.Count];
            
            for(int i = 0; i < data.levels.Count; i++)
            {
                mappings[i] = new Mappings();
                mappings[i].startPoint = data.levels[i].startPoint;
                mappings[i].endPoint = data.levels[i].endPoint;
                mappings[i].color = new Color(data.levels[i].color.r, data.levels[i].color.g, data.levels[i].color.b);
            }

            reader.Close();
            return mappings;
        }
    }
}
