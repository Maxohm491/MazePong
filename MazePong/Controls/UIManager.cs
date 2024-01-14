using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MazePong.Controls {
    public class UIManager : List<Drawable> {
        public void Update(GameTime gameTime, PlayerIndex playerIndex) {
            if (Count == 0)
                return;
            foreach (Drawable c in this) {
                if (c.Enabled) {
                    c.Update(gameTime);
                    c.HandleInput();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            foreach (Drawable c in this) {
                if (c.Visible) {
                    c.Draw(spriteBatch);
                }
            }
        }
    }
}