using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazePong.Controls {
    public class DrawableGroup : Drawable {
        private List<Drawable> drawables = new();

        public void Add(Drawable drawable) {
            drawables.Add(drawable);
        }

        public void Remove(Drawable drawable) {
            drawables.Remove(drawable);
        }

        public override void Update(GameTime gameTime) {
            foreach (Drawable drawable in drawables) {
                drawable.Update(gameTime);
            }
        }

        public override void HandleInput() {
            foreach (Drawable drawable in drawables) {
                drawable.HandleInput();
            }
        }

        public override void Draw(SpriteBatch spriteBatch) {
            foreach (Drawable drawable in drawables) {
                drawable.Draw(spriteBatch);
            }
        }

    }
}
