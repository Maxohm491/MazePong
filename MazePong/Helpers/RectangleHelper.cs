using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazePong.Helpers {
    public static class RectangleHelper {
        public static Vector2 NearestInt(Vector2 vector2) {
            return new((int)vector2.X, (int)vector2.Y);
        }

        public static Rectangle CenterRectangles(Rectangle outerRectangle, Rectangle innerRectangle) {
            return new Rectangle(
                outerRectangle.X + (outerRectangle.Width / 2) - (innerRectangle.Width / 2),
                outerRectangle.Y + (outerRectangle.Height / 2) - (innerRectangle.Height / 2),
                innerRectangle.Width,
                innerRectangle.Height);
        }
    }
}