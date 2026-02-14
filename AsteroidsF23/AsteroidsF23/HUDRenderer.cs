using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidsF23
{
    internal class HUDRenderer
    {
        private Form gameWindow;

        //Draw lives to top of screen as red dots
        public void DrawLives(Graphics g, LifeSystem LifeSystem)
        {
            int TotalLives = 3;
            int RemainingLives = LifeSystem.Lives;
            int x = 80;
            int y= 13;
            int size = 20;

            for (int i = 0; i < TotalLives; i++) 
            {
                Brush brush = (i < RemainingLives) ? Brushes.Red : Brushes.DarkRed;
                g.FillEllipse(brush, x + i * (size + 5), y, size, size);
                g.DrawEllipse(Pens.Black, x + i * (size + 5), y, size, size);
            }
        }

        //Draw shield bar
        public void DrawShield(Graphics g, bool ShieldActive)
        {
            int x = 80;
            int y = 50;
            int width = 60;
            int height = 10;

            g.FillRectangle(ShieldActive ? Brushes.Cyan : Brushes.Gray, x, y, width, height);
            g.DrawRectangle(Pens.Black, x, y, width, height);
        }

        //Render to screen
        public void Render(Graphics g, LifeSystem LifeSystem)
        {
            DrawLives(g, LifeSystem);
            DrawShield(g, LifeSystem.Shield.IsActive);
        }


    }
}
