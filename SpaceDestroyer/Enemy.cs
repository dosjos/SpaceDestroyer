using System;
using SpaceDestroyer.Player;

namespace SpaceDestroyer
{
    internal abstract class EnemyUnit
    {
        public int Healt { get; set; }
        public int MaxHealt { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Boolean Boss { get; set; }
        public PlayerOne p { get; set; }
        public int Speed { get; set; }


        private void Step()
        {
        }
    }
}