using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidsF23
{
    internal class Asteroid : GameObject
    {
        private static Random random = new Random();
        public float speed;
        public PointF direction { get; set; }
        public float size { get; private set; }
        private Form gameWindow { get; set; }

        /*
        * Asteroid (single parameter constructor)
        * Creates an Asteroid with a random position, size, and direction, while maintaining a safe distance from the player's starting position
        */
        public Asteroid(Form gameWindow)
        {

            float safeDistance = 150; // Set a safe distance from the ship's starting position so they don't spawn on top of the player
            PointF shipStartPosition = new PointF(gameWindow.ClientSize.Width / 2, gameWindow.ClientSize.Height / 2);

            //Generate the starting position for the asteroid based on the size of the window
            PointF asteroidPosition = new PointF(
            random.Next(0, gameWindow.ClientSize.Width),
            random.Next(0, gameWindow.ClientSize.Height));

            //Check if the asteroid's position is within the safe distance from the ship's starting position
            //If it is, generate a new position until it is outside the safe distance
            while (distance(asteroidPosition, shipStartPosition) < safeDistance)
            {
                asteroidPosition = new PointF(
                    random.Next(0, gameWindow.ClientSize.Width),
                    random.Next(0, gameWindow.ClientSize.Height));
            }
            //Set the position to the valid position that was generated
            Position = asteroidPosition;
            //Determine speed and direction
            speed = (float)(random.NextDouble() * 2 + 1);
            double randomAngle = random.NextDouble() * 2 * Math.PI;
            direction = new PointF((float)Math.Cos(randomAngle), (float)Math.Sin(randomAngle));
            //Set the size to 25. This is so it can be easily changed later on
            size = 25;
            //Game window property for the screen wrap
            this.gameWindow = gameWindow;
        }

        /*
        * Asteroid (four parameter constructor)
        * Creates an Asteroid with a specified position and size, while generating a random direction and slightly faster speed
        */
        public Asteroid(Form gameWindow, float x, float y, float size)
        {
            Position = new PointF(x, y);
            //make the new smaller asteroid a little faster than the original one
            speed = (float)(random.NextDouble() * 2 + 1.5);

            //Determine a random linear direction for the asteroid to move in
            double randomAngle = random.NextDouble() * 2 * Math.PI;
            direction = new PointF((float)Math.Cos(randomAngle), (float)Math.Sin(randomAngle));
            this.size = size;
            //Game window property for the screen wrap
            this.gameWindow = gameWindow;
        }

        /* 
        * Distance
        * Calculates the distance between two points
        */
        private float distance(PointF point1, PointF point2)
        {
            float deltaX = point1.X - point2.X;
            float deltaY = point1.Y - point2.Y;
            return (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }

        /*
        * Update
        * Updates the position of the Asteroid based on its speed and direction, applies screen wrap
        */
        public override void update(float deltaTime)
        {
            //Calculate the new position based on the asteroid's speed and direction
            float newX = Position.X + speed * direction.X * deltaTime;
            float newY = Position.Y + speed * direction.Y * deltaTime;

            //Apply screen wrap
            Position = new PointF(
                (newX + gameWindow.ClientSize.Width) % gameWindow.ClientSize.Width,
                (newY + gameWindow.ClientSize.Height) % gameWindow.ClientSize.Height);
        }

        /* 
        * Draw
        * Draws the Asteroid on the screen
        * Modified from Dr. Beard's note 8.3
        */
        public override void draw(Graphics g)
        {
            PointF[] polygon = generatePolygon();
            g.DrawPolygon(Pens.White, polygon);
        }

        /* 
        * erase
        * Erases the Asteroid's polygon from the screen
        * Modified from Dr. Beard's note 8.3
        */
        public void erase(Graphics g)
        {
            PointF[] polygon = generatePolygon();
            g.DrawPolygon(Pens.Black, polygon);
        }

        /* 
        * generatePolygon
        * Generates a polygon for the Asteroid's shape
        * I decided an octogon would be cooler than just a circle
        */

        private PointF[] generatePolygon()
        {
            PointF[] points = new PointF[8];
            float angleStep = 2 * (float)Math.PI / points.Length;
            //Calculate the points of the octagon based on the angle and size
            for (int i = 0; i < points.Length; i++)
            {
                float angle = Rotation * (float)Math.PI / 180 + angleStep * i;
                points[i] = new PointF(Position.X + size * (float)Math.Cos(angle), Position.Y + size * (float)Math.Sin(angle));
            }

            return points;
        }

        /* 
        * GetBounds
        * Returns the "bounds" of the asteroid as a rectangular hitbox
        * I wanted to use a more precise method of dealing with collisions, but I went with this
        * and decided I'd change it later if there was time. With extra time I would implement the SAT (seperating axis theorem) 
        * method for collisions, but the rectangular hitbox is just fine for this implementation.
        */
        public override RectangleF getBounds()
        {
            //Generate the polygon that represents the object
            PointF[] polygon = generatePolygon();

            //Find the minimum X value among all the points in the polygon
            float minX = float.MaxValue;
            foreach (PointF point in polygon)
            {
                if (point.X < minX)
                {
                    minX = point.X;
                }
            }

            //Find the maximum X value among all the points in the polygon
            float maxX = float.MinValue;
            foreach (PointF point in polygon)
            {
                if (point.X > maxX)
                {
                    maxX = point.X;
                }
            }

            //Find the minimum Y value among all the points in the polygon
            float minY = float.MaxValue;
            foreach (PointF point in polygon)
            {
                if (point.Y < minY)
                {
                    minY = point.Y;
                }
            }

            //Find the maximum Y value among all the points in the polygon
            float maxY = float.MinValue;
            foreach (PointF point in polygon)
            {
                if (point.Y > maxY)
                {
                    maxY = point.Y;
                }
            }

            //Calculate the width and height of the bounding rectangle
            float width = maxX - minX;
            float height = maxY - minY;

            //Return the bounding rectangle
            return new RectangleF(minX, minY, width, height);
        }
    }
}
