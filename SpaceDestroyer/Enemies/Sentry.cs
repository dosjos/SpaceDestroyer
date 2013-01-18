using System;
using System.Collections.Generic;
using SpaceDestroyer.Player;
using SpaceDestroyer.Weapons;

namespace SpaceDestroyer.Enemies
{
    internal class Sentry : Enemy
    {
        public Sentry(int health, int score, List<EnemyWeapons> bulletList,
                      int dropRate, Random rand, int crash)
        {
            CrashDamage = crash;
            Healt = health;
            MaxHealt = health;
            Points = score;
            this.TopLimit = Game1.TopLimit;
            this.BottomLimit = Game1.BLimit;
            WeaponList = bulletList;
            DropRate = dropRate;
            this.rand = rand;
            Y = rand.Next(TopLimit + 30, BottomLimit - 40);
            X = Game1.SWidth + 20;
            Height = 40;
            Width = 57;
            Type = 2;
        }


        public override void Calculate()
        {
            X -= rand.Next(1, 4);
        }
    }
}