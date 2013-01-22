using System;
using Microsoft.Xna.Framework;
using SpaceDestroyer.Controllers;

namespace SpaceDestroyer.Weapons
{
    class DirectionalBullet : EnemyWeapons
    {
        private Vector2 Position = new Vector2(), dir;
        public DirectionalBullet(int x, int y, int angle, int damage)
        {
            X = x;
            Y = y;
            Position.X = X;
            Position.Y = Y;
            Power = damage;
            Speed = 10;
            Vector2 t = new Vector2(GameController.Player.X + GameController.Player.Width / 2, GameController.Player.Y + GameController.Player.Height / 2);

            dir = t - Position;
            dir.Normalize();

            Angle = (float)Math.Atan2(
                      (double)dir.Y,
                      (double)dir.X);

            Position += dir * Speed;
        }

        public override void Calculate()
        {
            Position += dir * Speed;
            X = (int) Position.X;
            Y = (int) Position.Y;
        }

        public float Angle { get; set; }
    }
}
