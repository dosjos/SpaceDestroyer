using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SpaceDestroyer.Controllers;
using SpaceDestroyer.GameData;

namespace SpaceDestroyer.Screens
{
    class GameEndedScreen : MenuScreen
    {
        public GameEndedScreen(int score, int level, string menuTitle = "Game Over") : base(menuTitle)
        {
            GameController.HighScores = HighScoreController.ReadAllHighScores();
            var h = new HighScore()
                {
                    Difficulty = "Easy",
                    Level = level,
                    Score = score,
                    Name = "random"
                };
            GameController.HighScores.Add(h);
            GameController.HighScores = GameController.HighScores.OrderByDescending(x => x.Score).Take(10).ToList();

            if (GameController.HighScores.Contains(h))
            {
                var newScore = new MenuEntry("Congratulation, new High Score");

                var scor = new MenuEntry("Score: " + h.Score + ", Level: " + h.Level);

                newScore.Selected += HighScoreMenuSelected;
                scor.Selected += HighScoreMenuSelected;

                newScore.Selected += OnCancel;
                scor.Selected += OnCancel;

                MenuEntries.Add(newScore);
                MenuEntries.Add(scor);

                HighScoreController.SaveHighScores();
            }
            else
            {
                var damn = new MenuEntry("Game Over - no luck for you");
                damn.Selected += HighScoreMenuSelected;
                damn.Selected += OnCancel;

                MenuEntries.Add(damn);
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
