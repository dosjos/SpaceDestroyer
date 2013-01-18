namespace SpaceDestroyer.PowerUp
{
    internal class BossLoot : Power
    {
        public BossLoot(int x, int y)
        {
            Boss = true;
            X = x;
            Y = y;
        }

        public override void Calculate()
        {
        }
    }
}