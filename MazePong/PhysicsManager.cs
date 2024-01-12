using MazePong.Controls;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazePong {
    public abstract class PhysicsManager {
        protected ControlManager controls;
        protected Game Game { get; set; }
        public ControlManager ControlManager { get { return controls; } }

        public PhysicsManager(Game game, ControlManager controlManager) {
            Game = game;
            controls = controlManager;
        }

        public abstract void LoadContent();
        public abstract void Initialize();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);

        public abstract void RemoveControls();
    }
}
