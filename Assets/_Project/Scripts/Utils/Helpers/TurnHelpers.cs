using System.Collections.Generic;
using Game.Entities;
using Game.Managers;

namespace Game.Utils.Helpers {
	public static class TurnHelpers {
		public static bool IsPlayerTurn(GameState gameState) {
			return gameState == GameState.PLAYER_TURN ;
		}
		public static bool IsEnemyTurn(GameState gameState) => gameState == GameState.ENEMY_TURN;
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