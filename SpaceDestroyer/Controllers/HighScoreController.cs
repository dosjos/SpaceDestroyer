using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using SpaceDestroyer.GameData;

namespace SpaceDestroyer.Controllers
{
    internal class HighScoreController
    {
        private const string Filename = "score.txt";

        public static List<HighScore> ReadAllHighScores()
        {
            var list = new List<HighScore>();
            FileStream stream = File.Open(Filename, FileMode.OpenOrCreate, FileAccess.Read);
            try
            {
                var serializer = new XmlSerializer(typeof (HighScore));
                list.Add((HighScore) serializer.Deserialize(stream));
            }
            catch (Exception e)
            {
            }
            finally
            {
                stream.Close();
            }
            return list;
        }

        public static void SaveHighScores()
        {
            FileStream stream = File.Open(Filename, FileMode.OpenOrCreate);
            try
            {
                foreach (HighScore readAllHighScore in GameController.HighScores)
                {
                    var serializer = new XmlSerializer(typeof (HighScore));
                    serializer.Serialize(stream, readAllHighScore);
                }
            }
            finally
            {
                stream.Close();
            }
        }
    }
}