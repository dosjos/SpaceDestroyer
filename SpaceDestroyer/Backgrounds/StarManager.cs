using System;
using System.Collections.Generic;

namespace SpaceDestroyer.Backgrounds
{
    public class StarManager
    {
        private readonly int BottomLimit;
        private readonly int TopLimit;
        private readonly int Width;
        private readonly Random rand;
        private readonly List<Star> stars = new List<Star>();
        private int _counter;

        public StarManager()
        {
            rand = new Random();
            BottomLimit = Game1.BLimit;
            Width = Game1.SWidth;
            TopLimit = Game1.TopLimit;
        }

        public void Calculate()
        {
            int placement = rand.Next(TopLimit, BottomLimit);

            if (_counter%2 == 0)
            {
                stars.Add(new Star(2, 2, Width, placement));
            }

            if (_counter%4 == 0)
            {
                stars.Add(new Star(3, 3, Width, placement));
            }
            if (_counter%8 == 0)
            {
                stars.Add(new Star(5, 5, Width, placement));
            }
            _counter++;
            RemoveDeadStars();
            MoveStars();
        }

        private void MoveStars()
        {
            stars.ForEach(star => star.Calculate());
        }

        private void RemoveDeadStars()
        {
            for (int i = 0; i < stars.Count; i++)
            {
                if (stars[i].X < -10)
                {
                    stars.RemoveAt(i);
                    i--;
                }
            }
        }

        internal List<Star> GetAllStars()
        {
            return stars;
        }
    }
}