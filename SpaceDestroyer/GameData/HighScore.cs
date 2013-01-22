using System;

namespace SpaceDestroyer.GameData
{
    
    public class HighScore : IComparable
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public string Difficulty { get; set; }
        public int Level { get; set; }


        public int CompareTo(object obj)
        {
            if (obj is HighScore)
            {
                if (Score > ((HighScore) obj).Score)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                return -1;
            }
        }
    }
}