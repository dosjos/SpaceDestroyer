using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SpaceDestroyer.Controllers;
using SpaceDestroyer.Weapons;

namespace SpaceDestroyer.Enemies
{
    class ChargingSentry : Enemy
    {
        private enum Modes
        {
            Flying,
            Atacking
        };
        Modes mode = Modes.Flying;

        public ChargingSentry(int health, int score,
                       List<EnemyWeapons> bulletList, int dropRate, Random rand, int crash, int type)
            : base(health, score, bulletList, dropRate, rand, crash, false, type)
        {
            Height = 40;
            Width = 57;
            pos = new Vector2(X, Y);
        }


        public override void Calculate()
        {

            if (mode == Modes.Flying)
            {
                if (X > Game1.SWidth / 2 - 100)
                {
                    X -= rand.Next(4, 9);
                    pos = new Vector2(X,Y);
                }
                else
                {
                    Speed = 10;
                    mode = Modes.Atacking;
                    TargetX = GameController.Player.X + GameController.Player.Width/2;
                    TargetY = GameController.Player.Y + GameController.Player.Height/2;
                    pos = new Vector2(X,Y);
                    dest = new Vector2(TargetX, TargetY);
                }
                if (Y < Game1.TopLimit) Y = Game1.TopLimit;
                if (Y > Game1.BLimit - Height) Y = Game1.BLimit - Height;


                if (GameController.Player.Y + GameController.Player.Height > Y && GameController.Player.Y < Y + Height)
                {
                    if ((DateTime.Now - LastShoot).TotalMilliseconds > 1500)
                    {
                        WeaponList.Add(new EBullet(X, Y + Width / 2, 10));
                        LastShoot = DateTime.Now;
                    }
                }
            }
            else
            {
                if (Math.Abs(X - TargetX) < 6 && Math.Abs(Y - TargetY) < 6)
                {

                    TargetX = GameController.Player.X + GameController.Player.Width / 2;
                    TargetY = GameController.Player.Y + GameController.Player.Height / 2;

                    dest = new Vector2(TargetX, TargetY);
                }
                
                dir = dest - pos;
                dir.Normalize();

                pos += dir * Speed;
                X = (int)pos.X;
                Y = (int)pos.Y;
                Angle = ((float)Math.Atan2(
                     (double)dir.Y,
                     (double)dir.X));
            }
        }

        public int TargetX { get; set; }

        public int TargetY { get; set; }

        public Vector2 dest;
        public Vector2 dir;
        public Vector2 pos;

        public float Angle { get; set; }
    }
}
