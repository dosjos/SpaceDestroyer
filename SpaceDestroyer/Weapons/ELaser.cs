using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceDestroyer.Weapons
{
    class ELaser : EnemyWeapons
    {
        

        public ELaser(int x, int y, int length, int damage)
        {
            X = x;
            Y = y;
            Power = damage;
            RadiusX = length;
            RadiusY = 2;
        }
        public override void Calculate()
        {
            Fired = true;
        }

        public bool Fired { get; set; }
    }
}
