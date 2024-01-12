using MazePong.Controls;
using MazePong.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace MazePong.PongObjects {
    public class PongBall : Ball {

        public PongBall(Texture2D image, Vector2 startPosition, Vector2 startVelocity) : base(image, startPosition, startVelocity) {
        }

        public void OnCollide(Vector2 otherPosition, Vector2 otherVelocity, float otherRadius, Vector2 otherCurr) { 
            Vector2 displacement = position - otherPosition;
            float temp = Vector2.Dot(velocity - otherVelocity, displacement) / displacement.Length();

            //Vector2 toSurface = Vector2.Normalize(displacement) * (otherRadius + radius + 5);
            //position = otherCurr + toSurface;

            velocity -= (Vector2.Normalize(displacement) * temp * 2f);
        }

        public override void HandleInput() { }
        
    }
}


