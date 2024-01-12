using MazePong.Controls;
using MazePong.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazePong.PongObjects {
    public class Bumper : Control {
        private Vector2 lastPosition;
        private Point mousePoint;

        public int Radius { get { return radius; } }

        public Vector2 NormalizedVelocity {
            get {
                Vector2 velocity = position - lastPosition;
                float speed = velocity.Length();
                if (speed > maxSpeed) {
                    velocity *= maxSpeed / speed;
                }
                return velocity;
            }
        }

        public Vector2 Velocity { get { return position - lastPosition; } }


        public Vector2 LastPosition { get { return lastPosition; } }

        private readonly int radius;
        private readonly int diameter;
        private readonly Texture2D bumperImage;
        private readonly int wallThickness = 20;
        private readonly float maxSpeed = 20f;

        public Bumper(Texture2D image) {
            bumperImage = image;
            radius = image.Width / 2;
            diameter = image.Width;
        }

        private Vector2 mouseToClampedPosition(Point point) {
            return new Vector2(
                Math.Clamp(point.X, radius + wallThickness, Settings.BaseWidth - radius - wallThickness),
                Math.Clamp(point.Y, radius + wallThickness, Settings.BaseHeight - radius - wallThickness));
        }

        public override void HandleInput() {
            mousePoint = MouseHelper.Position;
        }

        public override void Update(GameTime gameTime) {
            HandleInput();

            lastPosition = position;
            position = mouseToClampedPosition(mousePoint);
        }

        public override void Draw(SpriteBatch spriteBatch) {
            Rectangle destination = new(
                (int)(position.X - radius),
                (int)(position.Y - radius),
                diameter,
                diameter);

            spriteBatch.Draw(bumperImage, destination, Color.White);
        }
    }
}
