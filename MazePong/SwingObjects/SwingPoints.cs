using MazePong.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MazePong.SwingObjects {
    public class SwingPoints {
        private Point? swingLocation;
        private readonly Texture2D swingImage;
        private readonly int diameter;

        public event EventHandler SwingCreated;
        public event EventHandler SwingDestroyed;

        public Point? SwingLocation { get { return swingLocation; } }

        public SwingPoints(Texture2D swingImage) {
            this.swingImage = swingImage;
            diameter = swingImage.Width;
        }

        public void HandleInput() {
            if (!(InputManager.IsMouseDown(MouseButtons.Left) || InputManager.IsMouseDown(MouseButtons.Right))) {
                if (swingLocation == null) {
                    return;
                }
                swingLocation = null;
                SwingDestroyed.Invoke(this, null);
                return;
            }

            else if (swingLocation != null) {
                return;
            }

            Point mousePoint = MouseHelper.OnScreenPosition;
            swingLocation = mousePoint;
            SwingCreated.Invoke(this, null);
        }

        public void Draw(SpriteBatch spriteBatch) {
            if (swingLocation == null) { return; }

            Rectangle destination = new(
                swingLocation.Value.X - (diameter / 2),
                swingLocation.Value.Y - (diameter / 2),
                diameter,
                diameter);

            spriteBatch.Draw(swingImage, destination, Color.White);
        }
    }
}
