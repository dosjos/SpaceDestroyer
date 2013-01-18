using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceDestroyer.Backgrounds;
using SpaceDestroyer.ScreenManagers;

namespace SpaceDestroyer.Screens
{
    class GameBackgroundScreen : GameScreen
    {

        private Color _bg;
        private int _blue;
        private int _green;
        private int _k;
        private int _red;
        StarManager StarManager = new StarManager(Game1.BLimit, Game1.SWidth, Game1.TopLimit);
        
        private Texture2D StarTexture;
        ContentManager content;
        private SpriteBatch spriteBatch;

        public GameBackgroundScreen()
        {
            _blue = 255;
            _green = 170;
            _k = 0;
            _red = 130;
            
        }

        public override void LoadContent()
        {
            base.LoadContent();

            spriteBatch = ScreenManager.SpriteBatch;
            if(content == null) content = new ContentManager(ScreenManager.Game.Services, "Content");
            StarTexture = content.Load<Texture2D>("Shapes/Circle");
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            ScreenManager.GraphicsDevice.Clear(_bg);
            spriteBatch.Begin();
            foreach (var allStar in StarManager.GetAllStars())
            {
                spriteBatch.Draw(StarTexture,new Rectangle(allStar.X, allStar.Y, allStar.Radius, allStar.Radius), Color.White);
            }
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            StarManager.AddStars();

            if (_blue > 0 && _k % 16 == 0)
            {
                _blue--;
                if (_green > 0)
                {
                    _red--;
                    _green--;
                    if (_red < 0)
                    {
                        _red = 0;
                    }
                }

                _bg.R = (byte)_red;
                _bg.G = (byte)_green;
                _bg.B = (byte)_blue;
            }
            _k++;
        }
    }
}
