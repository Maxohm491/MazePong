using MazePong.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazePong {
    public class GameStateManager {

        public event EventHandler OnStateChange;

        readonly Stack<GameState> activeGameStates = new();
        private List<GameState> allGameStates = new();
        private Game Game;

        public void NewState(GameState state) {
            allGameStates.Add(state);
        }

        public GameState CurrentState {
            get { return activeGameStates.Peek(); }
        }

        public GameStateManager(Game game){
            Game = game;
        }

        public void LoadContent() {
            foreach (GameState state in allGameStates) {
                state.LoadContent();
            }
        }

        public void Update(GameTime gameTime) {
            foreach (var state in allGameStates) {
                if (activeGameStates.Contains(state)) {
                    state.Update(gameTime);
                }
            }
        }

        public void Draw(GameTime gameTime) {
            foreach (var state in allGameStates) {
                if (activeGameStates.Contains(state)) {
                    state.Draw(gameTime);
                }
            }
        }

        public void PopState() {
            if (activeGameStates.Count > 0) {
                RemoveState();
                OnStateChange?.Invoke(this, null);
            }
        }

        private void RemoveState() {
            GameState State = activeGameStates.Peek();
            OnStateChange -= State.StateChange;
            activeGameStates.Pop();
        }

        public void PushState(GameState newState) {
            AddState(newState);

            OnStateChange?.Invoke(this, null);
        }

        private void AddState(GameState newState) {
            activeGameStates.Push(newState);

            newState.Initialize();

            OnStateChange += newState.StateChange;
        }

        public void ChangeState(GameState newState) {
            while (activeGameStates.Count > 0)
                RemoveState();

            AddState(newState);

            OnStateChange?.Invoke(this, null);
        }
    }

}
