using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceDestroyer.Backgrounds;
using SpaceDestroyer.Enemies;
using SpaceDestroyer.GameData;
using SpaceDestroyer.Player;
using SpaceDestroyer.PowerUp;
using SpaceDestroyer.ScreenManagers;
using SpaceDestroyer.Weapons;

namespace SpaceDestroyer.Controllers
{
    public class SpriteController
    {
        #region declarations
        private SpriteFont MediumNumber;

        private AnimatedExplosion SpriteTexture;
        private SpriteBatch spriteBatch;
        private ContentManager content;
        private int FlameCounter;

        private readonly GraphicsDevice _graphicsDevice;
        private GameServiceContainer gameServiceContainer;

        public GameController gameController { get; set; }

        private int ShieldBlink;

        #endregion

        #region Constructor
        public SpriteController(SpriteBatch SB, GameController gameController, GameServiceContainer gameServiceContainer, GraphicsDevice graphicsDevice)
        {
            this.gameController = gameController;
            _graphicsDevice = graphicsDevice;
            spriteBatch = SB;
            if (content == null) content = new ContentManager(gameServiceContainer, "Content");
            LoadSprites();
            LoadFonts();
        }
        #endregion

        #region FontsAndSprites
        private void LoadFonts()
        {
            //FONTS
            LKSFont = content.Load<SpriteFont>("Fonts/LevelKillScore");
            LargeNumber = content.Load<SpriteFont>("Fonts/LargeNumbers");
            MediumNumber = content.Load<SpriteFont>("Fonts/MediumNumbers");
            SmalNumbers = content.Load<SpriteFont>("Fonts/SmalNumbers");
            FloatingBig = content.Load<SpriteFont>("Fonts/FloatingBig");
        }

        private void LoadSprites()
        {
            //SPRITES
            StarTexture = content.Load<Texture2D>("Shapes/Circle");
            dummyTexture = new Texture2D(_graphicsDevice, 1, 1);
            dummyTexture.SetData(new[] { Color.LightGray });
            ship = content.Load<Texture2D>("Player/ship");
            Bullet = content.Load<Texture2D>("Weapons/Bullet");
            RocketTexture = content.Load<Texture2D>("Weapons/rocket");
            SwirlTexture = content.Load<Texture2D>("Weapons/Swirl");
            Wall = content.Load<Texture2D>("Shapes/wall");
            Healt = content.Load<Texture2D>("Shapes/Heart");
            Boost = content.Load<Texture2D>("Shapes/pflame");
            Shield = content.Load<Texture2D>("Shapes/shield2");
            CrateAmmo = content.Load<Texture2D>("Shapes/ammocrate");
            CrateHealth = content.Load<Texture2D>("Shapes/crateHealth");
            CrateSpecial = content.Load<Texture2D>("Shapes/crateUpgrade");
            Crate = content.Load<Texture2D>("Shapes/crate");
            ShortFlame = content.Load<Texture2D>("Shapes/pflamel");
            LongFlame = content.Load<Texture2D>("Shapes/pflame");
            ShortFlame2 = content.Load<Texture2D>("Shapes/pflamel2");
            LongFlame2 = content.Load<Texture2D>("Shapes/pflame2");
            PShield = content.Load<Texture2D>("Player/shield");

            CargoCrateT = content.Load<Texture2D>("Enemies/StashCrate");
            EnemySentry = content.Load<Texture2D>("Enemies/EnemySentry");
            EnemyComet = content.Load<Texture2D>("Enemies/comet");
            EnemyStriker = content.Load<Texture2D>("Enemies/EnemyStriker");
            EnemyBomber = content.Load<Texture2D>("Enemies/Bomber");
            EBomb = content.Load<Texture2D>("Weapons/Bomb");


            SmallBossT = content.Load<Texture2D>("Enemies/boss");
            Exp1 = content.Load<Texture2D>("Shapes/ani2");
            Exp2 = content.Load<Texture2D>("Shapes/ani3");
            Exp3 = content.Load<Texture2D>("Shapes/ani6");
            Exp4 = content.Load<Texture2D>("Shapes/ani7");
            Exp5 = content.Load<Texture2D>("Shapes/ani8");
        }
        #endregion

        #region SpritesAndFontsDeclaration
        private Texture2D StarTexture;
        public Texture2D ship { get; set; }
        public Texture2D PShield { get; set; }
        public Texture2D dummyTexture { get; set; }
        public Texture2D Bullet { get; set; }
        public Texture2D Wall { get; set; }
        public Texture2D Healt { get; set; }
        public Texture2D Shield { get; set; }
        public Texture2D Boost { get; set; }
        public Texture2D EnemySentry { get; set; }
        public Texture2D EnemyComet { get; set; }
        public Texture2D EnemyStriker { get; set; }
        public Texture2D EnemyBomber { get; set; }
        public Texture2D EBomb { get; set; }
        public Texture2D CrateAmmo { get; set; }
        public Texture2D Crate { get; set; }
        public Texture2D CrateSpecial { get; set; }
        public Texture2D CrateHealth { get; set; }
        public Texture2D CargoCrateT { get; set; }
        public Texture2D SwirlTexture { get; set; }

        public Texture2D RocketTexture { get; set; }

        public Texture2D Exp1 { get; set; }

        public Texture2D Exp2 { get; set; }

        public Texture2D Exp3 { get; set; }

        public Texture2D Exp4 { get; set; }

        public Texture2D Exp5 { get; set; }
        public SpriteFont LKSFont { get; set; }
        public SpriteFont LargeNumber { get; set; }
        public SpriteFont FloatingBig { get; set; }
        public SpriteFont SmalNumbers { get; set; }
        public Texture2D SmallBossT { get; set; }

        public Texture2D ShortFlame { get; set; }
        public Texture2D LongFlame { get; set; }
        public Texture2D ShortFlame2 { get; set; }
        public Texture2D LongFlame2 { get; set; }
        #endregion

        #region Background
        internal void DrawBackGroundElements()
        {
            SetBackGroundColor();
            DrawStars();
        }

        private void DrawStars()
        {
            foreach (var allStar in GameController.StarManager.GetAllStars())
            {
                spriteBatch.Draw(StarTexture, new Rectangle(allStar.X, allStar.Y, allStar.Radius, allStar.Radius), Color.White);
            }
        }

        private void SetBackGroundColor()
        {
            _graphicsDevice.Clear(gameController.GetBackgroundColor());
        }
        #endregion


        #region FloatingTexts
        public void DrawFloatingTexts()
        {
            foreach (FloatingText floatingText in gameController.FloatingTexts)
            {
                if (floatingText is FloatingInfoText)
                {
                    spriteBatch.DrawString(FloatingBig, floatingText.Text,
                                           new Vector2(floatingText.X, floatingText.Y - 40), Color.White);
                    if (!string.IsNullOrEmpty(floatingText.Text2))
                    {
                        spriteBatch.DrawString(FloatingBig, floatingText.Text2,
                                               new Vector2(floatingText.X + 40, floatingText.Y + 20), Color.Red);
                    }
                }

                if (floatingText is DamageText)
                {
                    spriteBatch.DrawString(SmalNumbers, floatingText.Text,
                                          new Vector2(floatingText.X, floatingText.Y), Color.Red);
                }
            }
        }
        #endregion

        #region loot
        public void DrawLoot()
        {
            Power p;
            for (int i = 0; i < gameController.PowerUps.Count; i++)
            {
                p = gameController.PowerUps[i];

                if (p.Boss)
                {
                    var v = new Vector2(p.X, p.Y);
                    spriteBatch.Draw(CrateSpecial, v, null, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
                }
                else
                {
                    Texture2D tex;
                    if (p.Payload == 0)
                    {
                        tex = CrateHealth;
                    }
                    else if (p.Payload == 1 || p.Payload == 2)
                    {
                        tex = CrateSpecial;
                    }
                    else
                    {
                        tex = CrateAmmo;
                    }

                    var v = new Vector2(p.X, p.Y);
                    spriteBatch.Draw(tex, v, null, Color.White, 0f, Vector2.Zero, 0.7f, SpriteEffects.None, 0f);
                }
            }
        }
        #endregion

        #region Explosion
        public void DrawExplosions()
        {
            for (int i = 0; i < gameController.Explosions.Count; i++)
            {
               
                if (gameController.Explosions[i].IsPaused)
                {
                    gameController.Explosions.RemoveAt(i);
                    i--;
                }
                else
                {
                    gameController.Explosions[i].DrawFrame(spriteBatch);
                }
            }
        }
        private Texture2D SelectExplosionTexture(Texture2D t)
        {
            var r = new Random();
            int rr = r.Next(0, 5);
            if (rr == 0) t = Exp1;
            else if (rr == 1) t = Exp2;
            else if (rr == 2) t = Exp3;
            else if (rr == 3) t = Exp4;
            else if (rr == 4) t = Exp5;
            return t;
        }

        internal AnimatedExplosion AddExplosion(AnimatedExplosion animatedTexture, int s)
        {
            if (s == 0)
            {
                s = 10;
            }
            Texture2D t = null;
            t = SelectExplosionTexture(t);
            animatedTexture.Load(t, s);
            var frames = new int[4] { 4, 4, 4, 4 };
            animatedTexture.defineFrames(4, frames);
            animatedTexture.setRow(1);
            return animatedTexture;
        }
#endregion

        #region statsAndFrames
        private void DrawGameStats()
        {
            int spacing = (Game1.SWidth - (20 * 7)) / GameController.NumberOfWeapons;
            spriteBatch.DrawString(LKSFont, "Level",
                                   new Vector2((Game1.SWidth / 2) + (spacing / 3), Game1.BLimit + 20),
                                   Color.White);

            spriteBatch.DrawString(LargeNumber, "" + gameController.Level,
                                   new Vector2((Game1.SWidth / 2) + (spacing / 3) + 30,
                                               Game1.BLimit + 50), Color.White);

            int numLength = ("" + gameController.Kills).Length * -10;

            spriteBatch.DrawString(LKSFont, "Kills",
                                   new Vector2((Game1.SWidth / 2) + ((spacing) * 2) - (spacing / 2) - 20,
                                               Game1.BLimit + 20), Color.White);
            spriteBatch.DrawString(LargeNumber, "" + gameController.Kills,
                                   new Vector2((Game1.SWidth / 2) + ((spacing) * 2) - (spacing / 2) + numLength + 10,
                                               Game1.BLimit + 50), Color.White);

            numLength = ("" + gameController.Score).Length * -10;

            spriteBatch.DrawString(LKSFont, "Score",
                                   new Vector2((Game1.SWidth / 2) + ((spacing) * 3) - (spacing / 2) - 20,
                                               Game1.BLimit + 20), Color.White);
            spriteBatch.DrawString(LargeNumber, "" + gameController.Score,
                                   new Vector2((Game1.SWidth / 2) + ((spacing) * 3) - (spacing / 2) + numLength + 10,
                                               Game1.BLimit + 50), Color.White);
        }

        private void DrawLifeStats()
        {
            int start = 100;
            int end = 60;


            spriteBatch.Draw(Healt, new Rectangle(start - 30, Game1.BLimit + 25, 20, 20), Color.Red);
            spriteBatch.Draw(dummyTexture,
                             new Rectangle(start, Game1.BLimit + 25,
                                           (Game1.SWidth / 2) - start - end, 20), Color.Red);
            double lifelength = (((Game1.SWidth / 2) - start - end) / 200.0) * GameController.Player.Healt;
            spriteBatch.Draw(dummyTexture, new Rectangle(start, Game1.BLimit + 25, (int)lifelength, 20),
                             new Color(7, 237, 15));

            spriteBatch.Draw(Shield, new Rectangle(start - 30, Game1.BLimit + 55, 20, 20), Color.Gray);
            spriteBatch.Draw(dummyTexture,
                             new Rectangle(start, Game1.BLimit + 55,
                                           (Game1.SWidth / 2) - start - end, 20), Color.Red);
            double shields = (((Game1.SWidth / 2) - start - end) / 200.0) * GameController.Player.Shield;
            spriteBatch.Draw(dummyTexture, new Rectangle(start, Game1.BLimit + 55, (int)shields, 20),
                             Color.Blue);

            spriteBatch.Draw(Boost, new Rectangle(start - 60, Game1.BLimit + 85, 50, 20),
                             Color.LightGoldenrodYellow);
            spriteBatch.Draw(dummyTexture,
                             new Rectangle(start, Game1.BLimit + 85,
                                           (Game1.SWidth / 2) - start - end, 20), Color.Red);
            double booster = (((Game1.SWidth / 2) - start - end) / 200.0) * GameController.Player.Booster;
            spriteBatch.Draw(dummyTexture, new Rectangle(start, Game1.BLimit + 85, (int)booster, 20),
                             Color.DarkOrange);
        }

        private void DrawWeaponStats()
        {
            int spacing = (Game1.SWidth - (20 * 7)) / GameController.NumberOfWeapons;

            spriteBatch.DrawString(MediumNumber, "Bullets", new Vector2((spacing / 2) - 40, Game1.TopLimit - 80),
                                   Color.White);
            spriteBatch.DrawString(MediumNumber, "" + gameController.AmmoCounter[0],
                                   new Vector2((spacing / 2) - 20, Game1.TopLimit - 60),
                                   (gameController.ChoosenWeapon == 1) ? Color.Red : Color.White);

            spriteBatch.DrawString(MediumNumber, "Spreader",
                                   new Vector2((spacing + (spacing / 2)) - 20, Game1.TopLimit - 80), Color.White);
            spriteBatch.DrawString(MediumNumber, "" + gameController.AmmoCounter[1],
                                   new Vector2((spacing + (spacing / 2)), Game1.TopLimit - 60),
                                   (gameController.ChoosenWeapon == 2) ? Color.Red : Color.White);

            spriteBatch.DrawString(MediumNumber, "Rockets",
                                   new Vector2(((spacing * 2) + (spacing / 2)) - 20, Game1.TopLimit - 80),
                                   Color.White);
            spriteBatch.DrawString(MediumNumber, "" + gameController.AmmoCounter[2],
                                   new Vector2(((spacing * 2) + (spacing / 2)) + 20, Game1.TopLimit - 60),
                                   (gameController.ChoosenWeapon == 3) ? Color.Red : Color.White);

            spriteBatch.DrawString(MediumNumber, "Whirl",
                                   new Vector2(((spacing * 3) + (spacing / 2)), Game1.TopLimit - 80), Color.White);
            spriteBatch.DrawString(MediumNumber, "" + gameController.AmmoCounter[3],
                                   new Vector2(((spacing * 3) + (spacing / 2)) + 20, Game1.TopLimit - 60),
                                   (gameController.ChoosenWeapon == 4) ? Color.Red : Color.White);

            spriteBatch.DrawString(MediumNumber, "Bomb",
                                   new Vector2(((spacing * 4) + (spacing / 2)) + 20, Game1.TopLimit - 80),
                                   Color.White);
            spriteBatch.DrawString(MediumNumber, "" + gameController.AmmoCounter[4],
                                   new Vector2(((spacing * 4) + (spacing / 2)) + 40, Game1.TopLimit - 60),
                                   (gameController.ChoosenWeapon == 5) ? Color.Red : Color.White);

            spriteBatch.DrawString(MediumNumber, "Laser",
                                   new Vector2(((spacing * 5) + (spacing / 2)) + 40, Game1.TopLimit - 80),
                                   Color.White);
            spriteBatch.DrawString(MediumNumber, "" + gameController.AmmoCounter[5],
                                   new Vector2(((spacing * 5) + (spacing / 2)) + 60, Game1.TopLimit - 60),
                                   (gameController.ChoosenWeapon == 6) ? Color.Red : Color.White);
        }

        private void DrawWireFrameBottom()
        {
            spriteBatch.Draw(dummyTexture,
                             new Rectangle(0, Game1.BLimit, Game1.SWidth,
                                           Game1.SHeight - Game1.BLimit), Color.Black);
            spriteBatch.Draw(Wall, new Rectangle(0, Game1.BLimit, Game1.SWidth, 20),
                             Color.LightGray);

            int spacing = (Game1.SWidth - (20 * 7)) / GameController.NumberOfWeapons;
            for (int i = (Game1.SWidth / 2) - 20, j = 0; j < 3; i = i + 20 + spacing, j++)
            {
                spriteBatch.Draw(Wall,
                                 new Rectangle(i, Game1.BLimit, 20,
                                               Game1.SHeight - Game1.BLimit), Color.LightGray);
            }

            spriteBatch.Draw(Wall,
                             new Rectangle(Game1.SWidth - 20, Game1.BLimit, 20,
                                           Game1.SHeight - Game1.BLimit), Color.LightGray);
            spriteBatch.Draw(Wall,
                             new Rectangle(0, Game1.BLimit, 20,
                                           Game1.SHeight - Game1.BLimit), Color.LightGray);
        }

        private void DrawWireFrameTop()
        {
            spriteBatch.Draw(dummyTexture, new Rectangle(0, 0, Game1.SWidth, Game1.TopLimit),
                             Color.Black);
            spriteBatch.Draw(Wall, new Rectangle(0, 80, Game1.SWidth, 20), Color.LightGray);
            spriteBatch.Draw(Wall, new Rectangle(0, 0, Game1.SWidth, 20), Color.LightGray);

            int spacing = (Game1.SWidth - (20 * 7)) / GameController.NumberOfWeapons;
            for (int i = 20 + spacing, j = 0; j < GameController.NumberOfWeapons - 1; i = i + 20 + spacing, j++)
            {
                spriteBatch.Draw(Wall, new Rectangle(i, 0, 20, Game1.TopLimit), Color.LightGray);
            }


            spriteBatch.Draw(Wall, new Rectangle(Game1.SWidth - 20, 0, 20, Game1.TopLimit),
                             Color.LightGray);
            spriteBatch.Draw(Wall, new Rectangle(0, 0, 20, Game1.TopLimit), Color.LightGray);
        }
        internal void DrawFramesAndStats()
        {
            DrawWireFrameTop();

            DrawWireFrameBottom();

            DrawWeaponStats();

            DrawLifeStats();

            DrawGameStats();
        }
        #endregion

        #region bullets
        public void DrawBullets()
        {
            List<PlayerWeapon> bullets = gameController.GetAllBullets();

            foreach (PlayerWeapon playerWeapon in bullets)
            {
                if (playerWeapon is Bullet || playerWeapon is Spreader)
                {
                    spriteBatch.Draw(Bullet,
                                     new Rectangle(playerWeapon.X, playerWeapon.Y, playerWeapon.RadiusX,
                                                   playerWeapon.RadiusY), Color.White);
                }
                else if (playerWeapon is Rocket)
                {
                   //spriteBatch.Draw(dummyTexture,
                   //              new Rectangle(playerWeapon.X, playerWeapon.Y, playerWeapon.RadiusX,
                   //                            playerWeapon.RadiusY), Color.Green);
                    Vector2 v = new Vector2();
                    //if (((Rocket) playerWeapon).Angle > Math.PI/2)
                    //{
                    //    v = new Vector2(0, 0);
                    //}
                    //else
                    //{
                      //  v = new Vector2(-10,7);
                   // }
                    
                    v = Rotate(((Rocket) playerWeapon).Angle, 0,
                               new Vector2(playerWeapon.RadiusX/2,
                                           playerWeapon.RadiusY/2));
                    
                     spriteBatch.Draw(
                         RocketTexture, 
                         ((Rocket)playerWeapon).Position,
                         null, 
                         Color.White,  
                         ((Rocket)playerWeapon).Angle,
                          v, 

                         //new Vector2(
                         //    playerWeapon.RadiusX / 2, 
                         //    playerWeapon.RadiusY / 2), 
                         0.5f, 
                         SpriteEffects.None, 
                         1.0f);

                }else if (playerWeapon is Whirl && ((Whirl)playerWeapon).i == 0)
                {

                   
                    spriteBatch.Draw(
                        SwirlTexture,
                        ((Whirl)playerWeapon).Position,
                        null,
                        Color.White,
                        ((Whirl)playerWeapon).Angle,
                         new Vector2(
                            playerWeapon.RadiusX / 2, 
                            playerWeapon.RadiusY / 2), 
                        1.0f,
                        SpriteEffects.None,
                        1.0f);
                    
                }
                else if (playerWeapon is Laser)
                {
                    spriteBatch.Draw(dummyTexture,
                                     new Rectangle(playerWeapon.X, playerWeapon.Y,
                                                   ((Laser)playerWeapon).dest - playerWeapon.X, 2), Color.Red);
                }
            }
        }

        private Vector2 Rotate(float angle, float distance, Vector2 centre)
        {
            return new Vector2((float)(distance * Math.Cos(angle)), (float)(distance * Math.Sin(angle))) + centre;
        }
        #endregion

        #region StartFinnish
        internal void FinnishDrawing(TimeSpan res)
        {
            spriteBatch.DrawString(LargeNumber, "" + res.TotalMilliseconds, new Vector2(200, 250), Color.White);
            
            spriteBatch.DrawString(LargeNumber, "" + (GC.GetTotalMemory(false) / 1000) , new Vector2(200, 200), Color.White);
            spriteBatch.End();
        }

        internal Boolean GetReady()
        {
            if (spriteBatch != null)
            {
                spriteBatch.Begin();
                return true;
            }
            return false;
        }
        #endregion

        #region Player
        internal void DrawPlayer()
        {
            // spriteBatch.Draw(dummyTexture, new Rectangle(player.X, player.Y, player.Height, player.Width), Color.Gray);
            if (GameController.Player.boost != 0 && GameController.Player.Booster > 0)
            {
                DrawPlayerBigFlames();
            }
            else
            {
                DrawPlayerShortFlames();
            }
            spriteBatch.Draw(ship,
                             new Rectangle(GameController.Player.X, GameController.Player.Y, GameController.Player.Width, GameController.Player.Height),
                             Color.White);
            FlameCounter++;
            if (FlameCounter == 10)
            {
                FlameCounter = 0;
            }

            DrawPlayerShield();
        }

        private void DrawPlayerBigFlames()
        {
            if (FlameCounter < 5)
            {
                spriteBatch.Draw(LongFlame,
                                 new Rectangle(GameController.Player.X - LongFlame.Width + 10, GameController.Player.Y + 35,
                                               LongFlame.Width, LongFlame.Height), Color.White);
                spriteBatch.Draw(LongFlame,
                                 new Rectangle(GameController.Player.X - LongFlame.Width + 10, GameController.Player.Y + 3,
                                               LongFlame.Width, LongFlame.Height), Color.White);
            }
            else
            {
                spriteBatch.Draw(LongFlame2,
                                 new Rectangle(GameController.Player.X - LongFlame.Width + 10, GameController.Player.Y + 35,
                                               LongFlame.Width, LongFlame.Height), Color.White);
                spriteBatch.Draw(LongFlame2,
                                 new Rectangle(GameController.Player.X - LongFlame.Width + 10, GameController.Player.Y + 3,
                                               LongFlame.Width, LongFlame.Height), Color.White);
            }
        }

        private void DrawPlayerShortFlames()
        {
            if (FlameCounter < 5)
            {
                spriteBatch.Draw(ShortFlame,
                                 new Rectangle(GameController.Player.X - ShortFlame.Width + 10, GameController.Player.Y + 35,
                                               ShortFlame.Width, ShortFlame.Height), Color.White);
                spriteBatch.Draw(ShortFlame,
                                 new Rectangle(GameController.Player.X - ShortFlame.Width + 10, GameController.Player.Y + 10,
                                               ShortFlame.Width, ShortFlame.Height), Color.White);
            }
            else
            {
                spriteBatch.Draw(ShortFlame2,
                                 new Rectangle(GameController.Player.X - ShortFlame.Width + 10, GameController.Player.Y + 35,
                                               ShortFlame.Width, ShortFlame.Height), Color.White);
                spriteBatch.Draw(ShortFlame2,
                                 new Rectangle(GameController.Player.X - ShortFlame.Width + 10, GameController.Player.Y + 10,
                                               ShortFlame.Width, ShortFlame.Height), Color.White);
            }
        }

        private void DrawPlayerShield()
        {
            if (GameController.Player.ShieldOn && GameController.Player.Shield > 0)
            {
                ShieldBlink++;
                if (GameController.Player.Shield < 30 && ShieldBlink < 5)
                {
                    return;
                }
                spriteBatch.Draw(PShield,
                                 new Rectangle(GameController.Player.X - 20, GameController.Player.Y - 20,
                                               GameController.Player.Width + 50, GameController.Player.Height + 40),
                                 Color.White);
                if (ShieldBlink == 11)
                {
                    ShieldBlink = 0;
                }
            }
        }

        #endregion

        #region Enemies
        internal void DrawEnemies()
        {
            foreach (Enemy enemy in gameController.EnemyList)
            {
                if (enemy.Boss)
                {
                    DrawBoss(enemy);
                }
                else
                {
                    DrawNormalEnemy(enemy);
                }
            }
        }

        private void DrawBoss(Enemy enemy)
        {
            spriteBatch.Draw(dummyTexture, new Rectangle(100, Game1.TopLimit + 40, Game1.SWidth - 200, 30),
                             Color.Red);
            double lifelength = ((Game1.SWidth - 200) / (double)enemy.MaxHealt) * enemy.Healt;
            spriteBatch.Draw(dummyTexture, new Rectangle(100, Game1.TopLimit + 40, (int)lifelength, 30),
                             new Color(7, 237, 15));

            spriteBatch.Draw(SmallBossT, new Rectangle(enemy.X, enemy.Y, enemy.Width, enemy.Height),
                             Color.White);
        }

        private void DrawNormalEnemy(Enemy enemy)
        {
            if (enemy.Type != 1)
            {

                spriteBatch.Draw(dummyTexture, new Rectangle(enemy.X, enemy.Y - 10, enemy.Width, 3), Color.Red);
                double lifelength = (enemy.Width / (double)enemy.MaxHealt) * enemy.Healt;
                spriteBatch.Draw(dummyTexture, new Rectangle(enemy.X, enemy.Y - 10, (int)lifelength, 3),
                                 new Color(7, 237, 15));
            }
            if (enemy.Type == 1)
            {
                var origin = new Vector2();
                origin.X = EnemyComet.Width / (float)2;
                origin.Y = EnemyComet.Height / (float)2;
                var screePos = new Vector2();
                screePos.X = enemy.X + (enemy.Width / 2);
                screePos.Y = enemy.Y + (enemy.Height / 2);
                var nonUniformScale = new Vector2();
                nonUniformScale.X = enemy.Width / (float)EnemyComet.Width;
                nonUniformScale.Y = enemy.Height / (float)EnemyComet.Height;
                //  spriteBatch.Draw(dummyTexture, new Rectangle(enemy.X, enemy.Y, enemy.Width, enemy.Height), Color.White);
                spriteBatch.Draw(EnemyComet, screePos, null, Color.White, (float)((Comet)enemy).rotate, origin,
                                 nonUniformScale, SpriteEffects.None, 0f);
            }

            if (enemy.Type == 2)
            {
                spriteBatch.Draw(EnemySentry, new Rectangle(enemy.X, enemy.Y, enemy.Width, enemy.Height),
                                 Color.White);
            }

            if (enemy.Type == 3)
            {
                spriteBatch.Draw(CargoCrateT, new Rectangle(enemy.X, enemy.Y, enemy.Width, enemy.Height),
                                 Color.White);
            }

            if (enemy.Type == 4)
            {
               

                Vector2 realPos = new Vector2(((Striker)enemy).pos.X + enemy.Width / 2, ((Striker)enemy).pos.Y + enemy.Height / 2);
                spriteBatch.Draw(
                       EnemyStriker,
                       realPos,
                       null,
                       Color.White,
                       ((Striker)enemy).Angle,
                      
                      new Vector2(
                           enemy.Width / 2,
                           enemy.Height / 2),
                       1.0f,
                       SpriteEffects.None,
                       1.0f);
            }
            if (enemy.Type == 5)
            {
                spriteBatch.Draw(EnemyBomber, new Rectangle(enemy.X, enemy.Y, enemy.Width, enemy.Height),
                                 Color.White);
            }

        }


        public void DrawEnemyBullets()
        {
            foreach (var enemyWeaponse in gameController.EnemyBullets)
            {

                if (enemyWeaponse is DirectionalBullet)
                {
                    
                    Vector2 realPos = new Vector2(enemyWeaponse.X + enemyWeaponse.RadiusX/2,
                                                  enemyWeaponse.Y + enemyWeaponse.RadiusY/2);
                    spriteBatch.Draw(
                        Bullet,
                        realPos,
                        null,
                        Color.OrangeRed,
                        ((DirectionalBullet) enemyWeaponse).Angle,
                        new Vector2(
                            enemyWeaponse.RadiusX/2,
                            enemyWeaponse.RadiusY/2),
                        0.3f,
                        SpriteEffects.None,
                        1.0f);
                }
                if (enemyWeaponse is EBullet)
                {
                    spriteBatch.Draw(Bullet, new Rectangle(enemyWeaponse.X, enemyWeaponse.Y, enemyWeaponse.RadiusX, enemyWeaponse.RadiusY),
                                 Color.Orange);
                }
                if (enemyWeaponse is EBomb)
                {
                    if (((EBomb) enemyWeaponse).flash > 10)
                    {
                        spriteBatch.Draw(EBomb,
                                         new Rectangle(enemyWeaponse.X, enemyWeaponse.Y, enemyWeaponse.RadiusX,
                                                       enemyWeaponse.RadiusY),
                                         Color.Red);
                    }
                    else
                    {
                        spriteBatch.Draw(EBomb,
                                         new Rectangle(enemyWeaponse.X, enemyWeaponse.Y, enemyWeaponse.RadiusX,
                                                       enemyWeaponse.RadiusY),
                                         Color.White);
                    }
                }

            }
        }

        #endregion

      
    }
}