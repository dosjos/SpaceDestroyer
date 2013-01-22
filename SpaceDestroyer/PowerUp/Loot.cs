using System;

namespace SpaceDestroyer.PowerUp
{
    internal class Loot : Power
    {
        private readonly Random r = new Random();

        public Loot(int load, int type, int x, int y)
        {
            Payload = load;
            Amount = type;
            Boss = false;
            X = x;
            Y = y;

            Dir = r.Next(0, 3);
        }

        public override void Calculate()
        {
            if (Dir == 2)
            {
                X += r.Next(-6, 2);
            }
            else
            {
                X += r.Next(-2, 6);
            }
            Y -= r.Next(-3, 3);
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