using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SpaceDestroyer.Controllers;
using SpaceDestroyer.GameData;
using SpaceDestroyer.Player;
using SpaceDestroyer.ScreenManagers;
using SpaceDestroyer.Screens;

namespace SpaceDestroyer
{
    /// <summary>
    ///     This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        public static int SHeight;
        public static int SWidth;
        public static int TopLimit;
        public static int BLimit;
        public static GraphicsDeviceManager _graphics;
        public ScreenManager screenManager { get; set; }
        //public static PlayerOne Player;
        
        public Vector2 StartPos;

       // 

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromMilliseconds(20); // 20 milliseconds, or 50 FPS.

            SoundController.SetContetn(Content);
            screenManager = new ScreenManager(this);

            Components.Add(screenManager);
            Components.Add(new MessageDisplayComponent(this));
            Components.Add(new GamerServicesComponent(this));
            screenManager.AddScreen(new BackgroundScreen(), null);
            screenManager.AddScreen(new MainMenuScreen(), null);
            Console.WriteLine(DateTime.Now);
        }

        

       
        protected override void Initialize()
        {
#if XBOX
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
#else
            _graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();
#endif

            SHeight = _graphics.PreferredBackBufferHeight;
            SWidth = _graphics.PreferredBackBufferWidth;
            TopLimit = 100;
            BLimit = _graphics.PreferredBackBufferHeight - 120;



            base.Initialize();
        }

        /// <summary>
        ///     LoadContent will be called once per game and is the place to load
        ///     all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            Console.WriteLine("I load contetnt");
            //
        }

        /// <summary>
        ///     UnloadContent will be called once per game and is the place to unload
        ///     all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        ///     Allows the game to run logic such as updating the world,
        ///     checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }


        /// <summary>
        ///     This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            _graphics.GraphicsDevice.Clear(Color.Black);
            
            base.Draw(gameTime);
        }


        private void InGameLogic(GameTime gameTime, KeyboardState newState)
        {
            //if (GameController.CurrentState == GameController.State.InGame)
            //{
            //    if (Keyboard.GetState().IsKeyDown(Keys.Space) || GamePad.GetState(PlayerIndex.One).Triggers.Right > 0.1)
            //    {
            //        GameController.AddBullet(Player);
            //    }

            //    if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) ||
            //        GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed)
            //    {
            //        Player.Boost(true);
            //    }
            //    else
            //    {
            //        Player.Boost(false);
            //    }
            //    if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.A) ||
            //        GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X < 0)
            //        Player.GoLeft();
            //    if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D) ||
            //        GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X > 0)
            //        Player.GoRight();
            //    if (Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.W) ||
            //        GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y > 0)
            //        Player.GoUp();
            //    if (Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.S) ||
            //        GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y < 0)
            //        Player.GoDown();

            //    if (Keyboard.GetState().IsKeyDown(Keys.LeftControl) && !_oldState.IsKeyDown(Keys.LeftControl) ||
            //        GamePad.GetState(PlayerIndex.One).Buttons.Y == ButtonState.Pressed)
            //        Player.ShieldOn = !Player.ShieldOn;

            //    if (newState.IsKeyDown(Keys.E) && !_oldState.IsKeyDown(Keys.E) ||
            //        GamePad.GetState(PlayerIndex.One).Buttons.RightShoulder == ButtonState.Pressed)
            //        GameController.WeaponUp();
            //    if (newState.IsKeyDown(Keys.Q) && !_oldState.IsKeyDown(Keys.Q) ||
            //        GamePad.GetState(PlayerIndex.One).Buttons.LeftShoulder == ButtonState.Pressed)
            //    {
            //        GameController.WeaponDown();
            //    }
            //    if (Keyboard.GetState().IsKeyDown(Keys.D1)) GameController.SetChoosenWeapon(1);
            //    if (Keyboard.GetState().IsKeyDown(Keys.D2)) GameController.SetChoosenWeapon(2);
            //    if (Keyboard.GetState().IsKeyDown(Keys.D3)) GameController.SetChoosenWeapon(3);
            //    if (Keyboard.GetState().IsKeyDown(Keys.D4)) GameController.SetChoosenWeapon(4);
            //    if (Keyboard.GetState().IsKeyDown(Keys.D5)) GameController.SetChoosenWeapon(5);
            //    if (Keyboard.GetState().IsKeyDown(Keys.D6)) GameController.SetChoosenWeapon(6);


            //    // TODO: Add your update logic here

            //    int start = DateTime.Now.Millisecond;



            //    Boolean died = GameController.CheckIfDied();

            //    if (died && GameController.CurrentState == GameController.State.InGame)
            //    {
            //        GameController.CurrentState = GameController.State.GameOver;
            //        CheckIfScoreIsGoodEnough(GameController.Score);
            //    }

            //    int end = DateTime.Now.Millisecond;
            //    //Console.WriteLine( end-start);
            //}
        }

        private void CheckIfScoreIsGoodEnough(int score)
        {
            //Boolean good = GameController.CheckIfHighScore(score);
            //if (good)
            //{
            //    var high = new HighScore
            //        {
            //            Level = GameController.Level,
            //            Score = GameController.Score,
            //            Difficulty = "Hearsy", // get dificulty when implemented
            //            Name = "NoooooooWay" //TODO get name from xbox
            //        };
            //    GameController.AddHigh(high);
            //    HighScoreController.SaveHighScores();
            //    GameController.CurrentState = GameController.State.NewHighScore;
            //}
            //else
            //{
            //    GameController.CurrentState = GameController.State.GameOver;
            //}
        }
    }
}