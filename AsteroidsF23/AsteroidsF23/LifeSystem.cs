using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidsF23
{
    internal class LifeSystem
    {
        public Shield Shield { get; private set; }
        public int Lives { get; private set; }

        public LifeSystem(int StartingLives = 3)
        {
            Lives = StartingLives;
            Shield = new Shield();
        }

        public bool TakeHit()
        {
            if (Shield.IsActive)
            {
                Shield.AbsorbHit();
                return false;
            }
            else
            {
                if (Lives > 0)
                {
                    Lives--;
                    return true;
                }
                return false;
        }

        public bool IsDead()
        {
            return Lives == 0;
        }

        public void Reset(int StartingLives = 3)
        {
            Lives = StartingLives;
            Shield.Activate();
        }

    }
}


