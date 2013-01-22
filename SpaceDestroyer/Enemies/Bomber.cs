using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SpaceDestroyer.Weapons;

namespace SpaceDestroyer.Enemies
{
    class Bomber : Enemy
    {
        
        public Bomber(int health, int score,
                       List<EnemyWeapons> bulletList, int dropRate, Random rand, int crash)
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
            Y = rand.Next(TopLimit + 30, BottomLimit - Height);
            X = Game1.SWidth + 20;
            Height = 74;
            Width = 150;
            Type = 5;
            pos = new Vector2(X, Y);
            Speed = 7;
            TargetX = rand.Next(-100, Game1.SWidth);
            TargetY = rand.Next(Game1.TopLimit, Game1.BLimit + Height);
            dest = new Vector2(TargetX, TargetY);
            WeaponList = bulletList;
            shotSpeed = rand.Next(1000, 2000);
        }

        
        public override void Calculate()
        {
            X -= 2;
        }

        public int TargetX { get; set; }

        public int TargetY { get; set; }

        public Vector2 dest { get; set; }

        public int shotSpeed { get; set; }

        public Vector2 pos { get; set; }
    }
}
