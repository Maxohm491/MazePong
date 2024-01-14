using MazePong.Controls;
using MazePong.GravityObjects;
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
    public class GravityPhysicsManager(Game game, UIManager controlManager) : PhysicsManager(game, controlManager) {
        private ContentManager Content;
        private Texture2D ballImage;
        private GravityBall ball;
        private GravityWells wells;
        private SoundEffect wallHitSound;
        private Song gravitySong;

        public override void LoadContent() {
            Content = Game.Content;

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

        public override void RemoveControls() {
            ControlManager.Remove(ball);
        }

        public override Ball GetBall() {
            return ball;
        }

        public override void Initialize() {
            ControlManager.Remove(ball);

            ball = new(ballImage, new Vector2(Settings.BaseWidth / 2, Settings.BaseHeight / 2), new Vector2(0, 0));
            ball.WallHit += Ball_WallHit;
            ControlManager.Add(ball);

            Game.IsMouseVisible = true;
        }

        public override void Update(GameTime gameTime) {
            wells.HandleInput();

            ball.FeelGravity(wells.WellLocation);
        }

        public override void Draw(GameTime gameTime) {
            wells.Draw(Game.Services.GetService<SpriteBatch>());
        }
    }
}
