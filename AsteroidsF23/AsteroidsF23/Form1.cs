using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace AsteroidsF23
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.Timer gameTimer;
        private Level level;
        private List<Asteroid> asteroids;
        private List<Projectile> projectiles;
        private Spaceship player;
        private ScoreSystem scoreSystem;

        //Return the game window so it can be used for dynamic screen wraparound
        //This took a while to figure out and get working
        public Form1 gameWindow
        {
            get { return this; }
        }

        public Form1()
        {
            //Initialize the form
            InitializeComponent();
            //Hide restart button
            restartButton.Visible = false;
            restartButton.Enabled = false;

            //Initialize game objects and systems
            level = new Level();
            KeyPress += Form1_KeyPress;
            KeyPreview = true;
            scoreSystem = new ScoreSystem();
        }

        /* 
        * spawnAsteroids
        * Spawns asteroids at the start of the game and when all asteroids are destroyed
        */
        private void spawnAsteroids()
        {
            for (int i = 0; i < level.asteroidCount; i++)
            {
                asteroids.Add(new Asteroid(gameWindow));
            }
        }

        /* 
        * initializeGame
        * Initializes the game objects and systems, and starts the game timer
        */
        private void initializeGame()
        {
            //Instantiating the game objects
            level = new Level();
            scoreSystem = new ScoreSystem();
            asteroids = new List<Asteroid>();
            player = new Spaceship(gameWindow);
            projectiles = new List<Projectile>();

            //Add initial asteroids
            spawnAsteroids();

            //Start the game timer
            //Set to 60 fps. This can be changed, but it messes with the game physics a bit
            //I have a 240 hz monitor, so I had it set to 240 for a while
            gameTimer = new System.Windows.Forms.Timer { Interval = 1000 / 60 };
            gameTimer.Tick += gameLoop;
            gameTimer.Start();
        }

        /*
        * gameLoop
        * The main game loop, responsible for updating and drawing game objects, detecting collisions, and handling level progression
        */
        private void gameLoop(object sender, EventArgs e)
        {
            //Update the level count label
            levelCountLabel.Text = "Level " + level.currentLevel;

            //Create a graphics object to draw game objects
            Graphics g = CreateGraphics();

            //Update and draw the player
            player.erase(g);
            player.update(1);
            player.draw(g);

            //Update and draw the projectiles
            //Modified from Dr. Beard's note 8.2
            foreach (Projectile projectile in projectiles.ToList())
            {
                projectile.erase(g);
                projectile.update(1);
                projectile.draw(g);

                //Remove projectiles that have exceeded their lifespan
                if (projectile.age >= projectile.timeToLive)
                {
                    projectile.erase(g);
                    projectiles.Remove(projectile);
                    continue;
                }

                //Check for collisions between projectiles not created by the player and the player.
                //This was to account for projectiles made by a saucer, but I did not end up having time to add it in.
                if (!projectile.createdByPlayer && projectile.getBounds().IntersectsWith(player.getBounds()))
                {
                    //Stop the game and show the restart button
                    gameTimer.Stop();
                    restartButton.Visible = true;
                    restartButton.Enabled = true;
                    //break to leave the game loop
                    break;
                }

                //Check for collisions between projectiles and asteroids
                foreach (Asteroid asteroid in asteroids.ToList())
                {
                    if (projectile.getBounds().IntersectsWith(asteroid.getBounds()))
                    {
                        //Remove collided projectile and asteroid
                        //Modified from Dr. Beard's note 8.2
                        projectile.erase(g);
                        projectiles.Remove(projectile);
                        asteroid.erase(g);

                        //Split large asteroids into smaller ones and update the score
                        if (asteroid.size > 12.5)
                        {
                            asteroids.Add(new Asteroid(gameWindow, asteroid.Position.X, asteroid.Position.Y, asteroid.size / 2));
                            asteroids.Add(new Asteroid(gameWindow, asteroid.Position.X, asteroid.Position.Y, asteroid.size / 2));
                            scoreSystem.addScore(50);
                            scoreLabel.Text = "Score " + scoreSystem.Score;
                        }
                        //Update the score for small asteroids
                        else
                        {
                            scoreSystem.addScore(100);
                            scoreLabel.Text = "Score " + scoreSystem.Score;
                        }
                        //remove the destroyed asteroid
                        asteroids.Remove(asteroid);
                    }
                }

                //Move to the next level if all asteroids have been destroyed
                if (asteroids.Count == 0)
                {
                    level.nextLevel();
                    spawnAsteroids();
                }
            }

            //Update and draw the asteroids
            foreach (Asteroid asteroid in asteroids)
            {
                asteroid.erase(g);
                asteroid.update(1);
                asteroid.draw(g);

                //Check for collisions between the player and asteroids
                if (player.getBounds().IntersectsWith(asteroid.getBounds()))
                {
                    if (player.LifeSystem.TakeHit())
                    {
                        if (player.LifeSystem.IsDead())
                        {
                            gameTimer.Stop();
                            restartButton.Visible = true;
                            restartButton.Enabled = true;
                            instructionsLabel.Visible = true;
                            instructionsLabel.Text = "Game Over. You made it to level " + level.currentLevel + " and had a score of " + scoreSystem.Score;
                        }
                    }
                }
            }
        }

        /* 
        * Form1_KeyPress
        * Event handler for key presses, controls the player's spaceship and fires projectiles
        * Modified from Dr. Beard's Note 8.4
        */
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'w') player.accelerate();
            if (e.KeyChar == 'a') player.rotateLeft();
            if (e.KeyChar == 'd') player.rotateRight();
            if (e.KeyChar == 's') player.decelerate();
            if (e.KeyChar == ' ')
            {
                var projectile = new Projectile(player.Position, player.Rotation, gameWindow);
                projectile.createdByPlayer = true;
                projectiles.Add(projectile);
            }
        }

        /* 
        * startButton_Click
        * On click method for startButton, initializes the game and hides the button and instructions
        */
        private void startButton_Click(object sender, EventArgs e)
        {
            initializeGame();
            //Hide the button and instructions
            startButton.Visible = false;
            startButton.Enabled = false;
            instructionsLabel.Visible = false;
            //Focus on the form so inputs can be taken
            this.Focus();
        }

        /* 
        * restartButton_Click
        * On click method for restartButton, clears the screen, resets the score, and initializes the game
        */
        private void restartButton_Click(object sender, EventArgs e)
        {
            clearScreen();
            //Reset score on restart
            scoreLabel.Text = "Score 0";
            initializeGame();
            //Hide the button
            restartButton.Visible = false;
            restartButton.Enabled = false;
            instructionsLabel.Visible = false;
            //Focus on the form so inputs can be taken
            this.Focus();
        }

        /*
        * clearScreen
        * Erases all game objects from the screen
        * Modified from Dr. Beard's note 8.2
        */
        private void clearScreen()
        {
            //Create a graphics object to erase game objects
            Graphics g = CreateGraphics();

            //Erase the spaceship
            player.erase(g);

            //Erase the projectiles
            foreach (Projectile projectile in projectiles.ToList())
            {
                projectile.erase(g);
            }

            //Erase the asteroids
            foreach (Asteroid asteroid in asteroids.ToList())
            {
                asteroid.erase(g);
            }
        }
    }
}