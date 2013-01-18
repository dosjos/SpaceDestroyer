using System;
using Microsoft.Xna.Framework;

namespace SpaceDestroyer.GameData
{
    internal class FloatingInfoText : FloatingText
    {
        private TimeSpan teller = TimeSpan.Zero;
        private TimeSpan temp;

        public FloatingInfoText(string info, string info2, int x )
        {
            Text = info;
            Text2 = info2;
            X = x;
            Y = Game1.TopLimit;
        }

        public override void Calculate()
        {
            X -= 5;
        }

        public override void Calculate(GameTime gt)
        {
            if (teller == TimeSpan.Zero)
            {
                teller = gt.ElapsedGameTime;
            }
            X = 200;
            temp += gt.ElapsedGameTime;
            if (teller.TotalSeconds + 4 < temp.TotalSeconds)
            {
                Done = true;
            }
        }
    }
}