using MazePong.Controls;
using MazePong.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace MazePong {
    public abstract class Ball : Control {
        protected Vector2 velocity;
        public event EventHandler WallHit;

        protected readonly int radius;
        public int Radius { get { return radius; } }
        protected readonly int diameter;
        protected readonly Texture2D ballImage;
        protected readonly int wallThickness = 20;
        protected readonly float maxSpeed = 30f;

        public Ball(Texture2D image, Vector2 startPosition, Vector2 startVelocity) {
            ballImage = image;
            radius = image.Width / 2;
            diameter = image.Width;

            velocity = startVelocity;
            position = startPosition;
        }

        protected virtual void OutOfBoundsCheck(Vector2 point) {
            if (point.X < radius + wallThickness || point.X > Settings.BaseWidth - radius - wallThickness) {
                position.X = Math.Clamp(position.X, radius + wallThickness, Settings.BaseWidth - radius - wallThickness);
                velocity.X = -velocity.X;
                WallHit?.Invoke(this, null);
            }
            if (point.Y < radius + wallThickness || point.Y > Settings.BaseHeight - radius - wallThickness) {
                Math.Clamp(position.Y, radius + wallThickness, Settings.BaseHeight - radius - wallThickness);
                velocity.Y = -velocity.Y;
                WallHit?.Invoke(this, null);
            } 
        }

        public override void HandleInput() { }

        public override void Update(GameTime gameTime) {
            float speed = velocity.Length();
            if (speed > maxSpeed) {
                velocity *= maxSpeed / speed;
            }

            OutOfBoundsCheck(position + velocity);

            position += velocity;
        }

        protected void InvokeWallHit() { 
            WallHit?.Invoke(this, null);
        }

        public override void Draw(SpriteBatch spriteBatch) {
            Rectangle destination = new(
                (int)(position.X - radius),
                (int)(position.Y - radius),
                diameter,
                diameter);

            spriteBatch.Draw(ballImage, destination, Color.Gray);
        }
    }
}


