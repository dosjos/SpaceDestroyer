using System.Collections.Generic;

namespace SpaceDestroyer.GameData
{
    public class Level
    {
        public int LevelNumber { get; set; }
        public List<EnemyData> Enemies { get; set; }
        public List<InfoText> InfoTexts { get; set; }
    }
}