using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SpaceDestroyer.Backgrounds;
using SpaceDestroyer.Controllers;
using SpaceDestroyer.GameData;
using SpaceDestroyer.Weapons;

namespace SpaceDestroyer.Enemies
{
    class Destroyer : Enemy
    {
        private enum Mode
        {
            Flying,
            Stopped
        };

        private Mode action;

        private int ShootSpeed = 2000;
        public Destroyer(int health, int score,
                       List<EnemyWeapons> bulletList, int dropRate, Random rand, int crash, int type)
            : base(health, score, bulletList, dropRate, rand, crash, false, type)
        {
            Width = 300;
            Height = 84;
            action = Mode.Flying;
            LastShoot = DateTime.Now;
        }
        public override void Calculate()
        {
            if (action == Mode.Flying)
            {
                X -= 1;
                if (X < Game1.SWidth/2)
                {
                    action = Mode.Stopped;
                    ShootSpeed = 1000;
                }
            }
            else
            {
                
            }
            if ((DateTime.Now - LastShoot).TotalMilliseconds > ShootSpeed)
            {
                WeaponList.Add(new ERocket(X + Width/2, Y + Height/2));
                LastShoot = DateTime.Now;
            }

            if (GameController.Player.X + GameController.Player.Width < X && GameController.Player.Y < Y+ Height/2 && GameController.Player.Y + GameController.Player.Height > Y + Height/2)
            {
                WeaponList.Add(new ELaser(GameController.Player.X + GameController.Player.Width - 10, Y + Height / 2, X - (GameController.Player.X + GameController.Player.Width)+ 15 , 1));
               // GameController.Player.Healt -= 1;
                //FloatingTexts.Add(new DamageText(tmp.X + tmp.Width / 2, tmp.Y, 3));
                //var animatedExplosion = new AnimatedExplosion(Vector2.Zero, 0.0f, 0.5f, 1.0f, tmp.X, Player.Y + (Player.Height / 2) - 7, 15, 15, 0);
                //var res = spriteController.AddExplosion(animatedExplosion, 10);
                //Explosions.Add(res);
            }

        }
    }
}
