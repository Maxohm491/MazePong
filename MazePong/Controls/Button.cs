using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;
using MazePong.Helpers;

namespace MazePong.Controls {
    public class Button : Control {
        public event EventHandler Click;
        public event EventHandler Down;

        private readonly Texture2D _background;
        private float _frames;
        private readonly int fontSize = 20;

        public int Width { get { return _background.Width; } }
        public int Height { get { return _background.Height; } }
        public Rectangle ButtonRectangle;

        public Button(Texture2D background, Vector2 position) {
            _background = background;
            Size = new Vector2(background.Width, background.Height);
            Position = position;
            ButtonRectangle = new(
                (int)Position.X - (int) (Size.X / 2),
                (int)Position.Y - (int)(Size.Y / 2),
                (int)Size.X,
                (int)Size.Y);
        }

        public Button(Texture2D background, Vector2 position, int fontSize) : this(background, position) {
            this.fontSize = fontSize;
        }

        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(_background, new Vector2(ButtonRectangle.X, ButtonRectangle.Y), Color.White);


            if (Text != null) {
                spriteFont = FontHelper.GetFont(fontSize);

                Vector2 textSize = spriteFont.MeasureString(Text);
                Rectangle textRectangle = RectangleHelper.CenterRectangles(ButtonRectangle, new(0, 0, (int)textSize.X, (int)textSize.Y));

                spriteBatch.DrawString(
                    spriteFont,
                    Text,
                    new Vector2(textRectangle.X, textRectangle.Y),
                    Color);
            }
        }

        public override void HandleInput() {
            Point position = MouseHelper.Position;

            if (InputManager.WasMouseReleased(MouseButtons.Left) && _frames >= 5) {
                if (ButtonRectangle.Contains(position)) {
                    OnClick();
                    return;
                }
            }

            if (InputManager.TouchReleased() && _frames >= 5) {
                if (ButtonRectangle.Contains(InputManager.TouchReleasedAt)) {
                    OnClick();
                    return;
                }
            }

            if (InputManager.IsMouseDown(MouseButtons.Left)) {
                if (ButtonRectangle.Contains(InputManager.MouseAsPoint)) {
                    OnDown();
                    return;
                }
            }

            if (InputManager.TouchLocation != new Vector2(-1, -1)) {
                if (ButtonRectangle.Contains(InputManager.TouchLocation)) {
                    OnDown();
                    return;
                }
            }
        }

        private void OnDown() {
            Down?.Invoke(this, null);
        }

        private void OnClick() {
            _frames = 0;
            Click?.Invoke(this, null);
        }

        public override void Update(GameTime gameTime) {
            _frames++;
            HandleInput();
        }

        public void Show() {
            _frames = 0;
        }
    }
}