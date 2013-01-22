using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceDestroyer.Controllers;
using SpaceDestroyer.Player;
using SpaceDestroyer.ScreenManagers;

namespace SpaceDestroyer.Screens
{
    class MainGameScreen : GameScreen
    {
        private SpriteController spriteController;
        private GameController gameController;
        float pauseAlpha;
        private KeyboardState oldState;

        private new bool IsActive
        {
            get
            {
                return base.IsActive;
            }
            set{}
        }

        public MainGameScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        public override void LoadContent()
        {
            gameController = new GameController();
            spriteController = new SpriteController(ScreenManager.SpriteBatch, gameController, ScreenManager.Game.Services, ScreenManager.GraphicsDevice);
            gameController.RegisterSpriteController(spriteController);
            GameController.Ammo = GameController.Ammos.Bullet;
            gameController.Reset();
            SoundController.StartBackgroundMusic();
            base.LoadContent();
        }


        public override void HandleInput(InputState input)
        {
            base.HandleInput(input);
            if (ControllingPlayer.HasValue)
            {
                // In single player games, handle input for the controlling player.
                HandlePlayerInput(input, ControllingPlayer.Value);
            }
        }

        private bool HandlePlayerInput(InputState input, PlayerIndex playerIndex)
        {
            KeyboardState keyboardState = input.CurrentKeyboardStates[(int)playerIndex];
            GamePadState gamePadState = input.CurrentGamePadStates[(int)playerIndex];

            bool gamePadDisconnected = !gamePadState.IsConnected &&
                                      input.GamePadWasConnected[(int)playerIndex];

            if (input.IsPauseGame(playerIndex) || gamePadDisconnected)
            {
                ScreenManager.AddScreen(new PauseMenuScreen(), playerIndex);
                return false;
            }

            if (keyboardState.IsKeyDown(Keys.Left))
                GameController.Player.GoLeft();

            if (keyboardState.IsKeyDown(Keys.Right))
                GameController.Player.GoRight();

            if (keyboardState.IsKeyDown(Keys.Up))
                GameController.Player.GoUp();

            if (keyboardState.IsKeyDown(Keys.Down))
                GameController.Player.GoDown();

             if (keyboardState.IsKeyDown(Keys.Space))
                 gameController.AddBullet();

            if (keyboardState.IsKeyDown(Keys.Q) && !oldState.IsKeyDown(Keys.Q))
            {
                gameController.WeaponDown();
            }
            if (keyboardState.IsKeyDown(Keys.E) && !oldState.IsKeyDown(Keys.E))
            {
                gameController.WeaponUp();
            }
            if (keyboardState.IsKeyDown(Keys.LeftControl) && !oldState.IsKeyDown(Keys.LeftControl))
            {
                GameController.Player.ShieldOn = !GameController.Player.ShieldOn;
            }
            if (keyboardState.IsKeyDown(Keys.D1)) gameController.SetChoosenWeapon(1);
            if (keyboardState.IsKeyDown(Keys.D2)) gameController.SetChoosenWeapon(2);
            if (keyboardState.IsKeyDown(Keys.D3)) gameController.SetChoosenWeapon(3);
            if (keyboardState.IsKeyDown(Keys.D4)) gameController.SetChoosenWeapon(4);
            if (keyboardState.IsKeyDown(Keys.D5)) gameController.SetChoosenWeapon(5);
            if (keyboardState.IsKeyDown(Keys.D6)) gameController.SetChoosenWeapon(6);


            if (keyboardState.IsKeyDown(Keys.U) && !oldState.IsKeyDown(Keys.U))
            {
                paused = !paused;
            }


            if (keyboardState.IsKeyDown(Keys.LeftShift) ||
                    gamePadState.Buttons.X == ButtonState.Pressed)
            {
                GameController.Player.Boost(true);
            }
            else
            {
                GameController.Player.Boost(false);
            }


            Vector2 thumbstick = gamePadState.ThumbSticks.Left;
            if (thumbstick.X < 0.1)
            {
                GameController.Player.GoLeft(thumbstick.X);
            }
            else if (thumbstick.X > 0.1)
            {
                GameController.Player.GoRight(thumbstick.X);
            }
            if (thumbstick.Y < 0.1)
            {
                GameController.Player.GoDown(thumbstick.Y);
            }
            else if (thumbstick.Y > 0.1)
            {
                GameController.Player.GoUp(thumbstick.Y);
            }

            oldState = keyboardState;
            return true;
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
            if (coveredByOtherScreen)
                pauseAlpha = Math.Min(pauseAlpha + 1f / 32, 1);
            else
                pauseAlpha = Math.Max(pauseAlpha - 1f / 32, 0);
            
            
            if (IsActive && !paused)
            {
                SoundController.ResumeBackgroundMusic();
                gameController.CalculateBackgroundElements();

                var t = gameController.CheckAndUpdateLevel();
                

                gameController.CalculateTexts(gameTime);
                gameController.RemoveTexts();

                gameController.AddEnemys();
                gameController.CalculateEnemiesMovement();
                gameController.RemoveEnemies();
                gameController.CalculateLootMovement();
                gameController.CalculateLootCollision();

                gameController.CalculateEnemyFire();

                gameController.RemoveEnemyBullets();
                gameController.RemoveBullets();
                gameController.CalculateBullets();
                gameController.CalculateBulletsHits();

                gameController.CalculateEnemyFireHit();

                gameController.CalculateColisions();
                gameController.CalculateExplosions((float) gameTime.ElapsedGameTime.TotalSeconds);

                if (GameController.Player.Healt <= 0 || !t)
                {
                    LoadingScreen.Load(ScreenManager, true, ControllingPlayer, new BackgroundScreen(), new GameEndedScreen(gameController.Score, gameController.Level));
                }

            }
            else
            {
                SoundController.PauseBackground();
            }

        }


        public override void Draw(GameTime gameTime)
        {

           // base.Draw(gameTime);
            spriteController.GetReady();
            
            spriteController.DrawBackGroundElements();
            spriteController.DrawFramesAndStats();
            spriteController.DrawFloatingTexts();
            spriteController.DrawPlayer();

            spriteController.DrawEnemyBullets();
            spriteController.DrawEnemies();
            spriteController.DrawLoot();
            spriteController.DrawExplosions();
            spriteController.DrawBullets();
            
            

            spriteController.FinnishDrawing();
            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0 || pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
                
            }
            
        }




        public bool paused { get; set; }
    }
}
