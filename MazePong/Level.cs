using MazePong.Controls;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazePong {
    public abstract class Level {
        protected DrawableGroup objects;
        protected Game Game;

        public DrawableGroup Objects { get { return objects; } }

        public Level(Game game) {
            this.Game = game;
            objects = new();
        }

        public abstract void LoadContent();
        public abstract void UnloadLevel();
        public abstract void Initialize();
        public abstract void Update();
        public abstract void CheckCollisions(Vector2 ballPosition);
    }
}
