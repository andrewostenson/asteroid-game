using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidsF23
{
    internal class Projectile : GameObject
    {
        //Speed of projectile
        public float speed { get; set; }

        private Form gameWindow;

        public float timeToLive { get; set; }
        public float age { get; set; }

        public bool createdByPlayer { get; set; }

        //How far the projectile can travel
        public float travelDistance { get; set; }

        private SizeF size;

        /* 
        * Projectile constructor
        * Creates a Projectile with a specified position and rotation, sets the speed and other properties
        */
        public Projectile(PointF position, float rotation, Form gameWindow)
        {
            Position = position;
            Rotation = rotation;
            size = new SizeF(4, 4);
            speed = 8f;
            this.gameWindow = gameWindow;
            timeToLive = 50f;
            age = 0.0f;
        }

        /* 
        * update
        * Updates the position of the Projectile based on its speed and rotation, applies screen wrap, and updates its age
        * Modified from Dr. Beard's Note 8.3
        */
        public override void update(float deltaTime)
        {
            age += deltaTime;
            Position = new PointF(Position.X + speed * (float)Math.Cos(Rotation * Math.PI / 180) * deltaTime,
                                  Position.Y + speed * (float)Math.Sin(Rotation * Math.PI / 180) * deltaTime);
            Position = new PointF((Position.X + gameWindow.ClientSize.Width) % gameWindow.ClientSize.Width,
                                 (Position.Y + gameWindow.ClientSize.Height) % gameWindow.ClientSize.Height);
        }

        /* 
        * draw
        * Draws the Projectile on the screen
        * Modified from Dr. Beard's Notes 8.3, 8.2, and 8.1
        */
        public override void draw(Graphics g)
        {
            g.FillRectangle(Brushes.Yellow, Position.X, Position.Y, size.Width, size.Height);
        }

        /* 
        * erase
        * Erases the Projectile from the screen
        * Modified from Dr. Beard's Notes 8.3, 8.2, and 8.1
        */
        public void erase(Graphics g)
        {
            g.FillRectangle(Brushes.Black, Position.X, Position.Y, size.Width, size.Height);
        }

        /* 
        * getBounds
        * Returns the "bounds" of the projectile as a rectangular hitbox
        */
        public override RectangleF getBounds()
        {
            return new RectangleF(Position.X, Position.Y, size.Width, size.Height);
        }
    }
}
