using Microsoft.Xna.Framework;
using System.ComponentModel;

namespace MazePong {
    public class Settings {
        public const int BaseWidth = 1920;
        public const int BaseHeight = 1080;

        public static Rectangle BaseRectangle {
            get {
                return new(0, 0, BaseWidth, BaseHeight);
            }
        }

        public static Rectangle TargetRectangle {
            get {
                return new(0, 0, TargetWidth, TargetHeight);
            }
        }

        public static Rectangle RenderedRectangle { get; set; } = TargetRectangle;

        public static Vector2 CenterPoint {
            get {
                return new Vector2((int) (BaseWidth / 2), (int) (BaseHeight / 2));
            }
        }

        public static int TargetWidth { get; set; } = BaseWidth;
        public static int TargetHeight { get; set; } = BaseHeight;
    }
}
