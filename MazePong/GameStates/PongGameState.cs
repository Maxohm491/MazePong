﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MazePong.Controls;
using MazePong.Helpers;
using MazePong.PongObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MazePong.GameStates {
    public interface IPongGameState {
        GameState Tag { get; }
    }

    public class PongGameState : GameState, IPongGameState {
        Texture2D backgroundImage;
        Texture2D ballImage;
        private int totalItems;
        private ContentManager Content;
        private Bumper bumper;
        private PongBall ball;
        private SoundEffect hitSound;
        private SoundEffect wallHitSound;

        private float _frames;

        public int TotalItems {
            get { return totalItems; }
            set { totalItems = value; }
        }

        public PongGameState(Game game) : base(game) {
            Game.Services.AddService<IPongGameState>(this);
        }

        public override void LoadContent() {
            base.LoadContent();

            Content = Game.Content;

            backgroundImage = Content.Load<Texture2D>(@"Backgrounds/ingamebackground");
            Texture2D bumperImage = Content.Load<Texture2D>(@"bumper");
            ballImage = Content.Load<Texture2D>(@"ballimage");
            hitSound = Content.Load<SoundEffect>(@"Sounds/hit");
            wallHitSound = Content.Load<SoundEffect>(@"Sounds/wallhit");

            ball = new(ballImage, new Vector2(Settings.BaseWidth / 2 + 250, Settings.BaseHeight / 2), new Vector2(0, 0));
            ball.WallHit += Ball_WallHit;
            ControlManager.Add(ball);

            bumper = new(bumperImage);
            ControlManager.Add(bumper);

            LoadUI();
        }

        private void Ball_WallHit(object sender, EventArgs e) {
            wallHitSound.Play();
        }

        public override void Initialize() {
            base.Initialize();
            
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

        public void LoadUI() {
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            CheckCollisions();

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                MainMenu();
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

            base.Draw(gameTime);
        }
    }
}