using MazePong.Controls;
using MazePong.PongObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MazePong.SwingObjects {
    public class PongPhysicsManager(Game game, UIManager controlManager) : PhysicsManager(game, controlManager) {
        Texture2D ballImage;
        private ContentManager Content;
        private Bumper bumper;
        private PongBall ball;
        private SoundEffect hitSound;
        private SoundEffect wallHitSound;
        private float _frames;

        public override void LoadContent() {
            Content = Game.Content;

            Texture2D bumperImage = Content.Load<Texture2D>(@"bumper");
            ballImage = Content.Load<Texture2D>(@"ballimage");
            hitSound = Content.Load<SoundEffect>(@"Sounds/hit");
            wallHitSound = Content.Load<SoundEffect>(@"Sounds/wallhit");

            ball = new(ballImage, new Vector2(Settings.BaseWidth / 2 + 250, Settings.BaseHeight / 2), new Vector2(0, 0));
            ball.WallHit += Ball_WallHit;
            ControlManager.Add(ball);

            bumper = new(bumperImage);
            ControlManager.Add(bumper);
        }

        private void Ball_WallHit(object sender, EventArgs e) {
            wallHitSound.Play();
        }

        public override Ball GetBall() {
            return ball;
        }

        public override void RemoveControls() {
            ControlManager.Remove(ball);
            ControlManager.Remove(bumper);
        }

        public override void Initialize() {
            ControlManager.Remove(ball);

            ball = new(ballImage, new Vector2(Settings.BaseWidth / 2 + 250, Settings.BaseHeight / 2), new Vector2(0, 0));
            ball.WallHit += Ball_WallHit;
            ControlManager.Add(ball);

            Game.IsMouseVisible = false;
        }

        public void CheckCollisions() {
            _frames++;
            if (_frames < 10) { return; }

            Vector2 lastBumperToBall = ball.Position - bumper.LastPosition;
            Vector2 projection = Project(bumper.Velocity, lastBumperToBall);
            Vector2 bumperAtContact = bumper.Position;
            Vector2 closestDisplacement;

            if (projection.Length() > bumper.Velocity.Length() || Vector2.Dot(projection, bumper.Velocity) < 0) {
                closestDisplacement = ball.Position - bumper.Position;
            }
            else {
                closestDisplacement = lastBumperToBall - projection;
                if (projection.Length() != 0) {
                    if (lastBumperToBall.Length() <= ball.Radius + bumper.Radius) {
                        closestDisplacement = lastBumperToBall;
                        bumperAtContact = bumper.LastPosition;
                    }
                    else if (closestDisplacement.Length() <= ball.Radius + bumper.Radius) {
                        float distToClosest = (float)Math.Sqrt(
                            (float)Math.Pow(ball.Radius + bumper.Radius, 2) - closestDisplacement.LengthSquared());
                        bumperAtContact = bumper.LastPosition + (projection.Length() - distToClosest) * Vector2.Normalize(bumper.Velocity);
                    }
                }
            }

            if (closestDisplacement.Length() <= ball.Radius + bumper.Radius) {
                hitSound.Play();
                _frames = 0;
                ball.OnCollide(bumperAtContact, bumper.NormalizedVelocity, bumper.Radius, bumper.Position);
            }
        }

        private Vector2 Project(Vector2 projectedOnto, Vector2 toProject) {
            if (projectedOnto.Length() <= 0.001) {
                return new Vector2(0, 0);
            }
            float component = Vector2.Dot(projectedOnto, toProject) / projectedOnto.Length();
            return component * Vector2.Normalize(projectedOnto);
        }

        public override void Update(GameTime gameTime) {
            CheckCollisions();
        }

        public override void Draw(GameTime gameTime) {
        }
    }
}
