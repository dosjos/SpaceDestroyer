using SpaceDestroyer.Player;

namespace SpaceDestroyer.Weapons
{
    internal class Spreader : PlayerWeapon
    {
        private readonly int dir;
        private PlayerOne player;
        private int step;


        public Spreader(PlayerOne player, int p1, int p2)
        {
            // TODO: Complete member initialization
            this.player = player;
            dir = p1;
            Power = p2;
            Speed = 7;
            X = player.X + player.Width - 5;
            Y = player.Y + (player.Height/2) - 3;

            if (dir > 3 && dir < 7)
            {
                X = player.X + player.Width - 50;
                Y = player.Y + (player.Height/2) - 30;
            }
            if (dir > 6 && dir < 10)
            {
                X = player.X + player.Width - 50;
                Y = player.Y + (player.Height/2) + 30;
            }


            RadiusX = 5;
            RadiusY = 5;
        }

        public override void Calculate()
        {
            if (dir == 1)
            {
                if (step == 1)
                {
                    X += 5;
                    Y += 2;
                }
                else
                {
                    X += Speed;
                }
            }
            else if (dir == 2)
            {
                X += Speed;
            }
            else if (dir == 3)
            {
                if (step == 1)
                {
                    X += 5;
                    Y -= 2;
                }
                else
                {
                    X += Speed;
                }
            }


            if (dir == 4)
            {
                if (step == 1)
                {
                    X += 5;
                    Y += 2;
                }
                else
                {
                    X += Speed;
                }
            }
            else if (dir == 5)
            {
                X += Speed;
            }
            else if (dir == 6)

            {
                if (step == 1)
                {
                    X += 5;
                    Y -= 2;
                }
                else
                {
                    X += Speed;
                }
            }

            if (dir == 7)
            {
                if (step == 1)
                {
                    X += 5;
                    Y += 2;
                }
                else
                {
                    X += Speed;
                }
            }
            else if (dir == 8)
            {
                X += Speed;
            }
            else if (dir == 9)
            {
                if (step == 1)
                {
                    X += 5;
                    Y -= 2;
                }
                else
                {
                    X += Speed;
                }
            }

            step++;
            if (step == 2)
            {
                step = 0;
            }
        }
    }
}