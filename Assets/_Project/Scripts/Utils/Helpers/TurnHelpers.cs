using System.Collections.Generic;
using Game.Abilities;
using Game.Entities;
using Game.Managers;
using UnityEngine;

namespace Game.Utils.Helpers {
	public static class TurnHelpers {
		public static bool IsPlayerTurn() => TurnManager.Instance.TurnPhase == TurnPhases.PLAYER_TURN;
		public static bool IsEnemyTurn() => TurnManager.Instance.TurnPhase == TurnPhases.ENEMY_TURN;
		public static bool AllRunOutOfActions<T>(List<T> entities) where T : AbilityPerformer {
			for (int i = 0; i < entities.Count; i++) {
				if (entities[i].TryGetComponent(out AbilityManagerComponent abilityManagerComponent)) {
					return !abilityManagerComponent.HasAbilitiesRemain() || entities[i].GetRemainingActions() == 0;
				}
			}
			return true;
		}
	}
}