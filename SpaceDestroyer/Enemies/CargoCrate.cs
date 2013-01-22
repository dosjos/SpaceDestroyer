using System;
using System.Collections.Generic;
using SpaceDestroyer.Player;
using SpaceDestroyer.Weapons;

namespace SpaceDestroyer.Enemies
{
    public class CargoCrate : Enemy
    {
        private readonly Random r = new Random();

        public CargoCrate(int health, int score, 
                          List<EnemyWeapons> bulletList, int dropRate, Random rand, int crash)
            : base(health, score, bulletList, dropRate, rand, crash, false)
        {
           
            Height = 44;
            Width = 44;
            Type = 3;
        }

        public override void Calculate()
        {
            X -= r.Next(-2, 10);
            Y += r.Next(-1, 2);

            if (Y < Game1.TopLimit)
            {
                Y = Game1.TopLimit;
            }
            if (Y + Height > Height + Game1.BLimit)
            {
                Y = Height - Game1.BLimit;
            }
        }
    }
}