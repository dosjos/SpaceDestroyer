using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using SpaceDestroyer.Controllers;
using SpaceDestroyer.GameData;
using SpaceDestroyer.ScreenManagers;

namespace SpaceDestroyer.Screens
{
    class HighscoreScreen : MenuScreen
    {
        private List<HighScore> HighScore;

        public HighscoreScreen() : base("High Score List")
        {
            HighScore = HighScoreController.ReadAllHighScores();
            foreach (var highScore in HighScore)
            {
                var entry = new MenuEntry("Score: " + highScore.Score + ", Level: " + highScore.Level);
                entry.Selected += OnCancel;
                entry.Selected += HighScoreMenuSelected;
                MenuEntries.Add(entry);
            }
        }

        private void HighScoreMenuSelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex, new BackgroundScreen(), new MainMenuScreen());
        }

        protected override void OnCancel(PlayerIndex playerIndex)
        {
            LoadingScreen.Load(ScreenManager, true, playerIndex, new BackgroundScreen(), new MainMenuScreen());
        }
    }
}
