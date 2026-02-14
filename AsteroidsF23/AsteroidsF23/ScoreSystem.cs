using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidsF23
{
    internal class ScoreSystem
    {
        public int Score { get; private set; }

        /*  4/27/23
        * AddScore
        * Adds the specified number of points to the current score
        */
        public void addScore(int points)
        {
            Score += points;
        }
    }
}
