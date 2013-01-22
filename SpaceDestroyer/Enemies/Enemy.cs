using System;
using System.Collections.Generic;
using SpaceDestroyer.Player;
using SpaceDestroyer.Weapons;

namespace SpaceDestroyer.Enemies
{
    public abstract class Enemy
    {
        public Boolean Boss = false;
        public int CrashDamage = 15;
        public int Healt { get; set; }
        public int MaxHealt { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int DropRate { get; set; }
        public List<EnemyWeapons> WeaponList { get; set; }
        public DateTime LastShoot { get; set; }
        public PlayerOne Player { get; set; }
        public int Points { get; set; }
        public int Speed { get; set; }
        public int TopLimit { get; set; }
        public int BottomLimit { get; set; }
        public Random rand { get; set; }
        public int Type { get; set; }

        public abstract void Calculate();
        public Enemy()
        {
           
        }
        
        public Enemy(int health, int score, List<EnemyWeapons> bulletList, int dropRate, Random rand, int crash, Boolean boss) 
        {
            Boss = boss;
            DropRate = dropRate;
            
            CrashDamage = crash;
            Healt = health;
            MaxHealt = health;
            Points = score;
            WeaponList = bulletList;
            DropRate = dropRate;
            WeaponList = bulletList;

            this.rand = rand;
            Y = rand.Next(Game1.TopLimit + 30, Game1.BLimit - Height);
            X = Game1.SWidth + 20;
            TopLimit = Game1.TopLimit;
            BottomLimit = Game1.BLimit;
        }

    }
}