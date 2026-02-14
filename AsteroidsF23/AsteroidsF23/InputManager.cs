using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidsF23
{
    internal class InputManager
    {
        private Spaceship player;
        private List<Projectile> projectiles;
        private Form gameWindow;
        private AudioManager audioManager;

        //Handle all objects involved in inputs
        public InputManager(Form gameWindow, Spaceship player, List<Projectile> projectiles, AudioManager audioManager)
        {
            this.gameWindow = gameWindow;
            this.player = player;
            this.projectiles = projectiles;
            this.audioManager = audioManager;

            gameWindow.KeyPress += HandleKeyPress;
            gameWindow.KeyPreview = true;

            gameWindow.MouseDown += HandleMouseDown;
            gameWindow.MouseMove += HandleMouseMove;

            gameWindow.Cursor = Cursors.Cross;
        }

        //Set key presses
        public void HandleKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'w') player.accelerate();
            if (e.KeyChar == 'a') player.rotateLeft();
            if (e.KeyChar == 'd') player.rotateRight();
            if (e.KeyChar == 's') player.decelerate();
            //if (e.KeyChar == 'e') player.LifeSystem.TakeHit(); Debugging tool meant to test life functions
            if (e.KeyChar == ' ')
            {
                var projectile = new Projectile(player.Position, player.Rotation, gameWindow);
                projectile.createdByPlayer = true;
                projectiles.Add(projectile);

                audioManager.Play("shoot");
            }
            if (e.KeyChar == 'r') //Restart button on death
            {
                if (gameWindow.Controls["restartButton"].Visible)
                {
                    ((Form1)gameWindow).restartGame();
                }
            }
        }

        //Mouse fire
        public void HandleMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                float dx = e.X - player.Position.X;
                float dy = e.Y - player.Position.Y;
                float angle = (float)(Math.Atan2(dy, dx) * 180 / Math.PI);

                FireProjectile(player.Position, angle);
            }
        }

        //Mouse aim
        public void HandleMouseMove(object sender, MouseEventArgs e)
        {
            float dx = e.X - player.Position.X;
            float dy = e.Y - player.Position.Y;
            player.Rotation = (float)(Math.Atan2(dy, dx) * 180 / Math.PI);

        }

        //Click to fire
        public void FireProjectile(PointF position, float angle)
        {
            var projectile = new Projectile(position, angle, gameWindow);
            projectile.createdByPlayer = true;
            projectiles.Add(projectile);

            audioManager.Play("shoot");
        }
    }
}
