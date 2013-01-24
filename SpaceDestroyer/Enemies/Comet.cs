using System;

namespace SpaceDestroyer.Enemies
{
    internal class Comet : Enemy
    {
        public int dir;
        public double rotate = 0;

        public Comet(int health, int score, int droprate, Random rand, int crash, int type)
            : base(health, score, null, droprate, rand, crash, false, type)
        {
            Width = rand.Next(30, 130);
            Height = rand.Next(30, 130);
            dir = rand.Next(0, 2);
            Speed = 8;
        }

        public override void Calculate()
        {
            X -= Speed;
            if (dir == 1)
            {
                rotate -= 0.03;
            }
            else
            {
                rotate += 0.03;
            }

            if (Y < Game1.TopLimit)
            {
                Y = Game1.TopLimit;
            }
            if (Y > Game1.BLimit - Height)
            {
                Y = Game1.BLimit - Height;
            }
        }
    }
}