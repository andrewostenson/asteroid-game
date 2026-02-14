using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidsF23
{
    internal class Spaceship : GameObject
    {
        public LifeSystem LifeSystem { get; private set; }

        //Constants for acceleration, max speed, and rotation speed
        private const float acceleration = 0.1f;
        private const float maxSpeed = 5f;
        private const float rotationSpeed = 3f;
        private Form gameWindow { get; set; }

        public PointF velocity { get; private set; }

        /*4/22/23
        * Spaceship constructor
        * Initializes a Spaceship object with a specified game window, sets position, velocity, and rotation
        */
        public Spaceship(Form gameWindow)
        {
            Position = new PointF(400, 300);
            velocity = new PointF(0, 0);
            Rotation = 0;
            this.gameWindow = gameWindow;
            LifeSystem = new LifeSystem();
        }

        /*  4/24/23
        * update
        * Updates the ship's position based on its velocity and applies screen wrapping
        */

        public override void update(float deltaTime)
        {
            //Update the ship's position based on its velocity
            Position = new PointF((Position.X + velocity.X * deltaTime), (Position.Y + velocity.Y * deltaTime));
            //Screen wrapping
            Position = new PointF(
                (Position.X + gameWindow.ClientSize.Width) % gameWindow.ClientSize.Width,
                (Position.Y + gameWindow.ClientSize.Height) % gameWindow.ClientSize.Height);
        }

        /*  4/24/23
        * draw
        * Draws the spaceship as a triangle on the screen
        * Modified from Dr. Beard's Notes 8.3, 8.2, and 8.1
        */
        public override void draw(Graphics g)
        {
            //Get the points for the triangular ship
            PointF[] points = getPoints();
            //Draw the triangle
            g.FillPolygon(Brushes.White, points);
        }

        /* 4/24/23
        * erase
        * Erases the spaceship from the screen by drawing over it with black
        * Modified from Dr. Beard's Notes 8.3, 8.2, and 8.1
        */
        public void erase(Graphics g)
        {
            //Size of rectangular area to erase
            float size = 22f;
            //Just erasing the triangle left behind white pixels sometimes, so it now erases a rectangular area around the triangle
            g.FillRectangle(Brushes.Black, Position.X - size, Position.Y - size, size * 2, size * 2);
        }

        /* 4/24/23
        * accelerate
        * Increases the spaceship's velocity in the direction it is currently facing
        */
        public void accelerate()
        {
            velocity = new PointF(velocity.X + acceleration * (float)Math.Cos(Rotation * Math.PI / 180),
                                  velocity.Y + acceleration * (float)Math.Sin(Rotation * Math.PI / 180));
            float currentSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);

            if (currentSpeed > maxSpeed)
            {
                velocity = new PointF(maxSpeed * velocity.X / currentSpeed, maxSpeed * velocity.Y / currentSpeed);
            }
        }


        /* 4/27/23
        * decelerate
        * Decreases the spaceship's velocity in the direction it is currently facing
        */
        public void decelerate()
        {
            velocity = new PointF(velocity.X - acceleration * (float)Math.Cos(Rotation * Math.PI / 180),
                                  velocity.Y - acceleration * (float)Math.Sin(Rotation * Math.PI / 180));

            float currentSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);

            if (currentSpeed > maxSpeed)
            {
                velocity = new PointF(maxSpeed * velocity.X / currentSpeed, maxSpeed * velocity.Y / currentSpeed);
            }
        }

        /* 4/25/23
        * rotateLeft
        * Rotates the spaceship counter-clockwise
        */
        public void rotateLeft()
        {
            Rotation -= rotationSpeed;
        }

        /* 4/25/23
        * rotateRight
        * Rotates the spaceship clockwise
        */
        public void rotateRight()
        {
            Rotation += rotationSpeed;
        }

        /* 4/28/23
        * getBounds
        * Returns the rectangular hitbox of the spaceship
        */
        public override RectangleF getBounds()
        {
            PointF[] points = getPoints();

            //Find the minimum and maximum X and Y values among all the points in the polygon
            float minX = float.MaxValue;
            float maxX = float.MinValue;
            float minY = float.MaxValue;
            float maxY = float.MinValue;

            foreach (PointF point in points)
            {
                if (point.X < minX)
                {
                    minX = point.X;
                }
                if (point.X > maxX)
                {
                    maxX = point.X;
                }
                if (point.Y < minY)
                {
                    minY = point.Y;
                }
                if (point.Y > maxY)
                {
                    maxY = point.Y;
                }
            }
            //Calculate the width and height of the rectangle
            float width = maxX - minX;
            float height = maxY - minY;

            float scaleFactor = 0.6f; //This lets you change the scaling of the hitbox. This made it easier to troubleshoot when I was testing things
            float scaledWidth = width * scaleFactor;
            float scaledHeight = height * scaleFactor;

            //Adjust the position of the rectangle so it remains centered on the spaceship
            float offsetX = (width - scaledWidth) / 2;
            float offsetY = (height - scaledHeight) / 2;

            return new RectangleF(minX + offsetX, minY + offsetY, scaledWidth, scaledHeight);
        }

        /* 4/28/23
        * getPoints
        * Returns the array of points that form the spaceship's triangular shape
        */
        private PointF[] getPoints()
        {
            PointF[] points = new PointF[3];
            double angle = Rotation * Math.PI / 180;
            float size = 22f;

            //Define points for the triangle
            points[0] = new PointF(Position.X + size * (float)Math.Cos(angle), Position.Y + size * (float)Math.Sin(angle));
            points[1] = new PointF(Position.X + size * (float)Math.Cos(angle + 5 * Math.PI / 6), Position.Y + size * (float)Math.Sin(angle + 5 * Math.PI / 6));
            points[2] = new PointF(Position.X + size * (float)Math.Cos(angle + 7 * Math.PI / 6), Position.Y + size * (float)Math.Sin(angle + 7 * Math.PI / 6));

            return points;
        }
    }
}
