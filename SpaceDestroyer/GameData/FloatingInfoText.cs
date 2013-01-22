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
            X = 100;
            Y = Game1.TopLimit + 40;
        }

        public override void Calculate()
        {
        }

        public override void Calculate(GameTime gt)
        {
            if (teller == TimeSpan.Zero)
            {
                teller = gt.ElapsedGameTime;
            }
            temp += gt.ElapsedGameTime;
            if (teller.TotalSeconds + 5 < temp.TotalSeconds)
            {
                Done = true;
            }
        }
    }
}