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

        private static int resCounter = Game1.Resulotions.Count-1;
        private static string sfxText = "SFX Volume: ";
        private static int sfxLevel = 10;
        private static string resulo = "Resolution ";
        MenuEntry MusicLevel = new MenuEntry(music + level);
        MenuEntry sfx = new MenuEntry(sfxText + sfxLevel);



        MenuEntry res = new MenuEntry(resulo + Game1.Resulotions[resCounter]);
        MenuEntry fullscreen = new MenuEntry("Fullscreen");

        MenuEntry Exit = new MenuEntry("Return to menu");

        public SettingsScreen(string menuTitle)
            : base(menuTitle)
        {
            MusicLevel.Selected += MusicSelected;
            sfx.Selected += SfxSelected;

            fullscreen.Selected += SelectFullScreen;
            res.Selected += changeRes;
            Exit.Selected += OnCancel;
            MenuEntries.Add(MusicLevel);

            MenuEntries.Add(sfx);

            MenuEntries.Add(res);
            MenuEntries.Add(fullscreen);

            MenuEntries.Add(Exit);


        }

        private void changeRes(object sender, PlayerIndexEventArgs e)
        {
#if XBOX
            
            resCounter++;
            if (resCounter >= 3)
            {
                resCounter = 0;
            }
            if(resCounter == 0){
                res.Text = resulo + "640×480";
            }else if(resCount == 1){
                res.Text = resulo + "1280×720";
            }else{
                res.Text = resulo + "1920×1080";
            }
            
            Game1.SetResulotion(resCounter);
#else
            resCounter++;
            if (resCounter == Game1.Resulotions.Count)
            {
                resCounter = 0;
            }
            res.Text = resulo + Game1.Resulotions[resCounter];
            Game1.SetResulotion(resCounter);
#endif
        }

        private void SelectFullScreen(object sender, PlayerIndexEventArgs e)
        {
            Game1.WindowScreen();
            fullscreen.Text = "Windowed Mode";
            fullscreen.Selected -= SelectFullScreen;
            fullscreen.Selected += SelectWindowed;
        }

        private void SelectWindowed(object sender, PlayerIndexEventArgs e)
        {
            Game1.FullScreen();
            fullscreen.Text = "Fullscreen";
            fullscreen.Selected += SelectFullScreen;
            fullscreen.Selected -= SelectWindowed;
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
