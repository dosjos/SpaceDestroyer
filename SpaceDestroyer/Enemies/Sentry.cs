using System;
using System.Collections.Generic;
using SpaceDestroyer.Controllers;
using SpaceDestroyer.Player;
using SpaceDestroyer.Weapons;

namespace SpaceDestroyer.Enemies
{
    internal class Sentry : Enemy
    {
        public Sentry(int health, int score, List<EnemyWeapons> bulletList,
                      int dropRate, Random rand, int crash, int type)
            : base(health, score, bulletList, dropRate, rand, crash, false, type)
        {
           
            Height = 40;
            Width = 57;
        }


        public override void Calculate()
        {
            if (X > Game1.SWidth/2 - 100)
            {
                X -= rand.Next(4, 9);
            }
            else
            {
                X -= rand.Next(1, 4);
            }
            if (Y < Game1.TopLimit) Y = Game1.TopLimit;
            if (Y > Game1.BLimit + Height) Y = Game1.BLimit + Height;


            if (GameController.Player.Y + GameController.Player.Height > Y && GameController.Player.Y < Y + Height)
            {
                if ((DateTime.Now - LastShoot).TotalMilliseconds > 1500)
                {
                    WeaponList.Add(new EBullet(X, Y + Width/2, 10));
                    LastShoot = DateTime.Now;
                }
            }
        }
    }
}