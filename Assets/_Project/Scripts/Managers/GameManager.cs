using UnityEngine;
using System;
using Game.Core;

namespace Game.Managers {
	public class GameManager : Singleton<GameManager> {
		public GameState GameState { get; private set; }
		public event Action<GameState> BeforeGameStateChanged = delegate { };
		public event Action<GameState> AfterGameStateChanged = delegate { };
		
		protected override void Awake() {
			base.Awake();
		}
		
		private void Start() => ChangeGameState(GameState.SetUp);
		public void ChangeGameState(GameState newState) {
			if (GameState == newState) {
				Debug.LogWarning($"Ignoring redundant state change: {newState}");
				return;
			}
			BeforeGameStateChanged?.Invoke(newState);
			GameState = newState;
			switch (newState) {
				case GameState.SetUp:
					break;
				case GameState.GenerateGrid:
					break;
				case GameState.SpawnHeroes:
					break;
				case GameState.SpawnEnemies:
					break;
				case GameState.PlayerTurn:
					break;
				case GameState.EnemyTurn:
					break;
			}
			AfterGameStateChanged?.Invoke(newState);
		}

	}
}

[Serializable]
public enum GameState {
	None = 0,
	SetUp = 1,
	GenerateGrid = 2,
	SpawnHeroes = 3,
	SpawnEnemies = 4,
	PlayerTurn = 5,
	EnemyTurn = 6,
}


