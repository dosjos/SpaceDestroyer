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
                         List<EnemyWeapons> EnemyWeapons, Random rand, int crash, List<Enemy> enemyList, int type)
            : base(health, score, EnemyWeapons, 100, rand, crash, true, type)
        {
            Width = 200;
            Height = 200;
            _enemyList = enemyList;
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
            if(Y < Game1.TopLimit) Y = Game1.TopLimit;
            if (Y > Game1.BLimit - Height) Y = Game1.BLimit - Height;

        }
    }
}