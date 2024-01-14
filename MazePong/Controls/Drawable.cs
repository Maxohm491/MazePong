using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazePong.Controls {
    public abstract class Drawable {
        protected bool enabled;
        protected bool visible;
        protected Vector2 position;

        public bool Enabled {
            get { return enabled; }
            set { enabled = value; }
        }
        public bool Visible {
            get { return visible; }
            set { visible = value; }
        }
        public Vector2 Position {
            get { return position; }
            set {
                position = value;
            }
        }

        public Drawable() { 
            Enabled = true;
            Visible = true;
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void HandleInput();
    }
}
