namespace SpaceDestroyer.Backgrounds
{
    internal class Star
    {
        public Star(int r, int speed, int p2, int random)
        {
            Radius = r;
            Speed = speed;
            Y = random;
            X = p2 + 20;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Speed { get; set; }
        public int Radius { get; set; }

        public void Calculate()
        {
            X = X - Speed;
        }
    }
}