using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MazePong.Helpers {
    public static class UIHelper {
        public enum RelativePoint {
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight,
            Center
        }

        public static Rectangle FittingRectangle {
            get {
                Rectangle renderTo;
                if ((16f / 9f) * Settings.TargetHeight < Settings.TargetWidth) {
                    renderTo = new(0, 0, (int)((16f / 9f) * Settings.TargetHeight), Settings.TargetHeight);
                }
                else {
                    renderTo = new(0, 0, Settings.TargetWidth, (int)((9f / 16f) * Settings.TargetWidth));
                }

                return RectangleHelper.CenterRectangles(Settings.TargetRectangle, renderTo);
            }
        }

        public static Vector2 EdgePosition(RelativePoint relativePoint, int edgeToEdge, Vector2 size) {
            int xPos;
            int yPos;
            int horizontalOffset = (int) (size.X / 2) + edgeToEdge;
            int verticalOffset = (int) (size.Y / 2) + edgeToEdge;

            switch (relativePoint) {
                case RelativePoint.TopLeft:
                    xPos = horizontalOffset;
                    yPos = verticalOffset;
                    break;
                case RelativePoint.TopRight:
                    xPos = Settings.TargetWidth - horizontalOffset;
                    yPos = verticalOffset;
                    break;
                case RelativePoint.BottomLeft:
                    xPos = horizontalOffset;
                    yPos = Settings.TargetHeight - verticalOffset;
                    break;
                case RelativePoint.BottomRight:
                    xPos = Settings.TargetWidth - horizontalOffset;
                    yPos = Settings.TargetHeight - verticalOffset;
                    break;
                case RelativePoint.Center:
                    xPos = (Settings.TargetWidth / 2) + horizontalOffset;
                    yPos = (Settings.TargetHeight / 2) - verticalOffset;
                    break;
                default:
                    return new Vector2(0, 0);
            }

            return new Vector2(xPos, yPos);
        }

        public static Vector2 EdgePosition(RelativePoint relativePoint, int toSide, int toTopOrBottom, Vector2 size) {
            int xPos;
            int yPos;
            int horizontalOffset = (int)(size.X / 2) + toSide;
            int verticalOffset = (int)(size.Y / 2) + toTopOrBottom;

            switch (relativePoint) {
                case RelativePoint.TopLeft:
                    xPos = horizontalOffset;
                    yPos = verticalOffset;
                    break;
                case RelativePoint.TopRight:
                    xPos = Settings.TargetWidth - horizontalOffset;
                    yPos = verticalOffset;
                    break;
                case RelativePoint.BottomLeft:
                    xPos = horizontalOffset;
                    yPos = Settings.TargetHeight - verticalOffset;
                    break;
                case RelativePoint.BottomRight:
                    xPos = Settings.TargetWidth - horizontalOffset;
                    yPos = Settings.TargetHeight - verticalOffset;
                    break;
                case RelativePoint.Center:
                    xPos = (Settings.TargetWidth / 2) + horizontalOffset;
                    yPos = (Settings.TargetHeight / 2) - verticalOffset;
                    break;
                default:
                    return new Vector2(0, 0);
            }

            return new Vector2(xPos, yPos);
        }

        public static Vector2 CenterPosition(RelativePoint relativePoint, int horizontalOffset, int verticalOffset) {
            int xPos;
            int yPos;

            switch (relativePoint) {
                case RelativePoint.TopLeft:
                    xPos = horizontalOffset;
                    yPos = verticalOffset;
                    break;
                case RelativePoint.TopRight:
                    xPos = Settings.TargetWidth - horizontalOffset;
                    yPos = verticalOffset;
                    break;
                case RelativePoint.BottomLeft:
                    xPos = horizontalOffset;
                    yPos = Settings.TargetHeight - verticalOffset;
                    break;
                case RelativePoint.BottomRight:
                    xPos = Settings.TargetWidth - horizontalOffset;
                    yPos = Settings.TargetHeight - verticalOffset;
                    break;
                case RelativePoint.Center:
                    xPos = (Settings.TargetWidth / 2) + horizontalOffset;
                    yPos = (Settings.TargetHeight / 2) - verticalOffset;
                    break;
                default:
                    return new Vector2(0, 0);
            }

            return new Vector2(xPos, yPos);
        }
    }
}
