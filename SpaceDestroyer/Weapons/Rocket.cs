using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SpaceDestroyer.Controllers;
using SpaceDestroyer.Enemies;

namespace SpaceDestroyer.Weapons
{
    class Rocket : PlayerWeapon
    {
        private int p;
        private List<Enemies.Enemy> EnemyList;
        public Enemy Target { get; set; }

        public float Angle { get; set; }
        public Vector2 dir, Position;
  
        public Rocket(int power, List<Enemies.Enemy> EnemyList)
        {
            Power = power;
            this.EnemyList = EnemyList;
            X = GameController.Player.X + GameController.Player.Width-10;
            Y = GameController.Player.Y + GameController.Player.Height/2;
            RadiusX = 40;
            RadiusY = 15;
            Speed = 12;
            Position = new Vector2(X,Y);

            dir = new Vector2(X + 10, Y) - Position;
            dir.Normalize();
        }

        
        public override void Calculate()
        {
            if (EnemyList.Count > 0)
            {
                Target = EnemyList[0];

                Vector2 t = new Vector2(Target.X + Target.Width/2, Target.Y + Target.Height/2);
                

                dir = t - Position;
                dir.Normalize();

                Angle = (float)Math.Atan2(
                          (double)dir.Y,
                          (double)dir.X);

                Position += dir * Speed;
                X = (int)Position.X;
                Y = (int)Position.Y;
            }
            else
            {
                X = (int)Position.X;
                Y = (int)Position.Y;
                Position += dir * Speed;
            }

        }

        public int XDistance { get; set; }

        public int YDistance { get; set; }
    }
}
