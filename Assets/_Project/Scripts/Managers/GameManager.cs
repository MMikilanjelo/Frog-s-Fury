using System;

using UnityEngine;

using Game.Core;
using Game.Core.Logic;
using Game.Entities.Characters;

namespace Game.Managers {
	public class GameManager : Singleton<GameManager> {
		public GameState GameState { get; private set; }
		public event Action<GameState> BeforeGameStateChanged = delegate { };
		public event Action<GameState> AfterGameStateChanged = delegate { };

		private DelegateStateMachine stateMachine_;

		protected override void Awake() {
			base.Awake();
			stateMachine_ = new DelegateStateMachine();

			AddGameState(GameState.SET_UP, SetUpState);
			AddGameState(GameState.GENERATE_GRID, GenerateGridState);
			AddGameState(GameState.SPAWN_HEROES, SpawnHeroesState);
			AddGameState(GameState.PLAYER_TURN, PlayerTurnState);
			AddGameState(GameState.ENEMY_TURN, EnemyTurnState);
		}

		private void Start() {
			ChangeGameState(GameState.SET_UP);
		}

		private void AddGameState(GameState gameState, DelegateStateMachine.State stateLogic) {
			stateMachine_.AddState(
					stateLogic,
					() => {
						BeforeGameStateChanged?.Invoke(gameState);
						GameState = gameState;
					},
					() => AfterGameStateChanged?.Invoke(gameState)
			);
		}

		private void SetUpState() {
			ChangeGameState(GameState.GENERATE_GRID);
		}

		private void GenerateGridState() {
			ChangeGameState(GameState.SPAWN_HEROES);
		}

		private void SpawnHeroesState() {
			UnitManager.Instance.SpawnEntity(GridManager.Instance.GetHex(new Vector3Int(1, 1)), CharacterTypes.FISH);
			ChangeGameState(GameState.PLAYER_TURN);
		}

		private void PlayerTurnState() {
		}

		private void EnemyTurnState() {

		}

		public void ChangeGameState(GameState newState) {
			if (GameState == newState) {
				Debug.LogWarning($"Ignoring redundant state change: {newState}");
				return;
			}

			stateMachine_.ChangeState(GetStateMethod(newState));
			stateMachine_.Update();
		}

		private DelegateStateMachine.State GetStateMethod(GameState state) {
			return state switch {
				GameState.SET_UP => SetUpState,
				GameState.GENERATE_GRID => GenerateGridState,
				GameState.SPAWN_HEROES => SpawnHeroesState,
				GameState.PLAYER_TURN => PlayerTurnState,
				GameState.ENEMY_TURN => EnemyTurnState,
				_ => null,
			};
		}
	}
}

[Serializable]
public enum GameState {
	NONE = 0,
	SET_UP = 1,
	GENERATE_GRID = 2,
	SPAWN_HEROES = 3,
	SPAWN_ENEMIES = 4,
	PLAYER_TURN = 5,
	ENEMY_TURN = 6,
}