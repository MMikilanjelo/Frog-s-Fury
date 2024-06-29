
using System;
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

		public TurnPhases TurnPhase { get; private set; } 

		protected override void Awake() {
			base.Awake();
		}


		private void Update() {
			if (Input.GetKeyDown(KeyCode.E)) {
				ChangeTurnPhase(TurnPhases.ENEMY_TURN);
			}
			if (Input.GetKeyDown(KeyCode.P)) {
				ChangeTurnPhase(TurnPhases.PLAYER_TURN);
			}
		}

		public void StartGameLoop() {
			ChangeTurnPhase(TurnPhases.PLAYER_TURN);
		}

		public void ChangeTurnPhase(TurnPhases newPhase) {
			if (TurnPhase == newPhase) return;

			switch (TurnPhase) {
				case TurnPhases.PLAYER_TURN:
					EndOfPlayerTurn?.Invoke();
					break;
				case TurnPhases.ENEMY_TURN:
					EndOfEnemyTurn?.Invoke();
					break;
			}

			TurnPhase = newPhase;

			switch (TurnPhase) {
				case TurnPhases.PLAYER_TURN:
					StartPlayerTurn?.Invoke();
					PlayerTurn?.Invoke();
					break;
				case TurnPhases.ENEMY_TURN:
					StartEnemyTurn?.Invoke();
					EnemyTurn?.Invoke();
					break;
			}
		}
	}

	public enum TurnPhases {
		PLAYER_TURN = 0,
		ENEMY_TURN = 1,
	}
}