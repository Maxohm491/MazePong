using MazePong.Controls;
using MazePong.Obstacles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace MazePong.Levels {
    public class AvoidanceLevel(Game game) : Level(game) {
        private readonly int spawnRate = 30;
        private int spawnTimer;
        private ContentManager Content;
        private Texture2D arrowImage;
        private Stack<Drawable> toRemove = new();

        public override void LoadContent() {
            Content = Game.Content;

            arrowImage = Content.Load<Texture2D>(@"arrowhead");
        }

        public override void Initialize() {
            spawnTimer = spawnRate;
        }

        public override void Update() { 
            spawnTimer -= 1;
            if (spawnTimer > 0) { return; }
            spawnTimer = spawnRate;

            ArrowEnemy arrow = new(new Vector2(3, 3), arrowImage);
            arrow.OutOfBounds += Arrow_OutOfBounds;
            objects.Add(arrow);

            while(toRemove.Count > 0) {
                objects.Remove(toRemove.Pop());
            }
        }

        public override void CheckCollisions(Vector2 ballPosition) { }

        public override void UnloadLevel() { }

        public void Arrow_OutOfBounds(object arrow, EventArgs e) { 
            toRemove.Push((Drawable) arrow);
        }
    }
}
