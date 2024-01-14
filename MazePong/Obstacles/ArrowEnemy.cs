using MazePong.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazePong.Obstacles {
    public class ArrowEnemy : Drawable {
        private readonly Vector2 velocity;
        private readonly int offscreenBuffer = 100;
        private readonly float rotation;
        private Texture2D image;
        public event EventHandler OutOfBounds;

        public ArrowEnemy(Vector2 velocity, Texture2D image) {
            this.velocity = velocity;
            this.image = image;
            rotation = (float) Math.Atan2(velocity.Y, velocity.X);
        }

        private void CheckBounds() {
            if (position.X > Settings.BaseWidth + offscreenBuffer ||
                position.X < 0 - offscreenBuffer ||
                position.Y > Settings.BaseHeight + offscreenBuffer ||
                position.Y < 0 - offscreenBuffer) {

                OutOfBounds?.Invoke(this, null);
            }
        }

        public override void Update(GameTime gameTime) {
            position += velocity;
            CheckBounds();
        }

        public override void Draw(SpriteBatch spriteBatch) {
            Rectangle destination = new(
                (int) position.X - (image.Width / 2),
                (int) position.Y - (image.Height / 2),
                image.Width,
                image.Height);

            spriteBatch.Draw( 
                image,
                destination,
                null,
                Color.White,
                rotation,
                new Vector2(image.Width / 2, image.Height / 2),
                SpriteEffects.None,
                0);
        }

        public override void HandleInput() { }

    }
}
