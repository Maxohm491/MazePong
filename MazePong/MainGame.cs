using MazePong.GameStates;
using MazePong.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MazePong {
    public class MainGame : Game {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public GameStateManager GameStateManager { get; private set; }
        private RenderTarget2D _renderTarget;

        private readonly TitleState _titleState;
        private readonly IngameState _ingameState;

        public MainGame() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = Settings.TargetWidth;
            _graphics.PreferredBackBufferHeight = Settings.TargetHeight;

            Components.Add(new InputManager(this));

            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromSeconds(1d / 60d); //60);

            GameStateManager = new GameStateManager(this);
            Services.AddService(typeof(GameStateManager), GameStateManager);

            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += OnResize;

            _titleState = new(this);
            _ingameState = new(this);

            GameStateManager.NewState(_titleState);
            GameStateManager.NewState(_ingameState);
        }

        protected override void Initialize() {
            base.Initialize();

            GameStateManager.ChangeState(_titleState);
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _renderTarget = new RenderTarget2D(
                GraphicsDevice,
                GraphicsDevice.PresentationParameters.BackBufferWidth,
                GraphicsDevice.PresentationParameters.BackBufferHeight,
                false,
                GraphicsDevice.PresentationParameters.BackBufferFormat,
                DepthFormat.Depth24);

            Settings.RenderedRectangle = UIHelper.FittingRectangle;

            new FontHelper().LoadContent(this);

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(typeof(SpriteBatch), _spriteBatch);

            GameStateManager.LoadContent();
        }

        protected override void Update(GameTime gameTime) {
            GameStateManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.SetRenderTarget(_renderTarget);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            GameStateManager.Draw(gameTime);
            _spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            _spriteBatch.Draw(_renderTarget, Settings.RenderedRectangle, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void OnResize(Object sender, EventArgs e) {
            if ((_graphics.PreferredBackBufferWidth != _graphics.GraphicsDevice.Viewport.Width) ||
                (_graphics.PreferredBackBufferHeight != _graphics.GraphicsDevice.Viewport.Height)) {
                _graphics.PreferredBackBufferWidth = _graphics.GraphicsDevice.Viewport.Width;
                _graphics.PreferredBackBufferHeight = _graphics.GraphicsDevice.Viewport.Height;
                _graphics.ApplyChanges();

                Settings.TargetWidth = _graphics.PreferredBackBufferWidth;
                Settings.TargetHeight = _graphics.PreferredBackBufferHeight;

                Settings.RenderedRectangle = UIHelper.FittingRectangle;
            }
        }
    }
}
