using System;
using System.Collections.Generic;
using System.Drawing;

namespace AsteroidsF23
{
    internal class BackgroundRenderer
    {
        private List<PointF> stars;
        private Random rand;
        private int starCount;
        private int width;
        private int height;

        //Build background stars
        public BackgroundRenderer(int screenWidth, int screenHeight, int starCount = 100)
        {
            this.width = screenWidth;
            this.height = screenHeight;
            this.starCount = starCount;
            rand = new Random();
            stars = new List<PointF>();

            GenerateStars();
        }

        //Randomly generate star positions
        private void GenerateStars()
        {
            stars.Clear();
            for (int i = 0; i < starCount; i++)
            {
                float x = rand.Next(width);
                float y = rand.Next(height);
                stars.Add(new PointF(x, y));
            }
        }

        //Draw stars
        public void Draw(Graphics g)
        {
            foreach (var star in stars)
            {
                g.FillEllipse(Brushes.White, star.X, star.Y, 2, 2);
            }
        }

        //Remove stars
        public void Delete()
        {
            stars.Clear();
        }
    }
}