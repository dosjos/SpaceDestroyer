using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SpaceDestroyer.Weapons;

namespace SpaceDestroyer.Enemies
{
    class Bomber : Enemy
    {
        private enum State
        {
            Shooting,
            Moving
        };

        State Action = State.Moving;

        public Bomber(int health, int score,
                       List<EnemyWeapons> bulletList, int dropRate, Random rand, int crash, int type)
            : base(health, score, bulletList, dropRate, rand, crash, false, type)
        {
           
            Height = 74;
            Width = 150;
            pos = new Vector2(X, Y);
            
            TargetX = rand.Next(-100, Game1.SWidth);
            TargetY = rand.Next(Game1.TopLimit, Game1.BLimit + Height);
            dest = new Vector2(TargetX, TargetY);
         
            shotSpeed = rand.Next(1000, 2000);
            LastMove = DateTime.Now;
        }

        
        public override void Calculate()
        {
            if (Action == State.Moving)
            {
                Speed = 2;
                X -= 2;
                if ((DateTime.Now - LastMove).TotalMilliseconds > rand.Next(5000, 13000))
                {
                    Action = State.Shooting;
                }

            }else if (Action == State.Shooting)
            {

                Speed = 0;
                if ((DateTime.Now - LastShoot).TotalMilliseconds > 500)
                {
                    WeaponList.Add(new EBomb(X + Width/2, Y + Height/2));
                    LastShoot = DateTime.Now;
                    
                    Shoots++;
                    if (Shoots == 2)
                    {
                        LastMove = DateTime.Now;
                        Action = State.Moving;
                        Shoots = 0;
                    }
                }
            }
            if (Y < Game1.TopLimit) Y = Game1.TopLimit;
            if (Y > Game1.BLimit - Height) Y = Game1.BLimit - Height;
        }

        public int TargetX { get; set; }

        public int TargetY { get; set; }

        public Vector2 dest { get; set; }

        public int shotSpeed { get; set; }

        public Vector2 pos { get; set; }

        public int Shoots { get; set; }

        public DateTime LastMove { get; set; }
    }
}
