using System;
using System.Diagnostics;
using MazePong.Controls;
using MazePong.Helpers;
using MazePong.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MazePong.GameStates {

    public interface ITitleState {
        GameState Tag { get; }
    }

    public class TitleState : GameState, ITitleState {
        Texture2D backgroundImage;

        public TitleState(Game game) : base(game) {
            Game.Services.AddService<ITitleState>(this);
        }

        public override void LoadContent() {
            base.LoadContent();
            ContentManager Content = Game.Content;
            SpriteBatch = Game.Services.GetService<SpriteBatch>();

            backgroundImage = Content.Load<Texture2D>(@"Backgrounds/titlebackground");
            Texture2D startButtonImage = Content.Load<Texture2D>(@"Buttons/startbutton");

            Button pongButton = new(startButtonImage, Settings.CenterPoint, 39) {
                Text = "Pong",
                Color = Color.Black,
            };
            pongButton.Click += PongButton_Selected;
            ControlManager.Add(pongButton);

            Button gravityGame = new(startButtonImage, UIHelper.CenterPosition(UIHelper.RelativePoint.Center, 0, -Settings.BaseHeight / 4), 39) {
                Text = "Gravity",
                Color = Color.Black,
            };
            gravityGame.Click += GravityButton_Selected;
            ControlManager.Add(gravityGame);

            Button swingGame = new(startButtonImage, UIHelper.CenterPosition(UIHelper.RelativePoint.Center, 0, Settings.BaseHeight / 4)) {
                Text = "Swing",
                Color = Color.Black,
            };
            swingGame.Click += SwingButton_Selected;
            ControlManager.Add(swingGame);
        }

        private void PongButton_Selected(object sender, EventArgs e) {
            IngameState state = (IngameState)Game.Services.GetService<IIngameState>();
            state.SetLevel(new AvoidanceLevel(Game));
            state.SetPhysicsType(IngameState.PhysicsType.Pong);

            StateManager.ChangeState(state);
        }

        private void SwingButton_Selected(object sender, EventArgs e) {
            IngameState state = (IngameState)Game.Services.GetService<IIngameState>();
            state.SetPhysicsType(IngameState.PhysicsType.Swing);

            StateManager.ChangeState(state);
        }

        private void GravityButton_Selected(object sender, EventArgs e) {
            IngameState state = (IngameState)Game.Services.GetService<IIngameState>();
            state.SetPhysicsType(IngameState.PhysicsType.Gravity);

            StateManager.ChangeState(state);
        }

        public override void Initialize() {
            Game.IsMouseVisible = true;

            base.Initialize();
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
            SpriteBatch spriteBatch = Game.Services.GetService<SpriteBatch>();

            spriteBatch.Draw(
                backgroundImage,
                Settings.BaseRectangle,
                Color.White
            );

            base.Draw(gameTime);
        }
    }
}
