using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceDestroyer.Backgrounds;
using SpaceDestroyer.Enemies;
using SpaceDestroyer.GameData;
using SpaceDestroyer.Player;
using SpaceDestroyer.PowerUp;
using SpaceDestroyer.Weapons;

namespace SpaceDestroyer.Controllers
{
    public class GameController
    {
        #region Enmums
        public enum Ammos
        {
            Bullet,
            Spreader,
            Rocket,
            Whirl,
            Bomb,
            Laser
        };

        public enum ShootSpeeds
        {
            Bullet = 150,
            Spreader = 50,
            Rocket = 200,
            Whirl = 1000,
            Bomb = 600,
            Laser = 1
        };

        public enum State
        {
            InGame,
            MainMenu,
            Paused,
            Difficultymenu,
            SettingsMenu,
            Intro,
            HighScore,
            Reset,
            GameOver,
            SavehighScore,
            NewHighScore
        };
        #endregion

        #region Vars
        public static PlayerOne Player;
        
        public static List<HighScore>     HighScores       = new List<HighScore>();
        private BulletController          bulletController = new BulletController();
        public List<EnemyWeapons>         enemyBullets     = new List<EnemyWeapons>();
        public List<AnimatedExplosion>    explosions       = new List<AnimatedExplosion>();
        public List<Power>                PowerUps         = new List<Power>();
        public List<FloatingText>         FloatingTexts    = new List<FloatingText>();
        public List<Enemy>                EnemyList        = new List<Enemy>();
        
        private BackgroundColor backGroundColor = new BackgroundColor();
        public static StarManager starManager;
        private LevelController levelController;

        public int[] AmmoCounter = {50, 10, 2, 2, 0, 50};
        public int ChoosenWeapon = 1;
        public int DamageMulti = 1;
        public static int NumberOfWeapons = 6;

        private DateTime LastShot;
        public static Ammos Ammo { get; set; }

        public static ShootSpeeds ShootSpeed { get; set; }

        public int Level
        {
            get { return levelController.Level; }
            set { levelController.Level = value; }
        }

        public int kills { get; set; }
        public int Score { get; set; }

        public Boolean BossMode { get; set; }

        public static State CurrentState { get; set; }

        public AnimatedExplosion animatedTexture { get; set; }
        private Boolean progresser;

        #endregion

        #region Constructor
        public GameController()
        {
            HighScores = HighScoreController.ReadAllHighScores();
        }

        internal void Reset()
        {
            levelController = new LevelController(enemyBullets, EnemyList);
            ShootSpeed = ShootSpeeds.Bullet;
            starManager = new StarManager();
            Player = new PlayerOne();
            EnemyList = new List<Enemy>();
            enemyBullets = new List<EnemyWeapons>();
            explosions = new List<AnimatedExplosion>();
            PowerUps = new List<Power>();
            FloatingTexts = new List<FloatingText>();

            levelController = new LevelController(enemyBullets, EnemyList);
            levelController.Level = 1;
            Level = 1;
            Score = 0;
            kills = 0;
            AmmoCounter = new[] {50, 10, 100, 2, 0, 50};
            ChoosenWeapon = 1;
            DamageMulti = 1;
            ShootSpeed = ShootSpeeds.Bullet;
        }
        #endregion


        #region Levels
        internal void CheckAndUpdateLevel()
        {
            if (levelController.HasText())
            {
                if (!levelController.TextIsSeen())
                {
                    FloatingTexts.Add(levelController.GetText());
                }
            }
            if (!BossMode)
            {
                if (kills != 0 && kills%10 == 0 && progresser)
                {
                    Level++;
                    progresser = false;
                }
            }

            //TODO
        }
        #endregion

        #region Enemies
        internal void AddEnemys()
        {
            if (FloatingTexts.Count == 0)
            {
                if (!BossMode)
                {
                    Enemy e = levelController.AddEnemy();
                    if (e != null)
                    {
                        if (e.Boss)
                        {
                            BossMode = true;
                        }
                        EnemyList.Add(e);
                    }
                }
            }
        }

        internal void CalculateEnemiesMovement()
        {
            foreach (Enemy enemy in EnemyList)
            {
                enemy.Calculate();
            }
        }

        internal void RemoveEnemies()
        {
            for (int i = 0; i < EnemyList.Count; i++)
            {
                if (EnemyList[i].X < -100)
                {
                    if (EnemyList[i].Type != 1 && EnemyList[i].Type != 3)
                    {
                        Player.Healt -= 5;
                    }
                    EnemyList.RemoveAt(i);
                    i--;
                }
            }
        }

        #endregion

        internal void CalculateEnemyFire()
        {
            //TODO
        }

       

        private void AddExplosion(Enemy e)
        {
            SoundController.PlayExplosion();
            var animatedTexture = new AnimatedExplosion(Vector2.Zero, 0.0f, 1.0f, 0.5f, e.X, e.Y, e.Width, e.Height, e.Speed);
            var res = spriteController.AddExplosion(animatedTexture);
            explosions.Add(res);
        }

        internal void CalculateEnemyFireHit()
        {
            //TODO
        }

        #region collissions
        internal void CalculateColisions()
        {
            Enemy e;
            for (int i = 0; i < EnemyList.Count; i++)
            {
                e = EnemyList[i];
                if (Player.X + Player.Width > e.X && Player.X < e.X + e.Width && Player.Y + Player.Height > e.Y &&
                    Player.Y < e.Y + e.Height)
                {
                    int dmg = e.CrashDamage;
                    if (Player.Shield > 0 && Player.Shield > dmg && Player.ShieldOn)
                    {
                        Player.Shield -= dmg;
                        dmg = 0;
                    }
                    else if (Player.Shield > 0 && Player.ShieldOn)
                    {
                        while (Player.Shield > 0)
                        {
                            Player.Shield--;
                            dmg--;
                        }
                    }
                    if (dmg > 0)
                    {
                        Player.Healt -= dmg;
                    }
                    AddExplosion(e);
                    if (!e.Boss)
                    {
                        EnemyList.RemoveAt(i);
                        i--;
                    }
                }
            }
        }
        #endregion

        #region Loot
        private void AddLoot(Enemy e)
        {
            var r = new Random();
            int amount = 0;
            int type = r.Next(0, 10);
            amount = SelectAmount(type, amount, r);
            int tmp = r.Next(1, 100);
            if (tmp < e.DropRate && !e.Boss)
            {
                PowerUps.Add(new Loot(type, amount, e.X, e.Y));
            }
            else if (e.Boss)
            {
                PowerUps.Add(new BossLoot(e.X, e.Y));
            }
        }

        private static int SelectAmount(int type, int amount, Random r)
        {
            if (type == 0) amount = r.Next(20, 70);
            else if (type == 1) amount = r.Next(100, 300);
            else if (type == 2) amount = r.Next(20, 70);
            else if (type == 3) amount = r.Next(20, 110);
            else if (type == 4) amount = r.Next(40, 150);
            else if (type == 5) amount = r.Next(20, 70);
            else if (type == 6) amount = r.Next(20, 40);
            else if (type == 7) amount = r.Next(1, 10);
            else if (type == 8) amount = r.Next(20, 500);
            return amount;
        }

        internal void CalculateLootMovement()
        {
            foreach (Power powerUp in PowerUps)
            {
                powerUp.Calculate();
            }
        }

        #endregion

        #region Weapons
        internal void AddBullet()
        {
            DateTime now = DateTime.Now;
            if ((now - LastShot).TotalMilliseconds > (int)ShootSpeed)
            {
                if (AmmoCounter[ChoosenWeapon - 1] > 0)
                {
                    if (Ammo == Ammos.Bullet)
                    {
                        SoundController.PlayBulletSound();
                        if (DamageMulti > 6)
                        {
                            bulletController.AddShoot(new Bullet(Player.X + Player.Width,
                                                                 Player.Y + (Player.Height / 2) - 20, 5 * DamageMulti));
                            bulletController.AddShoot(new Bullet(Player.X + Player.Width,
                                                                 Player.Y + (Player.Height / 2) - 10, 5 * DamageMulti));
                            bulletController.AddShoot(new Bullet(Player.X + Player.Width,
                                                                 Player.Y + (Player.Height / 2) + 10, 5 * DamageMulti));
                            bulletController.AddShoot(new Bullet(Player.X + Player.Width,
                                                                 Player.Y + (Player.Height / 2) + 20, 5 * DamageMulti));
                        }
                        else if (DamageMulti > 4)
                        {
                            bulletController.AddShoot(new Bullet(Player.X + Player.Width,
                                                                 Player.Y + (Player.Height / 2) - 10, 5 * DamageMulti));
                            bulletController.AddShoot(new Bullet(Player.X + Player.Width,
                                                                 Player.Y + (Player.Height / 2) + 10, 5 * DamageMulti));
                        }
                        else
                        {
                            bulletController.AddShoot(new Bullet(GameController.Player.X + GameController.Player.Width, GameController.Player.Y + (GameController.Player.Height / 2),
                                                                 5 * DamageMulti));
                        }
                    }
                    if (Ammo == Ammos.Spreader)
                    {
                        SoundController.PlaySpreaderSound();
                        if (DamageMulti > 4)
                        {
                            bulletController.AddShoot(new Spreader(Player, 4, 10 * DamageMulti));
                            bulletController.AddShoot(new Spreader(Player, 5, 10 * DamageMulti));
                            bulletController.AddShoot(new Spreader(Player, 6, 10 * DamageMulti));
                            bulletController.AddShoot(new Spreader(Player, 7, 10 * DamageMulti));
                            bulletController.AddShoot(new Spreader(Player, 8, 10 * DamageMulti));
                            bulletController.AddShoot(new Spreader(Player, 9, 10 * DamageMulti));
                        }
                        else
                        {
                            bulletController.AddShoot(new Spreader(Player, 1, 10 * DamageMulti));
                            bulletController.AddShoot(new Spreader(Player, 2, 10 * DamageMulti));
                            bulletController.AddShoot(new Spreader(Player, 3, 10 * DamageMulti));
                        }
                    }
                    if (Ammo == Ammos.Rocket)
                    {
                        if (DamageMulti > 5)
                        {
                            bulletController.AddShoot(new Rocket(20*DamageMulti, EnemyList));
                            bulletController.AddShoot(new Rocket(20 * DamageMulti, EnemyList));
                        }
                        else
                        {
                            bulletController.AddShoot(new Rocket(20 * DamageMulti, EnemyList));
                        }
                    }

                    if (Ammo == Ammos.Laser)
                    {
                        Enemy tmp = FireLaser();

                        if (tmp != null)
                        {
                            bulletController.AddShoot(new Laser(Player.X, Player.Y, Player.Width, Player.Height, tmp.X,
                                                                3 * DamageMulti));
                            tmp.Healt -= 3 * DamageMulti;
                        }
                        else
                        {
                            bulletController.AddShoot(new Laser(Player.X, Player.Y, Player.Width, Player.Height,
                                                                Game1.SWidth, 3 * DamageMulti));
                        }
                    }
                    AmmoCounter[ChoosenWeapon - 1]--;
                    LastShot = DateTime.Now;
                }
            }
        }

        private Enemy FireLaser()
        {
            Enemy e, tmp = null;

            for (int i = 0; i < EnemyList.Count; i++)
            {
                e = EnemyList[i];
                if (e.Y < Player.Y + Player.Height / 2 && e.Y + e.Height > Player.Y + Player.Width / 2 && e.X > Player.X)
                {
                    if (tmp == null)
                    {
                        tmp = e;
                    }
                    if (tmp.X > e.X)
                    {
                        tmp = e;
                    }
                }
            }

            return tmp;
        }
        internal List<PlayerWeapon> GetAllBullets()
        {
            return bulletController.GetAllBullets();
        }


        internal void WeaponDown()
        {
            ChoosenWeapon--;

            if (ChoosenWeapon < 1)
            {
                ChoosenWeapon = 6;
            }
            findAmmoType();
        }

        private void findAmmoType()
        {
            if (ChoosenWeapon == 1)
            {
                Ammo = Ammos.Bullet;
                ShootSpeed = ShootSpeeds.Bullet;
            }
            if (ChoosenWeapon == 2)
            {
                Ammo = Ammos.Spreader;
                ShootSpeed = ShootSpeeds.Spreader;
            }
            if (ChoosenWeapon == 3)
            {
                Ammo = Ammos.Rocket;
                ShootSpeed = ShootSpeeds.Rocket;
            }
            if (ChoosenWeapon == 4)
            {
                Ammo = Ammos.Whirl;
                ShootSpeed = ShootSpeeds.Whirl;
            }
            if (ChoosenWeapon == 5)
            {
                Ammo = Ammos.Bomb;
                ShootSpeed = ShootSpeeds.Bomb;
            }
            if (ChoosenWeapon == 6)
            {
                Ammo = Ammos.Laser;
                ShootSpeed = ShootSpeeds.Laser;
            }
        }

        internal void WeaponUp()
        {
            ChoosenWeapon++;
            if (ChoosenWeapon > 6)
            {
                ChoosenWeapon = 1;
            }
            findAmmoType();
        }

        internal void SetChoosenWeapon(int p)
        {
            ChoosenWeapon = p;
            findAmmoType();
        }
        #endregion

        internal void CalculateExplosions(float gameTime)
        {
            for (int i = 0; i < explosions.Count; i++)
            {
                explosions[i].UpdateFrame(gameTime);
            }
        }

        #region bullets
        internal void RemoveBullets()
        {
            bulletController.RemoveBullets();
        }

        internal void CalculateBullets()
        {
            bulletController.Calculate();
        }

        internal void CalculateBulletsHits()
        {
            Enemy e;
            PlayerWeapon pw;
            for (int i = 0; i < bulletController.Bullets.Count; i++)
            {
                pw = bulletController.Bullets[i];
                for (int j = 0; j < EnemyList.Count; j++)
                {
                    e = EnemyList[j];
                    if (pw.X + pw.RadiusX > e.X && pw.X < e.X + e.Width && pw.Y + pw.RadiusY > e.Y &&
                        pw.Y < e.Y + e.Height)
                    {
                        e.Healt = e.Healt - pw.Power;
                        try
                        {
                            bulletController.Bullets.RemoveAt(i);
                            i--;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                        }
                    }

                    if (e.Healt <= 0)
                    {
                        //TODO
                        //Add loot
                        //Todo skjekk om boss, hvis boss, spessiel loot og level ++

                        if (e.Boss)
                        {
                            //TODO add boss looot
                            BossMode = false;
                            Level++;
                        }
                        else
                        {
                            AddLoot(e);
                        }
                        AddExplosion(e);

                        Score += e.Points;
                        kills++;
                        if (!BossMode)
                        {
                            progresser = true;
                        }

                        EnemyList.RemoveAt(j);
                        j--;
                    }
                }
            }
            //TODO
        }
#endregion

        #region Lootcalculation
        internal void CalculateLootCollision()
        {
            for (int i = 0; i < PowerUps.Count; i++)
            {
                Power loot = PowerUps[i];
                if (Player.X + Player.Width > loot.X && Player.X < loot.X + 33 && Player.Y + Player.Height > loot.Y &&
                    Player.Y < loot.Y + 33)
                {
                    switch (loot.Payload)
                    {
                        case 0:
                            Player.Healt += loot.Amount;
                            if (Player.Healt > 200) Player.Healt = 200;
                            break;
                        case 1:
                            Player.Shield += loot.Amount;
                            if (Player.Shield > 200) Player.Shield = 200;
                            break;
                        case 2:
                            Player.Booster += loot.Amount;
                            if (Player.Booster > 200) Player.Booster = 200;
                            break;
                        case 3:
                            AmmoCounter[0] += loot.Amount;
                            break;
                        case 4:
                            AmmoCounter[1] += loot.Amount;
                            break;
                        case 5:
                            AmmoCounter[2] += loot.Amount;
                            break;
                        case 6:
                            AmmoCounter[3] += loot.Amount;
                            break;
                        case 7:
                            AmmoCounter[4] += loot.Amount;
                            break;
                        case 8:
                            AmmoCounter[5] += loot.Amount;
                            break;
                    }
                    PowerUps.RemoveAt(i);
                    i--;
                }
            }
        }

        #endregion

        #region texts
        internal void CalculateTexts(GameTime gt)
        {
            foreach (FloatingText text in FloatingTexts)
            {
                text.Calculate(gt);
            }
        }

        internal void RemoveTexts()
        {
            for (int i = 0; i < FloatingTexts.Count; i++)
            {
                if (FloatingTexts[i].X < -100)
                {
                    FloatingTexts.RemoveAt(i);
                    i--;
                }
                else if (FloatingTexts[i] is FloatingInfoText)
                {
                    if ((FloatingTexts[i]).Done)
                    {
                        FloatingTexts.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

        #endregion

        internal bool CheckIfDied()
        {
            return (Player.Healt <= 0);
        }

        internal bool CheckIfHighScore(int score)
        {
            Boolean ret = false;
            if (HighScores.Count == 0)
            {
                return true;
            }
            foreach (HighScore highScore in HighScores)
            {
                if (score > highScore.Score) ret = true;
            }
            return ret;
        }

        internal static void AddHigh(HighScore high)
        {
            HighScores.Add(high);
        }

        #region background
        internal void CalculateBackgroundElements()
        {
            starManager.Calculate();
            backGroundColor.Calculate();
        }

        internal Color GetBackgroundColor()
        {
            return backGroundColor.GetBackgroundColor();
        }
        #endregion

        internal void RegisterSpriteController(SpriteController spriteController)
        {
            this.spriteController = spriteController;
        }

        public SpriteController spriteController { get; set; }
    }
}