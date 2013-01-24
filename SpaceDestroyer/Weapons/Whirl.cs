using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SpaceDestroyer.Controllers;

namespace SpaceDestroyer.Weapons
{
    class Whirl : PlayerWeapon
    {
        private int xi;
        private int yi;
        public int i;
        private Vector2 dir;
        public Vector2 Position;
        public Whirl(int dmg, int xx, int yy, int i)
        {
            Power = dmg;
            this.xi = xx;
            this.yi = yy;
            this.i = i;
            dir = new Vector2(xi,yi);
            dir.Normalize();
            Angle = (float)Math.Atan2(
                         (double)dir.Y,
                         (double)dir.X);

            Position = new Vector2(GameController.Player.X, GameController.Player.Y);
            Speed = 7;
            RadiusX = 10;
            RadiusY = 10;
        }

        public override void Calculate()
        {
            if (i == 0)
            {

                //Vector2 t = new Vector2(X+xi, Y + yi);
                //dir = t - Position;
                //dir.Normalize();
                Position += dir * Speed;
                X = (int)Position.X;
                Y= (int)Position.Y;
            }
            else
            {
                X = GameController.Player.X + GameController.Player.Width/2;
                Y = GameController.Player.Y + GameController.Player.Height/2;
                Position = new Vector2(X,Y);
                

                i--;
            }
        }

        public float Angle { get; set; }
    }
}
