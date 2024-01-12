using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MazePong.Controls {
    public abstract class Control {
        protected string name;
        protected string text;
        protected Vector2 size;
        protected Vector2 position;
        protected object value;
        protected bool enabled;
        protected bool visible;
        protected SpriteFont spriteFont;

        protected Color color;
        protected string type;

        public event EventHandler Selected;

        public string Name {
            get { return name; }
            set { name = value; }
        }
        public string Text {
            get { return text; }
            set { text = value; }
        }
        public Vector2 Size {
            get { return size; }
            set { size = value; }
        }
        public Vector2 Position {
            get { return position; }
            set {
                position = value;
                position.Y = (int)position.Y; // Because text likes to be displayed only at integer Y
            }
        }
        public object Value {
            get { return value; }
            set { this.value = value; }
        }
        public bool Enabled {
            get { return enabled; }
            set { enabled = value; }
        }
        public bool Visible {
            get { return visible; }
            set { visible = value; }
        }
        public SpriteFont SpriteFont {
            get { return spriteFont; }
            set { spriteFont = value; }
        }
        public Color Color {
            get { return color; }
            set { color = value; }
        }
        public string Type {
            get { return type; }
            set { type = value; }
        }

        public Control() {
            Color = Color.Black;
            Enabled = true;
            Visible = true;
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void HandleInput();

        protected virtual void OnSelected(EventArgs e) {
            Selected?.Invoke(this, e);
        }
    }
}