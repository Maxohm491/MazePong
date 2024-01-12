using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace MazePong.Helpers {
    public static class MouseHelper {
        public static Point Position {
            get {
                MouseState mouse = Mouse.GetState();
                return VectorToPoint(ScreenToRender(new Vector2(mouse.X, mouse.Y)));
            }
        }

        public static Point OnScreenPosition { get { 
            Point position = Position;
            return new Point(
                Math.Clamp(position.X, 0, Settings.BaseWidth),
                Math.Clamp(position.Y, 0, Settings.BaseHeight));
        } }

        private static Point VectorToPoint(Vector2 vector) {
            return new Point((int) vector.X, (int) vector.Y);
        }

        private static Vector2 ScreenToRender(Vector2 point) {
            return new Vector2(
                ((float) (point.X - Settings.RenderedRectangle.X) / Settings.RenderedRectangle.Width) * Settings.BaseRectangle.Width + Settings.BaseRectangle.X,
                ((float) (point.Y - Settings.RenderedRectangle.Y) / Settings.RenderedRectangle.Height) * Settings.BaseRectangle.Height + Settings.BaseRectangle.Y);
        } 
    }
}
