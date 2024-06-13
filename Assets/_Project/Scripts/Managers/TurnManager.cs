using System;
using Game.Utils.Helpers;
using Game.Core;
using UnityEngine;

namespace Game.Managers {
	public class TurnManager : Singleton<TurnManager> {

		public event Action StartPlayerTurn = delegate { };
		public event Action PlayerTurn = delegate { };
		public event Action EndOfPlayerTurn = delegate { };

		public event Action StartEnemyTurn = delegate { };
		public event Action EnemyTurn = delegate { };
		public event Action EndOfEnemyTurn = delegate { };

		protected override void Awake() {
			base.Awake();
		}

		private void OnEnable() {
			GameManager.Instance.BeforeGameStateChanged += OnBeforeGameStateChanged;
			GameManager.Instance.AfterGameStateChanged += OnAfterGameStateChanged;
		}

		private void OnDisable() {
			GameManager.Instance.BeforeGameStateChanged -= OnBeforeGameStateChanged;
			GameManager.Instance.AfterGameStateChanged -= OnAfterGameStateChanged;
		}

		private void OnBeforeGameStateChanged(GameState newGameState) {
			
			if (TurnHelpers.IsPlayerTurn(GameManager.Instance.GameState) && newGameState == GameState.ENEMY_TURN) {
				EndOfPlayerTurn?.Invoke();
			}
			if (TurnHelpers.IsEnemyTurn(GameManager.Instance.GameState) && newGameState == GameState.PLAYER_TURN) {
				EndOfEnemyTurn?.Invoke();
			}
		}

		private void OnAfterGameStateChanged(GameState gameState) {

			if (TurnHelpers.IsPlayerTurn(gameState)) {
				StartPlayerTurn?.Invoke();
				PlayerTurn?.Invoke();
			}

			if (TurnHelpers.IsEnemyTurn(gameState)) {
				StartEnemyTurn?.Invoke();
				EnemyTurn?.Invoke();
			}
		}
	}
}