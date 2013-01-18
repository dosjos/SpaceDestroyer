namespace SpaceDestroyer.Weapons
{
    internal class Bullet : PlayerWeapon
    {
        public Bullet(int x, int y, int damage)
        {
            X = x - 3;
            Y = y - 3;
            Power = damage;
            Speed = 8;
            RadiusX = 7;
            RadiusY = 4;
        }

        public override void Calculate()
        {
            X += Speed;
        }
    }
}