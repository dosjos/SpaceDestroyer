using System;

namespace SpaceDestroyer.PowerUp
{
    internal class BossLoot : Power
    {
        private readonly Random r = new Random();

        public BossLoot(int x, int y)
        {
            Boss = true;
            X = x;
            Y = y;

            
        }

        public override void Calculate()
        {

            X += r.Next(-2, 10);

            Y -= r.Next(-3, 3);
            if (Y < Game1.TopLimit)
            {
                Y = Game1.TopLimit;
            }
            if (Y > Game1.BLimit + 40)
            {
                Y = Game1.BLimit + 40;
            }
        }
    }
}