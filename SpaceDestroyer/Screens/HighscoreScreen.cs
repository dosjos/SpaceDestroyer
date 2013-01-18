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
            MenuEntries.Add(new MenuEntry("Score: "));
            foreach (var highScore in HighScore)
            {
                MenuEntries.Add(new MenuEntry("Score: " + highScore.Score + ", Level: " + highScore.Level));
            }
        }

        

     
        public override void HandleInput(InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            if (ControllingPlayer.HasValue)
            {
                // In single player games, handle input for the controlling player.
                HandlePlayerInput(input, ControllingPlayer.Value);
            }
        }

        bool HandlePlayerInput(InputState input, PlayerIndex playerIndex)
        {
            // Look up inputs for the specified player profile.
            KeyboardState keyboardState = input.CurrentKeyboardStates[(int)playerIndex];
            GamePadState gamePadState = input.CurrentGamePadStates[(int)playerIndex];
            if (input.IsPauseGame(playerIndex))
            {
                ExitScreen();
                ScreenManager.AddScreen(new MainMenuScreen(), playerIndex);
                return false;
            }
            return true;
        }

    }
}
