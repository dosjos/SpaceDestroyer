using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceDestroyer.GameData
{
    class DamageText : FloatingText
    {
       
        public DamageText(int x, int y, int amount)
        {
            X = x;
            StartX = x;
            Y = y;
            Text = "" + amount;
            StartY = Y;
        }



        public override void Calculate()
        {
            Y--;
            if (Xdir)
            {
                X++;
            }
            else
            {
                X--;
            }

            if (StartX > X + 5)
            {
                Xdir = !Xdir;
            }
            else if (StartX < X - 5)
            {
                Xdir = !Xdir;
            }
        }

        public override void Calculate(Microsoft.Xna.Framework.GameTime gt)
        {
            Calculate();
        }

        public bool Xdir { get; set; }

        public int StartX { get; set; }

        public int StartY { get; set; }
    }
}
