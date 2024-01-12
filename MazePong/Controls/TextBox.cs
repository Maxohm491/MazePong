using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;
using MazePong.Helpers;

namespace MazePong.Controls {
    public class TextBox : Control {
        private readonly Texture2D _background;
        private readonly int fontSize = 20;

        public int Width { get { return _background.Width; } }
        public int Height { get { return _background.Height; } }
        public Rectangle BackgroudRectangle;

        public TextBox(Texture2D background, Vector2 position) {
            _background = background;
            Size = new Vector2(background.Width, background.Height);
            Position = position;
            BackgroudRectangle = new(
                (int)Position.X - (int)(Size.X / 2),
                (int)Position.Y - (int)(Size.Y / 2),
                (int)Size.X,
                (int)Size.Y);
        }

        public TextBox(Vector2 position) {
            Position = position;
            BackgroudRectangle = new(
               (int)Position.X - 1,
               (int)Position.Y - 1,
               2,
               2);
        }

        public TextBox(Texture2D background, Vector2 position, int fontSize) : this(background, position) {
            this.fontSize = fontSize;
        }

        public TextBox(Vector2 position, int fontSize) : this(position) {
            this.fontSize = fontSize;
        }

        public override void Draw(SpriteBatch spriteBatch) {
            if (_background != null) {
                spriteBatch.Draw(
                    _background,
                    new Vector2(BackgroudRectangle.X, BackgroudRectangle.Y),
                    Color.White);
            }

            if (Text != null) {
                spriteFont = FontHelper.GetFont(fontSize);

                Vector2 textSize = spriteFont.MeasureString(Text);
                Rectangle textRectangle = RectangleHelper.CenterRectangles(BackgroudRectangle, new(0, 0, (int)textSize.X, (int)textSize.Y));

                spriteBatch.DrawString(
                    spriteFont,
                    Text,
                    new Vector2(textRectangle.X, textRectangle.Y),
                    Color);
            }
        }

        public override void Update(GameTime gameTime) { }
        public override void HandleInput() { }
    }
}