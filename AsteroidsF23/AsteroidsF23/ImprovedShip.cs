using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AsteroidsF23
{
    internal class ImprovedShip : Spaceship
    {
        private Image shipSprite;

        //Find desired sprite
        public ImprovedShip(Form gameWindow, string spriteFilePath) : base(gameWindow)
        {
            shipSprite = Image.FromFile(spriteFilePath);
        }

        //Override default ship
        public override void draw(Graphics g)
        {
            if (shipSprite == null)
            {
                return;
            }

            var state = g.Save();

            g.TranslateTransform(Position.X,  Position.Y);

            g.RotateTransform(Rotation + 90);

            int targetSize = 44;
            g.DrawImage(shipSprite, -targetSize / 2, -targetSize / 2, targetSize, targetSize);

            g.Restore(state);
        }

        //Erase ship for updates
        public void erase(Graphics g)
        {
            float size = Math.Max(shipSprite.Width, shipSprite.Height);
            g.FillRectangle(Brushes.Black, Position.X - size / 2, Position.Y - size / 2, size, size);
        }
    }
}
