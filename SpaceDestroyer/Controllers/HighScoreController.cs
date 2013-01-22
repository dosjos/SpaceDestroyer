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
                var serializer = new XmlSerializer(typeof (List<HighScore>));
                list = (List<HighScore>) serializer.Deserialize(stream);
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
                var serializer = new XmlSerializer(typeof(List<HighScore>));
                serializer.Serialize(stream, GameController.HighScores);
            }
            finally
            {
                stream.Close();
            }
        }
    }
}