using System;
using System.Xml.Serialization;
using MazePong.Controls;
using MazePong.GravityObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MazePong.GameStates {
    public interface IGravityGameState {
        GameState Tag { get; }
    }

    public class GravityGameState : GameState, IGravityGameState {
        Texture2D backgroundImage;
        Texture2D ballImage;
        private ContentManager Content;
        private GravityBall ball;
        private GravityWells wells;
        private SoundEffect wallHitSound;
        private Song gravitySong;

        public GravityGameState(Game game) : base(game) {
            Game.Services.AddService<IGravityGameState>(this);
        }

        public override void LoadContent() {
            base.LoadContent();

            Content = Game.Content;

            backgroundImage = Content.Load<Texture2D>(@"Backgrounds/ingamebackground");
            Texture2D wellImage = Content.Load<Texture2D>(@"bumper");
            ballImage = Content.Load<Texture2D>(@"ballimage");
            wallHitSound = Content.Load<SoundEffect>(@"Sounds/wallhit");
            gravitySong = Content.Load<Song>(@"Sounds/gravity");

            MediaPlayer.Play(gravitySong);
            MediaPlayer.Pause();
            MediaPlayer.Volume = 0.5f;
            MediaPlayer.IsRepeating = true;

            ball = new(ballImage, new Vector2(Settings.BaseWidth / 2, Settings.BaseHeight / 2), new Vector2(3, 3));
            ball.WallHit += Ball_WallHit;
            ControlManager.Add(ball);

            wells = new(wellImage);
            wells.WellCreated += GravityWells_WellCreated;
            wells.WellDestroyed += GravityWells_WellDestroyed;

            LoadUI();
        }

        private void GravityWells_WellCreated(object sender, EventArgs e) {
            MediaPlayer.Resume();
        }

        private void GravityWells_WellDestroyed(object sender, EventArgs e) {
            MediaPlayer.Pause();
        }

        private void Ball_WallHit(object sender, EventArgs e) {
            wallHitSound.Play();
        }

        public override void Initialize() {
            base.Initialize();

            ControlManager.Remove(ball);

            ball = new(ballImage, new Vector2(Settings.BaseWidth / 2, Settings.BaseHeight / 2), new Vector2(0, 0));
            ball.WallHit += Ball_WallHit;
            ControlManager.Add(ball);

            Game.IsMouseVisible = true;
        }

        public void LoadUI() {
        }

        public override void Update(GameTime gameTime) {
            wells.HandleInput();

            ball.FeelGravity(wells.WellLocation);

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                MainMenu();

            base.Update(gameTime);
        }

        public void MainMenu() {
            TitleState state = (TitleState)Game.Services.GetService<ITitleState>();
            StateManager.ChangeState(state);
        }

        public override void Draw(GameTime gameTime) {
            SpriteBatch spriteBatch = Game.Services.GetService<SpriteBatch>();

            spriteBatch.Draw(
                backgroundImage,
                Settings.BaseRectangle,
                Color.White
            );

            wells.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}