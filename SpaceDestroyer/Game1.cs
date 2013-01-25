using System;
using System.Collections.Generic;
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
        public static List<string> Resulotions = new List<string>();
        private static List<DisplayMode> modes = new List<DisplayMode>();

        //public static PlayerOne Player;

        public Vector2 StartPos;

        // 

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromMilliseconds(20); // 20 milliseconds, or 50 FPS.

            SoundController.SetContent(Content);
            screenManager = new ScreenManager(this);

            Components.Add(screenManager);
            Components.Add(new MessageDisplayComponent(this));
            Components.Add(new GamerServicesComponent(this));
            screenManager.AddScreen(new BackgroundScreen(), null);
            screenManager.AddScreen(new MainMenuScreen(), null);

            foreach (DisplayMode mode in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
            {
                modes.Add(mode);
                Resulotions.Add("" + mode.Width + "x" + mode.Height);
            }

        }

        internal static void SetResulotion(int resCounter)
        {

#if XBOX
            if(resCounter == 0){
            SHeight = _graphics.PreferredBackBufferHeight = 480;
            SWidth = _graphics.PreferredBackBufferWidth = 640;
            
            
            }else if(resCounter == 1){
            SHeight = _graphics.PreferredBackBufferHeight = 720;
            SWidth = _graphics.PreferredBackBufferWidth = 1280;
            }else if(resCounter == 2){
            SHeight = _graphics.PreferredBackBufferHeight = 1080;
            SWidth = _graphics.PreferredBackBufferWidth = 1920;
            }

            BLimit = _graphics.PreferredBackBufferHeight - 120;
            _graphics.ApplyChanges();
            
#else
            SHeight = _graphics.PreferredBackBufferHeight = modes[resCounter].Height;
            SWidth = _graphics.PreferredBackBufferWidth = modes[resCounter].Width;
            BLimit = _graphics.PreferredBackBufferHeight - 120;
            _graphics.ApplyChanges();
#endif
        }


        public static void FullScreen()
        {
            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();
        }

        public static void WindowScreen()
        {
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
#if XBOX
            _graphics.PreferredBackBufferWidth = 1920;// 1280;
            _graphics.PreferredBackBufferHeight = 1080;// 720;
            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();
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
    }
}