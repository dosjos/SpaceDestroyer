using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
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

        public static List<HighScore>     HighScores = new List<HighScore>();
        private BulletController          bulletController = new BulletController();
        public List<EnemyWeapons>         EnemyBullets     = new List<EnemyWeapons>();
        public List<AnimatedExplosion>    Explosions       = new List<AnimatedExplosion>();
        public List<Power>                PowerUps         = new List<Power>();
        public List<FloatingText>         FloatingTexts    = new List<FloatingText>();
        public List<Enemy>                EnemyList        = new List<Enemy>();
        
        private readonly BackgroundColor _backGroundColor = new BackgroundColor();
        public static StarManager StarManager;
        private LevelController _levelController;

        public int[] AmmoCounter = {50, 10, 2, 2, 0, 50};
        public int ChoosenWeapon = 1;
        public int DamageMulti = 1;
        public static int NumberOfWeapons = 6;

        private DateTime _lastShot;
        public static Ammos Ammo { get; set; }

        public static ShootSpeeds ShootSpeed { get; set; }

        public int Level
        {
            get { return _levelController.Level; }
            set { _levelController.Level = value; }
        }

        public int Kills { get; set; }
        public int Score { get; set; }

        public Boolean BossMode { get; set; }

        public static State CurrentState { get; set; }

        public AnimatedExplosion AnimatedTexture { get; set; }
        private Boolean _progresser;

        #endregion

        #region Constructor
        public GameController()
        {
            HighScores = HighScoreController.ReadAllHighScores();
        }

        internal void Reset()
        {
            _levelController = new LevelController(EnemyBullets, EnemyList);
            ShootSpeed = ShootSpeeds.Bullet;
            StarManager = new StarManager();
            Player = new PlayerOne();
            EnemyList = new List<Enemy>();
            EnemyBullets = new List<EnemyWeapons>();
            Explosions = new List<AnimatedExplosion>();
            PowerUps = new List<Power>();
            FloatingTexts = new List<FloatingText>();

            _levelController = new LevelController(EnemyBullets, EnemyList) {Level = 1};
            Level = 1;
            Score = 0;
            Kills = 0;
            AmmoCounter = new[] {150, 20, 5, 20, 0, 50};
            ChoosenWeapon = 1;
            DamageMulti = 1;
            ShootSpeed = ShootSpeeds.Bullet;
        }
        #endregion


        #region Levels
        internal Boolean CheckAndUpdateLevel()
        {
            if (_levelController.HasText())
            {
                if (!_levelController.TextIsSeen())
                {
                    FloatingTexts.Add(_levelController.GetText());
                }
            }
            if (!BossMode)
            {
                if (Kills != 0 && Kills%10 == 0 && _progresser)
                {
                    Level++;
                    _progresser = false;
                }
            }
            if (_levelController.Level == _levelController.levels.Count && EnemyList.Count == 0)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region Enemies
        internal void AddEnemys()
        {
            if (FloatingTexts.Count == 0)
            {
                if (!BossMode)
                {
                    Enemy e = _levelController.AddEnemy();
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


        #region enemyFire
        internal void CalculateEnemyFire()
        {
            foreach (var enemyWeaponse in EnemyBullets)
            {
                enemyWeaponse.Calculate();
            }
        }

        private void AddExplosion(Enemy e)
        {
            SoundController.PlayExplosion();
            var animatedExplosion = new AnimatedExplosion(Vector2.Zero, 0.0f, 1.0f, 0.5f, e.X, e.Y, e.Width, e.Height, e.Speed);
            var res = spriteController.AddExplosion(animatedExplosion, 10);
            Explosions.Add(res);
        }

        internal void CalculateEnemyFireHit()
        {
            for (int i = 0; i < EnemyBullets.Count; i++)
            {
                EnemyWeapons e = EnemyBullets[i];

                if (e.X + e.RadiusX > Player.X && e.X < Player.X + Player.Width && e.Y + e.RadiusY > Player.Y &&
                    e.Y < Player.Y + Player.Height)
                {
                    var animatedExplosion = new AnimatedExplosion(Vector2.Zero, 0.0f, 0.5f, 1.0f, e.X, e.Y, 15, 15, 5);
                    var res = spriteController.AddExplosion(animatedExplosion, 10);
                    Explosions.Add(res);
                    int dmg = e.Power;
                     FloatingTexts.Add(new DamageText(Player.X + Player.Width / 2, Player.Y, dmg));
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

                    EnemyBullets.RemoveAt(i);
                    i--;
                }
            }
        }

        public void RemoveEnemyBullets()
        {
            for (int i = 0; i < EnemyBullets.Count; i++)
            {
                if (EnemyBullets[i].X < -20 || EnemyBullets[i].X > Game1.SWidth + 20 || EnemyBullets[i].Y < 0 - 20 ||
                    EnemyBullets[i].Y > Game1.SHeight + 20)
                {
                    EnemyBullets.RemoveAt((i));
                    i--;
                    continue;
                }

                if (EnemyBullets[i] is EBomb)
                {
                    if (((EBomb) EnemyBullets[i]).destruct <= 0)
                    {
                        var animatedExplosion = new AnimatedExplosion(Vector2.Zero, 0.0f, 0.5f, 1.0f, EnemyBullets[i].X-10, EnemyBullets[i].Y-10, 40, 40, 0);
                        var res = spriteController.AddExplosion(animatedExplosion, 10);
                        Explosions.Add(res);
                        EnemyBullets.RemoveAt((i));
                        i--;
                    }
                }
            }
        }
        #endregion

        #region collissions
        internal void CalculateColisions()
        {
            for (int i = 0; i < EnemyList.Count; i++)
            {
                Enemy e = EnemyList[i];
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
            else if (type == 3) amount = r.Next(50, 200);
            else if (type == 4) amount = r.Next(50, 100);
            else if (type == 5) amount = r.Next(10, 40);
            else if (type == 6) amount = r.Next(10, 30);
            else if (type == 7) amount = r.Next(1, 5);
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
            if ((now - _lastShot).TotalMilliseconds > (int)ShootSpeed)
            {
                if (AmmoCounter[ChoosenWeapon - 1] > 0)
                {
                    if (Ammo == Ammos.Bullet)
                    {
                        SoundController.PlayBulletSound();
                        if (DamageMulti > 3)
                        {
                            bulletController.AddShoot(new Bullet(Player.X + Player.Width -10,
                                                                 Player.Y + (Player.Height / 2) - 20, 5 * DamageMulti));
                            bulletController.AddShoot(new Bullet(Player.X + Player.Width - 5,
                                                                 Player.Y + (Player.Height / 2) - 10, 5 * DamageMulti));
                            bulletController.AddShoot(new Bullet(Player.X + Player.Width - 5,
                                                                 Player.Y + (Player.Height / 2) + 10, 5 * DamageMulti));
                            bulletController.AddShoot(new Bullet(Player.X + Player.Width - 10,
                                                                 Player.Y + (Player.Height / 2) + 20, 5 * DamageMulti));
                        }
                        else if (DamageMulti > 2)
                        {
                            bulletController.AddShoot(new Bullet(Player.X + Player.Width,
                                                                 Player.Y + (Player.Height / 2) - 10, 5 * DamageMulti));
                            bulletController.AddShoot(new Bullet(Player.X + Player.Width,
                                                                 Player.Y + (Player.Height / 2) + 10, 5 * DamageMulti));
                        }
                        else
                        {
                            bulletController.AddShoot(new Bullet(Player.X + Player.Width, Player.Y + (Player.Height / 2),
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

                    if (Ammo == Ammos.Whirl)
                    {
                        int xx = 0;
        int yy = 0;
        int t = 0;
                        for (int i = 0; i < 60; i++)
                        {
                            if (t == 0)
                            {
                                xx = 5;
                                yy = 0;
                            }
                            if (t == 1)
                            {
                                xx = 3;
                                yy = -2;
                            }
                            if (t == 2)
                            {
                                xx = 2;
                                yy = -3;
                            }
                            if (t == 3)
                            {
                                xx = 0;
                                yy = -5;
                            }
                            if (t == 4)
                            {
                                xx = -2;
                                yy = -3;
                            }
                            if (t == 5)
                            {
                                xx = -3;
                                yy = -2;
                            }
                            if (t == 6)
                            {
                                xx = -5;
                                yy = 0;
                            }
                            if (t == 7)
                            {
                                xx = -3;
                                yy = 2;
                            }
                            if (t == 8)
                            {
                                xx = -2;
                                yy = 3;
                            }
                            if (t == 9)
                            {
                                xx = 0;
                                yy = 5;
                            }
                            if (t == 10)
                            {
                                xx = 2;
                                yy = 3;
                            }
                            if (t == 11)
                            {
                                xx = 3;
                                yy = 2;
                            }

                            bulletController.AddShoot(new Whirl(10*DamageMulti, xx, yy, i));
                            t++;
                            if (t == 12)
                            {
                                t = 0;
                            }

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
                            FloatingTexts.Add(new DamageText(tmp.X + tmp.Width/2, tmp.Y, 3* DamageMulti));
                            var animatedExplosion = new AnimatedExplosion(Vector2.Zero, 0.0f, 0.5f, 1.0f, tmp.X, Player.Y+(Player.Height/2)-7, 15, 15, 0);
                            var res = spriteController.AddExplosion(animatedExplosion, 10);
                            Explosions.Add(res);
                        }
                        else
                        {
                            bulletController.AddShoot(new Laser(Player.X, Player.Y, Player.Width, Player.Height,
                                                                Game1.SWidth, 3 * DamageMulti));
                        }
                    }
                    AmmoCounter[ChoosenWeapon - 1]--;
                    _lastShot = DateTime.Now;
                }
            }
        }

        private Enemy FireLaser()
        {
            Enemy tmp = null;

            foreach (Enemy t in EnemyList)
            {
                Enemy e = t;
                if (e.Y < Player.Y + Player.Height / 2 && e.Y + e.Height > Player.Y + Player.Height / 2 && e.X > Player.X)
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
            FindAmmoType();
        }

        private void FindAmmoType()
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
            FindAmmoType();
        }

        internal void SetChoosenWeapon(int p)
        {
            ChoosenWeapon = p;
            FindAmmoType();
        }
        #endregion

        internal void CalculateExplosions(float gameTime)
        {
            foreach (AnimatedExplosion t in Explosions)
            {
                t.UpdateFrame(gameTime);
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
            foreach (var playerWeapon in bulletController.Bullets)
            {
                if (playerWeapon is Rocket)
                {
                    var animatedExplosion = new AnimatedExplosion(Vector2.Zero, 0.0f, 0.5f, 1.0f, playerWeapon.X, playerWeapon.Y, 10, 10, 0);
                    var res = spriteController.AddExplosion(animatedExplosion, 20);
                    Explosions.Add(res);
                }
            }
        }

        internal void CalculateBulletsHits()
        {
            for (int i = 0; i < bulletController.Bullets.Count; i++)
            {
                bool expl = false;
                PlayerWeapon pw = bulletController.Bullets[i];
                for (int j = 0; j < EnemyList.Count; j++)
                {
                    Enemy e = EnemyList[j];
                    if (pw.X + pw.RadiusX > e.X && pw.X < e.X + e.Width && pw.Y + pw.RadiusY > e.Y &&
                        pw.Y < e.Y + e.Height)
                    {
                        e.Healt = e.Healt - pw.Power;
                        FloatingTexts.Add(new DamageText(e.X + e.Width / 2, e.Y, pw.Power * DamageMulti));
                        expl = true;
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
                        if (e.Boss)
                        {
                            BossMode = false;
                            Level++;
                        }
                        AddLoot(e);
                        AddExplosion(e);

                        Score += e.Points;
                        Kills++;
                        if (!BossMode)
                        {
                            _progresser = true;
                        }

                        EnemyList.RemoveAt(j);
                        j--;
                    }
                    if(expl){
                        var animatedExplosion = new AnimatedExplosion(Vector2.Zero, 0.0f, 0.5f, 1.0f, pw.X, pw.Y, 15, 15, 0);
                        var res = spriteController.AddExplosion(animatedExplosion, 10);
                        Explosions.Add(res);
                    }
                }
            }
        }
#endregion

        #region Lootcalculation
        internal void CalculateLootCollision()
        {
            for (int i = 0; i < PowerUps.Count; i++)
            {
                Power loot = PowerUps[i];

                if (loot.Boss)
                {
                    if (Player.X + Player.Width > loot.X && Player.X < loot.X + 94 && Player.Y + Player.Height > loot.Y &&
                        Player.Y < loot.Y + 94)
                    {
                        PowerUps.RemoveAt(i);
                        i--;
                        DamageMulti++;
                        continue;
                    }
                }


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
                
                if (FloatingTexts[i] is FloatingInfoText)
                {
                    if ((FloatingTexts[i]).Done)
                    {
                        FloatingTexts.RemoveAt(i);
                        i--;
                        continue;
                    }
                }

                if (FloatingTexts[i] is DamageText)
                {
                    if (((DamageText)FloatingTexts[i]).StartY - 40 > ((DamageText)FloatingTexts[i]).Y)
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
            StarManager.Calculate();
            _backGroundColor.Calculate();
        }

        internal Color GetBackgroundColor()
        {
            return _backGroundColor.GetBackgroundColor();
        }
        #endregion

        internal void RegisterSpriteController(SpriteController spriteController)
        {
            this.spriteController = spriteController;
        }

        public SpriteController spriteController { get; set; }
    }
}