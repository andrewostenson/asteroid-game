using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidsF23
{
    internal class Explosion
    {
        public PointF Position {  get; set; }
        public int Age { get; set; }
        public int TimeToLive { get; set; } = 5;
        
        //Create explosion
        public Explosion(PointF position)
        {
            Position = position;
            Age = 0;
        }

        //Update time to live
        public void Update()
        {
            Age++;
        }

        //Draw explosion
        public void Draw(Graphics g)
        {
            int length = 20; // line length
            Pen pen = Pens.Orange;

            g.DrawLine(pen, Position.X - length, Position.Y, Position.X + length, Position.Y);
            g.DrawLine(pen, Position.X, Position.Y - length, Position.X, Position.Y + length);
            g.DrawLine(pen, Position.X - length, Position.Y - length, Position.X + length, Position.Y + length);
            g.DrawLine(pen, Position.X - length, Position.Y + length, Position.X + length, Position.Y - length);
        }

        //Is animation finished
        public bool isFinished()
        {
            return Age > TimeToLive;
        }

        //Draw back over in black to erase
        public void Delete(Graphics g)
        {

            int length = 20;
            Pen pen = Pens.Black;

            g.DrawLine(pen, Position.X - length, Position.Y, Position.X + length, Position.Y);
            g.DrawLine(pen, Position.X, Position.Y - length, Position.X, Position.Y + length);
            g.DrawLine(pen, Position.X - length, Position.Y - length, Position.X + length, Position.Y + length);
            g.DrawLine(pen, Position.X - length, Position.Y + length, Position.X + length, Position.Y - length);
        }
    }
}
