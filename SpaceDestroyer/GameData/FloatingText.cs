using System;
using Microsoft.Xna.Framework;

namespace SpaceDestroyer.GameData
{
    public abstract class FloatingText
    {
        public string Text { get; set; }
        public string Text2 { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Boolean Done { get; set; }

        public abstract void Calculate();

        public abstract void Calculate(GameTime gt);
    }
}