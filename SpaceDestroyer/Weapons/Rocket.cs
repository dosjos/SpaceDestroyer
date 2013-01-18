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

        public double Angle { get; set; }


        public Rocket(int power, List<Enemies.Enemy> EnemyList)
        {
            Power = power;
            this.EnemyList = EnemyList;
            X = GameController.Player.X + 30;
            Y = GameController.Player.Y + GameController.Player.Height/2;
            RadiusX = 40;
            RadiusY = 15;
        }

        
        public override void Calculate()
        {
            if (EnemyList.Count > 0)
            {
                Target = EnemyList[0];

                if (Target.Y < Y)
                {
                    Y -= 10;
                }
                if (Target.Y > Y)
                {
                    Y += 10;
                }
                if (Target.X < X)
                {
                    X -= 10;
                }
                if (Target.X > X)
                {
                    X += 10;
                }
            }
            else
            {
                X += 10;
            }
        }
    }
}
