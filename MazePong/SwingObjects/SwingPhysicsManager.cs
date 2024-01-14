using MazePong.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazePong.SwingObjects {
    public class SwingPhysicsManager(Game game, UIManager controlManager) : PhysicsManager(game, controlManager){
        Texture2D ballImage;
        private ContentManager Content;
        private SwingBall ball;
        private SwingPoints swings;
        private SoundEffect wallHitSound;
        private SoundEffect flippedSound;
        private Song gravitySong;

        public override void LoadContent() {
            Content = Game.Content;

            Texture2D wellImage = Content.Load<Texture2D>(@"bumper");
            ballImage = Content.Load<Texture2D>(@"ballimage");
            wallHitSound = Content.Load<SoundEffect>(@"Sounds/wallhit");
            flippedSound = Content.Load<SoundEffect>(@"Sounds/hit");
            gravitySong = Content.Load<Song>(@"Sounds/gravity");

            MediaPlayer.Play(gravitySong);
            MediaPlayer.Pause();
            MediaPlayer.Volume = 0.5f;
            MediaPlayer.IsRepeating = true;

            ball = new(ballImage, new Vector2(Settings.BaseWidth / 2, Settings.BaseHeight / 2));
            ball.WallHit += Ball_WallHit;
            ball.FlippedDirection += Ball_FlippedDirection;
            ControlManager.Add(ball);

            swings = new(wellImage);
            swings.SwingCreated += SwingPoint_WellCreated;
            swings.SwingDestroyed += SwingPoints_WellDestroyed;
            swings.SwingCreated += (sender, e) => { ball.SwingLocation = swings.SwingLocation; };
            swings.SwingDestroyed += (sender, e) => { ball.SwingLocation = swings.SwingLocation; };
        }

        private void SwingPoint_WellCreated(object sender, EventArgs e) {
            MediaPlayer.Resume();
        }

        private void SwingPoints_WellDestroyed(object sender, EventArgs e) {
            MediaPlayer.Pause();
        }

        private void Ball_WallHit(object sender, EventArgs e) {
            wallHitSound.Play();
        }

        private void Ball_FlippedDirection(object sender, EventArgs e) {
            flippedSound.Play();
        }

        public override Ball GetBall() {
            return ball;   
        }

        public override void RemoveControls() {
            ControlManager.Remove(ball);
        }

        public override void Initialize() {
            ControlManager.Remove(ball);

            ball = new(ballImage, new Vector2(Settings.BaseWidth / 2, Settings.BaseHeight / 2));
            ball.WallHit += Ball_WallHit;
            ball.FlippedDirection += Ball_FlippedDirection;
            ControlManager.Add(ball);

            Game.IsMouseVisible = true;
        }

        public override void Update(GameTime gameTime) {
            swings.HandleInput();
        }

        public override void Draw(GameTime gameTime) {
            swings.Draw(Game.Services.GetService<SpriteBatch>());
        }
    }
}
