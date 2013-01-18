using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceDestroyer.Screens
{
    class PauseMenuScreen: MenuScreen
    {
        #region Fields

       
        
        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public PauseMenuScreen()
            : base("Game Paused")
        {
            // Add the Resume Game menu entry.
            MenuEntry resumeGameMenuEntry = new MenuEntry("Resume Game");
            resumeGameMenuEntry.Selected += OnCancel;
            MenuEntries.Add(resumeGameMenuEntry);

            MenuEntry quitGameMenuEntry = new MenuEntry("Quit Game");
            quitGameMenuEntry.Selected += QuitGameMenuEntrySelected;
            MenuEntries.Add(quitGameMenuEntry);
        }


        #endregion

        #region Handle Input


        /// <summary>
        /// Event handler for when the Quit Game menu entry is selected.
        /// </summary>
        void QuitGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            MessageBoxScreen confirmQuitMessageBox =
                                    new MessageBoxScreen("Are you sure you want to quit this game?");

            confirmQuitMessageBox.Accepted += ConfirmQuitMessageBoxAccepted;

            ScreenManager.AddScreen(confirmQuitMessageBox, ControllingPlayer);
        }


        /// <summary>
        /// Event handler for when the user selects ok on the "are you sure
        /// you want to quit" message box. This uses the loading screen to
        /// transition from the game back to the main menu screen.
        /// </summary>
        void ConfirmQuitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(), new MainMenuScreen());
        }


     


       

        #endregion
    }
    
}
