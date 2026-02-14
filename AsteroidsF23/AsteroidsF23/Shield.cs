using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidsF23
{
    internal class Shield
    {
        public bool IsActive { get; set; } = false;

        //Activate shield
        public void Active()
        {
            IsActive = true;
        }

        //Returns true if absorbed damage
        public bool AbsorbHit()
        {
            if (IsActive)
            {
                IsActive = false;
                return true;
            }

            return false;
        }
    }
}


