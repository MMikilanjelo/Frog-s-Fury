using System;

using UnityEngine;

using Game.Core;
using Game.Core.Logic;
using Game.Entities;
using Game.Hexagons;
using Game.Utils.Helpers;
using System.Linq;

namespace Game.Managers {
	public class GameManager : Singleton<GameManager> {
		public GameState GameState { get; private set; }
		public event Action<GameState> BeforeGameStateChanged = delegate { };
		public event Action<GameState> AfterGameStateChanged = delegate { };

		private DelegateStateMachine stateMachine_;

		public void Start() {
			stateMachine_ = new DelegateStateMachine();

			AddGameState(GameState.SET_UP, SetUpState);
			AddGameState(GameState.GENERATE_GRID, GenerateGridState);
			AddGameState(GameState.SPAWN_HEROES, SpawnHeroesState);
			AddGameState(GameState.SPAWN_ENEMIES, SpawnEnemiesState);
			AddGameState(GameState.START_GAME_LOOP, StartGameLoopState);
			ChangeGameState(GameState.SET_UP);
		}
		public void OnEnable() {
			TurnManager.Instance.StartPlayerTurn += () => {
				if (UnitManager.Instance.Characters.Any()) {
					SelectionManager.Instance.SetSelectedEntity(UnitManager.Instance.Characters.First());
				}
			};
		}
		public void OnDisable() {
			TurnManager.Instance.StartPlayerTurn += () => {
				if (UnitManager.Instance.Characters.Any()) {
					SelectionManager.Instance.SetSelectedEntity(UnitManager.Instance.Characters.First());
				}
			};
		}
		public void Update() {
			if (Input.GetKeyDown(KeyCode.Escape)) {
				if (TurnHelpers.AllRunOutOfActions(UnitManager.Instance.Characters)) {
					TurnManager.Instance.ChangeTurnPhase(TurnPhases.ENEMY_TURN);
				}
			}
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
			var allHexes = FindObjectsOfType<Hex>();
			foreach (var hex in allHexes) {
				GridManager.Instance.AddHex(hex);
			}
			foreach (var hex in GridManager.Instance.HexesInGrid.Values) {
				hex.CacheNeighbors();
			}
			SelectionManager.Instance.EnableSelection();
			ChangeGameState(GameState.GENERATE_GRID);
		}

		private void GenerateGridState() {
			ChangeGameState(GameState.SPAWN_HEROES);
		}

		private void SpawnHeroesState() {
			UnitManager.Instance.SpawnCharacter(GridManager.Instance.GetHex(new Vector3Int(1, 1)), EntityTypes.FISH);

			UnitManager.Instance.SpawnCharacter(GridManager.Instance.GetHex(new Vector3Int(2, 2)), EntityTypes.FISH);
			ChangeGameState(GameState.SPAWN_ENEMIES);
		}
		private void SpawnEnemiesState() {
			UnitManager.Instance.SpawnEnemy(GridManager.Instance.GetHex(new Vector3Int(-1, 1)), EntityTypes.RAT);

			UnitManager.Instance.SpawnEnemy(GridManager.Instance.GetHex(new Vector3Int(-1, 2)), EntityTypes.RAT);
			ChangeGameState(GameState.START_GAME_LOOP);
		}
		private void StartGameLoopState() {
			TurnManager.Instance.StartGameLoop();
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
				GameState.SPAWN_ENEMIES => SpawnEnemiesState,
				GameState.START_GAME_LOOP => StartGameLoopState,
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
	START_GAME_LOOP = 5,
}