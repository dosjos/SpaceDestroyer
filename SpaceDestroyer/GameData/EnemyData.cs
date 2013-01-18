using System;

namespace SpaceDestroyer.GameData
{
    public class EnemyData
    {
        public int EnemyNumber { get; set; }
        public int Health { get; set; }
        public int Score { get; set; }
        public int Droprate { get; set; }
        public int SpawnRate { get; set; }
        public Boolean Boss { get; set; }
        public long LastSpawn { get; set; }
        public int CrashDamage { get; set; }
    }
}