using MazePong.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MazePong.GravityObjects {
    public class GravityWells {
        private Point? wellLocation;
        private readonly Texture2D wellImage;
        private readonly int diameter;

        public event EventHandler WellCreated;
        public event EventHandler WellDestroyed;

        public Point? WellLocation { get { return wellLocation; } }

        public GravityWells(Texture2D wellImage) {
            this.wellImage = wellImage;
            diameter = wellImage.Width;
        }

        public void HandleInput() {
            if (!(InputManager.IsMouseDown(MouseButtons.Left) || InputManager.IsMouseDown(MouseButtons.Right))) {
                if (wellLocation == null) {
                    return;
                }
                wellLocation = null;
                WellDestroyed.Invoke(this, null);
                return;
            }

            else if (wellLocation != null) {
                return;
            }

            Point mousePoint = MouseHelper.OnScreenPosition;
            wellLocation = mousePoint;
            WellCreated.Invoke(this, null);
        }

        public void Draw(SpriteBatch spriteBatch) {
            if (wellLocation == null) { return; }

            Rectangle destination = new(
                wellLocation.Value.X - (diameter / 2),
                wellLocation.Value.Y - (diameter / 2),
                diameter,
                diameter);

            spriteBatch.Draw(wellImage, destination, Color.White);
        }
    }
}
