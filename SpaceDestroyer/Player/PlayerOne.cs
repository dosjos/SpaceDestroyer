using System;

namespace SpaceDestroyer.Player
{
    public class PlayerOne
    {
        public int boost = 0;
        public int X { get; set; }
        public int Y { get; set; }
        public int Speed { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Healt { get; set; }
        public int Shield { get; set; }
        public int Booster { get; set; }
        public Boolean ShieldOn { get; set; }

        public PlayerOne()
        {
            Healt = 50;
            Shield = 100;
            Booster = 100;
            Height = 60;
            Width = 100;
            X = 30;
            Y = (Game1.SHeight/2) - Height/2;
        }

        internal void GoLeft(double speed = 1.0)
        {
            X = X - (int)(10 * speed);

            if (X < 15)
            {
                X = 15;
            }
        }

        internal void GoRight(double speed = 1.0)
        {
            X = X + (int)(8 * speed) + boost;
            if (boost != 0)
            {
                Booster--;
            }
            if (X > Game1.SWidth - 5 - Width)
            {
                X = Game1.SWidth - 5 - Width;
            }
        }

        internal void GoUp(double speed = 1.0)
        {
            Y = Y - (int)(11 * speed);

            if (Y < Game1.TopLimit)
            {
                Y = Game1.TopLimit;
            }
        }

        internal void GoDown(double speed = 1.0)
        {
            Y = Y + (int)(11 * speed);

            if (Y > Game1.BLimit - Height )
            {
                Y = Game1.BLimit - Height ;
            }
        }

        public void Boost(bool p)
        {
            boost = (p && Booster > 0) ? 4 : 0;
        }
    }
}