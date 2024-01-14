using MazePong.Controls;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazePong {
    public abstract class PhysicsManager {
        protected UIManager controls;
        protected Game Game { get; set; }
        public UIManager ControlManager { get { return controls; } }

        public PhysicsManager(Game game, UIManager controlManager) {
            Game = game;
            controls = controlManager;
        }

        public abstract void LoadContent();
        public abstract void Initialize();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);
        public abstract Ball GetBall();
        public abstract void RemoveControls();
    }
}
