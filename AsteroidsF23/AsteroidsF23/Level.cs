using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidsF23
{
    internal class Level
    {
        //Current level information
        public int currentLevel { get; set; }

        //Number of asteroids per level
        public int asteroidCount { get; set; }

        //Speed of asteroids each level
        public int asteroidSpeed { get; set; }

        public Level()
        {
            currentLevel = 1;
            asteroidCount = 3;
        }
        /* 
        * update
        * Abstract method for updating the object state based on deltaTime
        */
        public void nextLevel()
        {
            currentLevel++;
            asteroidCount += 2; //Increase the number of asteroids for the next level
        }
        //Resetting the levels is done by making a new level object
    }
}
