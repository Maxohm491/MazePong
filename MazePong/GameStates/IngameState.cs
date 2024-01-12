using System;
using System.Reflection.Metadata;
using System.Xml.Serialization;
using MazePong.Controls;
using MazePong.GravityObjects;
using MazePong.SwingObjects;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using static MazePong.Helpers.UIHelper;

namespace MazePong.GameStates {
    public interface IIngameState {
        GameState Tag { get; }
    }

    public class IngameState : GameState, IIngameState {
        private Texture2D backgroundImage;
        private PhysicsManager physicsManager;

        public enum PhysicsType { 
            Swing,
            Gravity,
            Pong
        };

        public IngameState(Game game) : base(game) {
            Game.Services.AddService<IIngameState>(this);
        }

        public override void LoadContent() {
            base.LoadContent();

            backgroundImage = Game.Content.Load<Texture2D>(@"Backgrounds/ingamebackground");
        }

        public override void Initialize() {
            base.Initialize();

            physicsManager.Initialize();
        }

        public override void Update(GameTime gameTime) {
            physicsManager.Update(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                MainMenu();

            base.Update(gameTime);
        }

        public void MainMenu() {
            TitleState state = (TitleState)Game.Services.GetService<ITitleState>();
            StateManager.ChangeState(state);
        }

        public void SetPhysicsType(PhysicsType type) {
            physicsManager?.RemoveControls();

            switch (type) {
                case PhysicsType.Swing:
                    physicsManager = new SwingPhysicsManager(Game, ControlManager);
                    break;
                case PhysicsType.Gravity:
                    physicsManager = new GravityPhysicsManager(Game, ControlManager);
                    break;
                case PhysicsType.Pong:
                    physicsManager = new PongPhysicsManager(Game, ControlManager); 
                    break;
            }

            physicsManager.LoadContent();
        }

        public override void Draw(GameTime gameTime) {
            SpriteBatch spriteBatch = Game.Services.GetService<SpriteBatch>();

            spriteBatch.Draw(
                backgroundImage,
                Settings.BaseRectangle,
                Color.White
            );

            physicsManager.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}