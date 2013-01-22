using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceDestroyer.Weapons
{
    class EBullet : EnemyWeapons
    {
        
        public EBullet(int x, int y, int damage)
        {
            X = x;
            Y = y;
            Power = damage;
            RadiusX = 7;
            RadiusY = 4;
        }


        public override void Calculate()
        {
            X -= 7;
        }
    }
}
