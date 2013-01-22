using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SpaceDestroyer.Weapons
{
    public class EBomb : EnemyWeapons
    {
       
        public EBomb(int x, int y)
        {
            X = x;
            Y = y;

            Random r = new Random();

            destX = r.Next(X - 300, X + 300);
            destY = r.Next(Y - 300, Y + 300);

            if (destX < 0) destX = 0;
            if (destX > Game1.SWidth-20) destX = Game1.SWidth - 20;
            if (destY > Game1.BLimit-20) destY = Game1.BLimit-20;
            if (destY < Game1.TopLimit) destY = Game1.TopLimit;


            t = new Vector2(destX, destY);
            Position = new Vector2(X, Y);
            Speed = 3;
            Power = 40;
            RadiusX = RadiusY = 20;
            destruct = 1000;
        }


        public override void Calculate()
        {
            flash++;
            if (flash > 12)
            {
                flash = 0;
            }
            if (Math.Abs(X - destX) > 2 && Math.Abs(Y - destY) > 2)
            {
                var dir = t - Position;
                dir.Normalize();

                Position += dir*Speed;
                X = (int) Position.X;
                Y = (int) Position.Y;

            }

            destruct--;
        }

        public int destX { get; set; }

        public int destY { get; set; }

        public Vector2 t { get; set; }

        public Vector2 Position { get; set; }

        public int flash { get; set; }

        public int destruct { get; set; }
    }
}
