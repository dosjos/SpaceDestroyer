namespace SpaceDestroyer.Weapons
{
    public abstract class EnemyWeapons
    {

        public int X { get; set; }
        public int Y { get; set; }
        public int RadiusX { get; set; }
        public int RadiusY { get; set; }
        public int Power { get; set; }
        public int Speed { get; set; }

        public abstract void Calculate();
    }
}