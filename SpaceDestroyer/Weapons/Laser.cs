namespace SpaceDestroyer.Weapons
{
    internal class Laser : PlayerWeapon
    {
        public int dest;

        public Laser(int pX, int pY, int pH, int pW, int destX, int dmg)
        {
            X = pX + pH;
            Y = pY + (pW/2) - 3;
            dest = destX;
            Power = dmg;
            Fired = false;
        }

        public bool Fired { get; set; }

        public override void Calculate()
        {
            Fired = true;
        }
    }
}