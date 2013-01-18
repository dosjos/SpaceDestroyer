using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using SpaceDestroyer.Enemies;
using SpaceDestroyer.GameData;
using SpaceDestroyer.Screens;
using SpaceDestroyer.Weapons;

namespace SpaceDestroyer.Controllers
{
    internal class LevelController
    {
        // private readonly PlayerOne Player;
        private readonly List<Enemy> _enemyList;
        private readonly Random rand;
        private DateTime LastAdded;
        public List<Level> levels;

        public LevelController(List<EnemyWeapons> enemyWeapons, List<Enemy> enemyList)
        {
            _enemyList = enemyList;
            EnemyWeapons = enemyWeapons;
            levels = new List<Level>();

            ReadLevelFile();
            rand = new Random();
        }

        
        public List<EnemyWeapons> EnemyWeapons { get; set; }
        

        public int Level { get; set; }
        public Boolean Bossfight { get; set; }

        private void ReadLevelFile()
        {
            //Evt fiks et eget prosjekt for de to klassene og referer til de fra begge prosjektetne og bruk pipelinen til å hente inn dataene
            Stream stream = TitleContainer.OpenStream("Content\\leveldata.xml");
            XDocument doc = XDocument.Load(stream);
            levels = (from level in doc.Descendants("Level")
                      select new Level
                          {
                              LevelNumber = Convert.ToInt32(level.Element("LevelNumber").Value),
                              InfoTexts = (from infoText in level.Descendants("InfoText")
                                           select new InfoText
                                               {
                                                   Info = Convert.ToString(infoText.Element("Info").Value),
                                                   Info2 = Convert.ToString(infoText.Element("Info2").Value)
                                               }).ToList(),
                              Enemies = (from enemy in level.Descendants("EnemyData")
                                         select new EnemyData
                                             {
                                                 EnemyNumber = Convert.ToInt32(enemy.Element("Type").Value),
                                                 Health = Convert.ToInt32(enemy.Element("Healt").Value),
                                                 Score = Convert.ToInt32(enemy.Element("Score").Value),
                                                 SpawnRate = Convert.ToInt32(enemy.Element("Spawnrate").Value),
                                                 Droprate = Convert.ToInt32(enemy.Element("Droprate").Value),
                                                 Boss = Convert.ToBoolean(enemy.Element("Boss").Value),
                                                 CrashDamage = Convert.ToInt32(enemy.Element("CrashDamage").Value)
                                             }).ToList()
                          }).ToList();
        }

        internal Enemy AddEnemy()
        {
            //TODO add sjekk for å hoppe ut når alle levler er nådd
            List<EnemyData> potentialEnemies = levels[Level - 1].Enemies;
            foreach (EnemyData p in potentialEnemies)
            {
                if (p.Boss)
                {
                    if (p.EnemyNumber == 1)
                    {
                        return new SmallBoss(p.Health, p.Score, EnemyWeapons,
                                             rand, p.CrashDamage, _enemyList);
                    }
                }
                else
                {
                    var lastSpawn = (long) (DateTime.Now - LastAdded).TotalMilliseconds;
                    if (p.LastSpawn + p.SpawnRate < lastSpawn)
                    {
                        p.LastSpawn = lastSpawn;
                        if (p.EnemyNumber == 1)
                        {
                            return new Comet(p.Health, p.Score, p.Droprate, rand,
                                             p.CrashDamage);
                        }

                        if (p.EnemyNumber == 2)
                        {
                            return new Sentry(p.Health, p.Score,  EnemyWeapons,
                                              p.Droprate,
                                               rand, p.CrashDamage);
                        }
                        if (p.EnemyNumber == 3)
                        {
                            return new CargoCrate(p.Health, p.Score, EnemyWeapons,
                                                  p.Droprate,
                                                   rand, p.CrashDamage);
                        }
                    }
                }
            }
            return null;
        }

        internal bool HasText()
        {
            return (levels[Level-1] != null && levels[Level - 1].InfoTexts.Count > 0 && !levels[Level - 1].InfoTexts[0].Seen) ? true : false;
        }

        internal bool TextIsSeen()
        {
            return levels[Level - 1].InfoTexts[0].Seen;
        }

        internal FloatingText GetText()
        {
            levels[Level - 1].InfoTexts[0].Seen = true;
            return new FloatingInfoText(levels[Level - 1].InfoTexts[0].Info, levels[Level - 1].InfoTexts[0].Info2,
                                        200);
        }
    }
}