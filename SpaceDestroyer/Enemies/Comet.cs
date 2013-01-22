using System;

namespace SpaceDestroyer.Enemies
{
    internal class Comet : Enemy
    {
        public int dir;
        public double rotate = 0;

        public Comet(int p1, int p2, int p3, Random rand, int crash)
        {
            CrashDamage = crash;
            Healt = p1;
            MaxHealt = p1;
            Points = p2;
            this.TopLimit = Game1.TopLimit;
            this.BottomLimit = Game1.BLimit;
            DropRate = p3;
            Width = Width;
            this.rand = rand;
            X = Game1.SWidth + 10;

            Y = rand.Next(TopLimit + 10, BottomLimit - 10);
            Type = 1;
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
            if (Y > Game1.BLimit - 40)
            {
                Y = Game1.BLimit - 40;
            }
        }
    }
}