using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SpaceDestroyer.Controllers;

namespace SpaceDestroyer.Screens
{
    class SettingsScreen : MenuScreen
    {
        private static int level = 10;
        private static string music = "Music volume: ";


        private static string sfxText = "SFX Volume: ";
        private static int sfxLevel = 10;

        MenuEntry MusicLevel = new MenuEntry(music + level);
        MenuEntry sfx = new MenuEntry(sfxText + sfxLevel);

        MenuEntry Exit = new MenuEntry("Return to menu");

        public SettingsScreen(string menuTitle)
            : base(menuTitle)
        {
            MusicLevel.Selected += MusicSelected;
            sfx.Selected += SfxSelected;

            Exit.Selected += OnCancel;
            MenuEntries.Add(MusicLevel);
          
            MenuEntries.Add(sfx);
            MenuEntries.Add(Exit);


        }

        private void SfxSelected(object sender, PlayerIndexEventArgs e)
        {
            sfxLevel++;
            if (sfxLevel == 11)
            {
                sfxLevel = 0;
            }
            sfx.Text = sfxText + sfxLevel;
            SoundController.AdjustSfx(sfxLevel);
        }

        private void MusicSelected(object sender, PlayerIndexEventArgs e)
        {
            level++;
            if (level == 11)
            {
                level = 0;
            }
            MusicLevel.Text = music + level;
            SoundController.AdjustSongVolume(level);
        }

        

        protected override void OnCancel(Microsoft.Xna.Framework.PlayerIndex playerIndex)
        {
            LoadingScreen.Load(ScreenManager, false, playerIndex, new BackgroundScreen(), new MainMenuScreen());
        }
    }
}
