using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazePong.Helpers {
    public class FontHelper {
        public static Dictionary<int, SpriteFont> SpriteFonts = new();
        public static SpriteFont tempFont;

        public void LoadContent(Game Game) {
            //if (SpriteFonts.Count != 0) { return; }

            //SpriteFonts[20] = Game.Content.Load<SpriteFont>(@"Fonts/ControlFont");
            //SpriteFonts[39] = Game.Content.Load<SpriteFont>(@"Fonts/ControlFont39");
            //SpriteFonts[64] = Game.Content.Load<SpriteFont>(@"Fonts/ControlFont64");
            tempFont = Game.Content.Load<SpriteFont>(@"Fonts/tempfont");
        }

        public static SpriteFont GetFont(int id) {
            return tempFont;

            if(SpriteFonts.TryGetValue(id, out var font)) { return font; }

            throw new InvalidOperationException("Requested font is not loaded");
        }
    }
}
