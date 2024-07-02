using System.Collections.Generic;
using Game.Entities;
using Game.Managers;

namespace Game.Utils.Helpers {
	public static class TurnHelpers {
		public static bool IsPlayerTurn() => TurnManager.Instance.TurnPhase == TurnPhases.PLAYER_TURN;
		public static bool IsEnemyTurn() => TurnManager.Instance.TurnPhase == TurnPhases.ENEMY_TURN;
		public static bool AllRunOutOfActions<T>(List<T> entities) where T : IActionPerformer {
			for (int i = 0; i < entities.Count; i++) {
				if (entities[i].GetRemainingActions() > 0) {
					return false;
				}
			}
			return true;
		}
	}
}