using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SpaceDestroyer.Controllers;

namespace SpaceDestroyer.Weapons
{
    class ERocket : EnemyWeapons
    {

        public Vector2 dir;
        public Vector2 pos;

        public Vector2 Targ;
        public ERocket(int x, int y)
        {
            X = x;
            Y = y;
            RadiusX = 40;
            RadiusY = 15;
            Power = 30;
            TargetX = GameController.Player.X;
            TargetY = GameController.Player.Y;
            pos = new Vector2(X, Y);
            Targ = new Vector2(TargetX, TargetY);
            dir = Targ - pos;
            dir.Normalize();
            Speed = 10;
            Angle = (float)Math.Atan2(
                          (double)dir.Y,
                          (double)dir.X);
        }

        public override void Calculate()
        {
            pos += dir*Speed;
            X = (int)pos.X;
            Y = (int)pos.Y;

        }

        public int TargetX { get; set; }

        public int TargetY { get; set; }

        

        public float Angle { get; set; }
    }
}
