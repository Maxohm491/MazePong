﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static System.Net.Mime.MediaTypeNames;

namespace MazePong.Controls {
    public class Label : Control {

        public override void Update(GameTime gameTime) {}

        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.DrawString(SpriteFont, Text, Position, Color);
        }

        public override void HandleInput() {}
    }
}