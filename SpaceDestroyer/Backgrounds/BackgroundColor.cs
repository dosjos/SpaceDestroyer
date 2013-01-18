using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SpaceDestroyer.Backgrounds
{
    class BackgroundColor
    {
        private Color _bg = new Color();
        private int _blue;
        private int _green;
        private int _k;
        private int _red;

        public BackgroundColor()
        {
            _blue = 255;
            _green = 170;
            _k = 0;
            _red = 130;
        }

        public void Calculate()
        {
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
        public Color GetBackgroundColor()
        {
            return _bg;
        }
    }
}
