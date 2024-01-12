using System;
using System.Collections.Generic;
using MazePong.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MazePong.GameStates {

    public abstract partial class GameState {
        protected SpriteBatch SpriteBatch { get; set; }
        protected ControlManager controls;
        protected Game Game { get; set; }
        public ControlManager ControlManager { get { return controls; } }
        private bool Enabled;
        private bool Visible;

        readonly GameState tag;
        public GameState Tag {
            get { return tag; }
        }

        protected GameStateManager StateManager;

        public GameState(Game game) {
            Game = game;
            tag = this;
        }

        public virtual void Initialize() {
        }

        public virtual void LoadContent() {
            SpriteBatch = Game.Services.GetService<SpriteBatch>();
            controls = new();
        }

        public virtual void Update(GameTime gameTime) {
            ControlManager.Update(gameTime, PlayerIndex.One);
        }

        public virtual void Draw(GameTime gameTime) {
            ControlManager.Draw(Game.Services.GetService<SpriteBatch>());
        }

        internal protected virtual void StateChange(object sender, EventArgs e) {
            StateManager ??= Game.Services.GetService<GameStateManager>();

            if (StateManager.CurrentState == Tag)
                Show();
            else
                Hide();
        }

        protected virtual void Show() {
            Visible = true;
            Enabled = true;
        }

        protected virtual void Hide() {
            Visible = false;
            Enabled = false;
        }

    }
}