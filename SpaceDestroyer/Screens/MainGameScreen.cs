using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private GamePadState oldPadState;
        private DateTime start, startU, stopU;
        private new bool IsActive
        {
            get
            {
                return base.IsActive;
            }
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

            //TODO hvis xbox last opp en ovenforliggende skjer med kun et alternativ, nemlig ready. Legg ved bilde av kontrollerne
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
                GameController.Player.GoLeft(1.0);

            if (keyboardState.IsKeyDown(Keys.Right))
                GameController.Player.GoRight(1.0);

            if (keyboardState.IsKeyDown(Keys.Up))
                GameController.Player.GoUp(1.0);

            if (keyboardState.IsKeyDown(Keys.Down))
                GameController.Player.GoDown(1.0);

             if (keyboardState.IsKeyDown(Keys.Space) || gamePadState.Triggers.Right > 0.1)
                 gameController.AddBullet();

            if (keyboardState.IsKeyDown(Keys.Q) && !oldState.IsKeyDown(Keys.Q)
                || gamePadState.Buttons.LeftShoulder == ButtonState.Pressed && oldPadState.Buttons.LeftShoulder != ButtonState.Pressed)
            {
                gameController.WeaponDown();
            }
            if (keyboardState.IsKeyDown(Keys.E) && !oldState.IsKeyDown(Keys.E)
                || gamePadState.Buttons.RightShoulder == ButtonState.Pressed && oldPadState.Buttons.RightShoulder != ButtonState.Pressed)
            {
                gameController.WeaponUp();
            }
            if (keyboardState.IsKeyDown(Keys.LeftControl) && !oldState.IsKeyDown(Keys.LeftControl)
                || gamePadState.Buttons.Y == ButtonState.Pressed && oldPadState.Buttons.Y != ButtonState.Pressed)
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
            if (thumbstick.X < -0.1)
            {
                GameController.Player.GoLeft(1.0);//thumbstick.X);
            }
             if (thumbstick.X > 0.1)
            {
                GameController.Player.GoRight(1.0);//thumbstick.X);
            }
            if (thumbstick.Y < -0.1)
            {
                GameController.Player.GoDown(1.0);//thumbstick.Y);   
            }
             if (thumbstick.Y > 0.1)
            {
                GameController.Player.GoUp(1.0);//thumbstick.Y);
            }

        //     System.Diagnostics.Debug.WriteLine(thumbstick.X);
            oldState = keyboardState;
            oldPadState = gamePadState;
            return true;
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            

            // Gradually fade in or out depending on whether we are covered by the pause screen.
            if (coveredByOtherScreen)
                pauseAlpha = Math.Min(pauseAlpha + 1f / 32, 1);
            else
                pauseAlpha = Math.Max(pauseAlpha - 1f / 32, 0);
           
            
            if (IsActive && !paused)
            {
                startU = DateTime.Now;
                SoundController.ResumeBackgroundMusic();
                stopU = DateTime.Now;
                gameController.CalculateBackgroundElements();
                
                var t = gameController.CheckAndUpdateLevel();
                //
                gameController.CalculateEnemyFireHit();
                gameController.CalculateTexts(gameTime);
                gameController.RemoveTexts();
                //
                gameController.RemoveEnemyBullets();
                gameController.AddEnemys();
                gameController.CalculateEnemiesMovement();
                gameController.RemoveEnemies();
                gameController.CalculateLootMovement();
                gameController.CalculateLootCollision();

                
                //
                gameController.CalculateEnemyFire();

                
                gameController.RemoveBullets();
                gameController.CalculateBullets();
                gameController.CalculateBulletsHits();

                

                gameController.CalculateColisions();
                gameController.CalculateExplosions((float) gameTime.ElapsedGameTime.TotalSeconds);
                
                if (GameController.Player.Healt <= 0 || !t)
                {
                    LoadingScreen.Load(ScreenManager, true, ControllingPlayer, new BackgroundScreen(), new GameEndedScreen(gameController.Score, gameController.Level, "Game Over"));
                }

            }
            else
            {
                SoundController.PauseBackground();
            }
            
            base.Update(gameTime, otherScreenHasFocus, false);

        }


        public override void Draw(GameTime gameTime)
        {
            start = DateTime.Now;
            base.Draw(gameTime);
            spriteController.GetReady();

            

            spriteController.DrawBackGroundElements();
            spriteController.DrawFramesAndStats();
            spriteController.DrawFloatingTexts();
            

            spriteController.DrawEnemyBullets();
            spriteController.DrawEnemies();
            spriteController.DrawLoot();
            spriteController.DrawExplosions();
            spriteController.DrawBullets();

            spriteController.DrawPlayer();

            spriteController.FinnishDrawing(stopU-startU);
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
