using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace SpaceDestroyer.Controllers
{
    class SoundController
    {
        private static Song _bgsong;
        private static SoundEffect bulletSound;
        private static SoundEffect exposionSound;
        public static SoundEffect spreaderSound { get; set; }
        private static ContentManager Content;

        internal static void StartBackgroundMusic()
        {
            MediaPlayer.Play(_bgsong);
            MediaPlayer.IsRepeating = true;
            while (MediaPlayer.State != MediaState.Playing)
            {
                
            }
        }

        internal static void ResumeBackgroundMusic()
        {
            if (Content == null) return;
            if (MediaPlayer.State == MediaState.Playing) return;

            if (MediaPlayer.State == MediaState.Paused)
            {
                MediaPlayer.Resume();
            }
            else
            {
                MediaPlayer.Play(_bgsong);
                MediaPlayer.IsRepeating = true;
            }
        }

        public static void AdjustSongVolume(int level)
        {
            MediaPlayer.Volume = level/10.0f;
        }

        internal static void SetContent(ContentManager Contents)
        {
            Content = Contents;
            _bgsong = Content.Load<Song>("Music/bg");
            bulletSound = Content.Load<SoundEffect>("Sound/Bullet");
            exposionSound = Content.Load<SoundEffect>("Sound/explode");
            spreaderSound = Content.Load<SoundEffect>("Sound/spreader3");
        }

        internal static void PauseBackground()
        {
            if (MediaPlayer.State == MediaState.Paused) return;
            MediaPlayer.Pause();
        }

        internal static void PlayBulletSound()
        {
            bulletSound.Play();
        }

        internal static void PlaySpreaderSound()
        {
            spreaderSound.Play();
        }

        internal static void PlayExplosion()
        {
            exposionSound.Play();
        }



        internal static void AdjustSfx(int sfxLevel)
        {
            SoundEffect.MasterVolume = sfxLevel/10.0f;
        }
    }
}
