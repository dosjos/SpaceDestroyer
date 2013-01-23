using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SpaceDestroyer.Controllers;
using SpaceDestroyer.Weapons;

namespace SpaceDestroyer.Enemies
{
    class Striker : Enemy
    {
        private int TargetX, TargetY;
        Vector2 dir = new Vector2();
        private Vector2 dir2 = new Vector2();
        private int shotSpeed;

        public Striker(int health, int score,
                       List<EnemyWeapons> bulletList, int dropRate, Random rand, int crash, int type) 
            : base(health, score, bulletList, dropRate, rand, crash, false, type)
        {
            Speed = 7;
            Height = 60;
            Width = 60;
            pos = new Vector2(X, Y);
            TargetX = rand.Next(-100, Game1.SWidth);
            TargetY = rand.Next(Game1.TopLimit, Game1.BLimit + Height);
            dest = new Vector2(TargetX, TargetY);
            shotSpeed = rand.Next(1000, 2000);
        }
        public override void Calculate()
        {
            if (Math.Abs(X - TargetX) < 10 && Math.Abs(Y - TargetY) < 10)
            {
                TargetX = rand.Next(-100, Game1.SWidth);
                TargetY = rand.Next(Game1.TopLimit, Game1.BLimit + Height);
                dest = new Vector2(TargetX, TargetY);
            }

            dir = dest - pos;
            dir.Normalize();

            pos += dir * Speed;
            X = (int)pos.X;
            Y = (int)pos.Y;
            if (Y < Game1.TopLimit) Y = Game1.TopLimit;
            if (Y > Game1.BLimit + Height) Y = Game1.BLimit + Height;
            CalcHeading();
            Shoot();

        }

        private void Shoot()
        {
            if ((DateTime.Now - LastShoot).TotalMilliseconds > shotSpeed)
            {
                WeaponList.Add(new DirectionalBullet(X + Width/2, Y + Height/2, (int)Angle, 15));
                LastShoot = DateTime.Now;
            }
        }

        private void CalcHeading()
        {
            Vector2 t = new Vector2(GameController.Player.X + GameController.Player.Width / 2, GameController.Player.Y + GameController.Player.Height / 2);


            dir2 = t - pos;
            dir2.Normalize();

            Angle = (float)Math.Atan2(
                      (double)dir2.Y,
                      (double)dir2.X);
        }

        public Vector2 dest { get; set; }

        public Vector2 pos { get; set; }

        public float Angle { get; set; }
    }
}
