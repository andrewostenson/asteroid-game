using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidsF23
{
    internal class CollisionManager
    {
        private Spaceship spaceship;
        private AsteroidManager asteroidManager;
        private List<Projectile> projectile;
        private ScoreSystem scoreSystem;
        private List<Explosion> explosions;

        //Add all items involved in a collision
        public CollisionManager(Spaceship spaceship, AsteroidManager asteroidManager, List<Projectile> projectile, ScoreSystem score, List<Explosion> explosions)
        {
            this.spaceship = spaceship;
            this.asteroidManager = asteroidManager;
            this.projectile = projectile;
            this.scoreSystem = score;
            this.explosions = explosions;
        }

        //Check for collisions
        public void Update(Graphics g)
        {
            AsteroidToPlayerCollision(g);
            ProjectileToAsteroidCollision(g);
        }

        //Handle player taking damage from asteroid
        public void AsteroidToPlayerCollision(Graphics g)
        {
            foreach(var asteroid  in asteroidManager.Asteroids.ToList())
            {
                if (spaceship.getBounds().IntersectsWith(asteroid.getBounds()))
                {
                    spaceship.LifeSystem.TakeHit();
                    explosions.Add(new Explosion(asteroid.Position));
                    asteroidManager.Delete(asteroid, g);
                }
            }

        }

        //Handle player destroying an asteroid
        public void ProjectileToAsteroidCollision(Graphics g)
        {
            foreach (var projectiles in projectile.ToList())
            {
                foreach (var asteroid in asteroidManager.Asteroids.ToList())
                {
                    if (projectiles.getBounds().IntersectsWith(asteroid.getBounds()))
                    {
                        if (asteroid.size > 12.5)
                        {
                            asteroidManager.Split(asteroid);
                            explosions.Add(new Explosion(asteroid.Position));
                            asteroidManager.Delete(asteroid, g);
                            scoreSystem.addScore(50);
                        }
                        else
                        {
                            explosions.Add(new Explosion(asteroid.Position));
                            asteroidManager.Delete(asteroid, g);
                            scoreSystem.addScore(100);
                        }
                        projectiles.erase(g);
                        projectile.Remove(projectiles);

                        break;
                    }
                }
            }
        }
    }
}
