using System;
using Game.Core;
namespace Game.Managers {
	public class GameManager : Singleton<GameManager> {
		public GameState CurrentGameState { get; private set; } = GameState.None;
		public event Action<GameState> BeforeGameStateChanged = delegate { };
		public event Action<GameState> AfterGameStateChanged = delegate { };

		public void ChangeGameState(GameState gameStateToChange) {

			if (CurrentGameState == gameStateToChange) {
				return;
			}

			BeforeGameStateChanged?.Invoke(CurrentGameState);
			CurrentGameState = gameStateToChange;
			AfterGameStateChanged?.Invoke(CurrentGameState);

			switch (CurrentGameState) {
				case GameState.Setup:
					break;
			}

		}
		public enum GameState {
			None = 0,
			Setup = 1,
			GenerateGrid = 2,
		}
	}
}

