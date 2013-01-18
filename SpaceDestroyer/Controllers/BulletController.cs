using System.Collections.Generic;
using SpaceDestroyer.Weapons;

namespace SpaceDestroyer.Controllers
{
    internal class BulletController
    {
        public List<PlayerWeapon> Bullets;

        public BulletController()
        {
            Bullets = new List<PlayerWeapon>();
        }

        internal void AddShoot(PlayerWeapon bullet)
        {
            Bullets.Add(bullet);
        }

        internal List<PlayerWeapon> GetAllBullets()
        {
            return Bullets;
        }

        internal void Calculate()
        {
            Bullets.ForEach(x => x.Calculate());
        }


        internal void RemoveBullets()
        {
            for (int i = 0; i < Bullets.Count; i++)
            {
                if (Bullets[i].X > Game1.SWidth + 10)
                {
                    Bullets.RemoveAt(i);
                    i--;
                    continue;
                }
                var laser = Bullets[i] as Laser;
                if (laser != null)
                {
                    if (laser.Fired)
                    {
                        Bullets.RemoveAt(i);
                        i--;
                    }
                }
            }
        }
    }
}