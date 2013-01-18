using System;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceDestroyer.PowerUp
{
    public abstract class Power
    {
        public int Amount;
        public Boolean Boss;
        public int Dir;
        public int Payload;
        public int Speed;
        public int X;
        public int Y;
        public Texture2D texture;

        public abstract void Calculate();
    }
}