using MazePong.Controls;
using MazePong.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace MazePong.GravityObjects {
    public class GravityBall : Ball {
        private readonly float wellMass = 2f; // 2000

        public GravityBall(Texture2D image, Vector2 startPosition, Vector2 startVelocity) : base(image, startPosition, startVelocity) {
        }

        public void FeelGravity(Point? wellLocation) {
            if (wellLocation.HasValue) {
                Vector2 wellToBall = position - Helper.PointToVector(wellLocation.Value);

                velocity -= Vector2.Normalize(wellToBall) * (wellMass); //  / wellToBall.Length()
            }
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


