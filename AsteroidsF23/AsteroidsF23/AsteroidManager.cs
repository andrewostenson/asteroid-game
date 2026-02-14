using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidsF23
{
    internal class AsteroidManager
    {
        private List<Asteroid> asteroids;
        private AudioManager audioManager;
        private Form gameWindow;
        public List<Asteroid> Asteroids
        {
            get { return asteroids; }
        }

        //Constructor
        public AsteroidManager(Form window)
        {
            gameWindow = window;
            asteroids = new List<Asteroid>();
            audioManager = new AudioManager();
        }

        //Add specified amount of asteroids to the list
        public void Spawn(int count)
        {
            for (int i = 0; i < count; i++) 
            {
                asteroids.Add(new Asteroid(gameWindow));
            }
        }

        //Draw and update asteroids
        public void Update(Graphics g)
        {
            foreach (var asteroid in asteroids.ToList())
            {
                asteroid.erase(g);
                asteroid.update(1);
                asteroid.draw(g);
            }

        }

        //Erase asteroids
        public void Delete(Asteroid a, Graphics g)
        {
            a.erase(g);
            asteroids.Remove(a);
            audioManager.Play("explode");
            
        }

        //Split larger asteroids
        public void Split(Asteroid a)
        {
            asteroids.Add(new Asteroid(gameWindow, a.Position.X, a.Position.Y, a.size / 2));
            asteroids.Add(new Asteroid(gameWindow, a.Position.X, a.Position.Y, a.size / 2));

            audioManager.Play("explode");
        }

        //Check for no asteroids remaining
        public bool AllDestroyed()
        {
            if (asteroids.Count == 0)
            {
                return true;
            }

            return false;
        }
    }
}
