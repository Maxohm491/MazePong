using MazePong.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace MazePong.SwingObjects {
    public class SwingBall : Ball {
        private readonly float speed = 20f;
        public event EventHandler FlippedDirection;
        private Point? swingLocation;
        public Point? SwingLocation { set { swingLocation = value; } }

        public SwingBall(Texture2D image, Vector2 startPosition) : base(image, startPosition, new Vector2(20, 0)) {
        }

        public override void Update(GameTime gameTime) {
            if (swingLocation != null) {
                Vector2 swingVector = Helper.PointToVector(swingLocation.Value);
                Vector2 swingToBall = position - swingVector;
                Vector2 ballToSwing = -swingToBall;
                float angle = speed / swingToBall.Length();

                if (velocity.Y * ballToSwing.X > velocity.X * ballToSwing.Y) {
                    angle = -angle;
                }

                (float cos, float sin) = ((float)Math.Cos(angle), (float)Math.Sin(angle));

                Vector2 newSwingToBall = new(
                    cos * swingToBall.X - sin * swingToBall.Y,
                    sin * swingToBall.X + cos * swingToBall.Y);

                velocity = newSwingToBall - swingToBall;
                position += velocity;
                SwingingOutOfBoundsCheck(position);
            }

            else { base.Update(gameTime); }
        }

        public override void HandleInput() {
            if (InputManager.WasKeyPressed(Keys.Space)) {
                velocity = -velocity;
                FlippedDirection.Invoke(this, null);
            }
        }

        protected void SwingingOutOfBoundsCheck(Vector2 point) {
            if (point.X < radius + wallThickness || point.X > Settings.BaseWidth - radius - wallThickness ||
                point.Y < radius + wallThickness || point.Y > Settings.BaseHeight - radius - wallThickness) {
                InvokeWallHit();
                velocity = -velocity;
                position += velocity;
            }
        }

        public static Vector2 PerpendicularCW(Vector2 vector) {
            return Vector2.Normalize(new Vector2(vector.Y, -vector.X));
        }

        public static Vector2 PerpendicularCCW(Vector2 vector) {
            return Vector2.Normalize(new Vector2(-vector.Y, vector.X));
        }
    }
}
