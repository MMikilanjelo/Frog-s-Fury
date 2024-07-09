
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

		public event Action<TurnPhases> BeforeTurnPhaseChanged = delegate { };
		public event Action<TurnPhases> AfterTurnPhaseChanged = delegate { };

		public TurnPhases TurnPhase { get; private set; } = TurnPhases.NONE;
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
			BeforeTurnPhaseChanged?.Invoke(newPhase);
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
			AfterTurnPhaseChanged?.Invoke(TurnPhase);
		}
	}

	public enum TurnPhases {
		NONE = 0,
		PLAYER_TURN = 1,
		ENEMY_TURN = 2,
	}
}