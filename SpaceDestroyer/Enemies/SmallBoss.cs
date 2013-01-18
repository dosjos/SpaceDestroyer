using System;
using System.Collections.Generic;
using SpaceDestroyer.Player;
using SpaceDestroyer.Weapons;

namespace SpaceDestroyer.Enemies
{
    internal class SmallBoss : Enemy
    {
        private readonly List<Enemy> _enemyList;
        private readonly int limit;
        private List<EnemyWeapons> EnemyWeapons;


        public SmallBoss(int health, int score, 
                         List<EnemyWeapons> EnemyWeapons, Random rand, int crash, List<Enemy> enemyList)
        {
            Healt = health;
            MaxHealt = health;
            Points = score;
            this.TopLimit = Game1.TopLimit;
            this.BottomLimit = Game1.BLimit;
            this.EnemyWeapons = EnemyWeapons;
            Width = 200;
            Height = 200;
            this.rand = rand;
            CrashDamage = crash;
            _enemyList = enemyList;
            X = Game1.SWidth + 10;
            Y = rand.Next(TopLimit + 50, BottomLimit - 100);
            Boss = true;
            limit = (Game1.SWidth/2) + 70;
        }

        public override void Calculate()
        {
            if (_enemyList.Count == 1)
            {
                X -= rand.Next(-2, 5);
                if (X < limit)
                {
                    X = limit;
                }

                Y += rand.Next(-2, 3);
            }
        }
    }
}