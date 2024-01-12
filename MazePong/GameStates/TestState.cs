using System;
using System.Diagnostics;
using MazePong.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MazePong.GameStates {
    public interface ITestState{
        GameState Tag { get; }
    }

    public class TestState : GameState, ITestState {
        Texture2D backgroundImage;
        Texture2D startButtonImage;


        public TestState(Game game) : base(game) {
            Game.Services.AddService<ITestState>(this);
        }

        public override void LoadContent() {
            base.LoadContent();

            ContentManager Content = Game.Content;

            backgroundImage = Content.Load<Texture2D>(@"Backgrounds/secondscreen");
            startButtonImage = Content.Load<Texture2D>(@"Buttons/testbutton");


            Button backButton = new(startButtonImage, new Vector2(Settings.TargetWidth / 2, Settings.TargetHeight / 2)) {
                Text = "Back",
                Color = Color.Black,
            };

            backButton.Click += BackButton_Selected;

            ControlManager.Add(backButton);
        }

        private void BackButton_Selected(object sender, EventArgs e) {
            TitleState state = (TitleState)Game.Services.GetService<ITitleState>().Tag;

            StateManager.ChangeState(state);
        }

        public override void Initialize() {
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