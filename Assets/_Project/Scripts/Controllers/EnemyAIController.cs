
using System.Collections;
using System.Collections.Generic;
using Game.Entities.Enemies;
using Game.Managers;
using UnityEngine;

namespace Game.Controllers {
	public class EnemyAIController : MonoBehaviour {
		private List<Enemy> enemies_ => UnitManager.Instance.Enemies;
		private Coroutine executeAbilitiesCoroutine_;

		private void Awake() {
			TurnManager.Instance.EnemyTurn += () => {
				if (executeAbilitiesCoroutine_ != null) {
					StopCoroutine(executeAbilitiesCoroutine_);
				}
				executeAbilitiesCoroutine_ = StartCoroutine(ExecuteAbilities());
			};
		}

		private IEnumerator ExecuteAbilities() {
			foreach (var enemy in enemies_) {
				var enemyAbilities = AbilityResourcesManager.Instance.Get(enemy);
				foreach (var ability in enemyAbilities) {
					if (ability.CanCastAbility()) {
						var abilityStrategy = ability.AbilityStrategy;
						abilityStrategy.CastAbility();
						yield return new WaitForSeconds(0.5f);
						yield return new WaitUntil(() => abilityStrategy.Executed);
					}
				}
			}
			yield return new WaitForSeconds(1.0f);
			TurnManager.Instance.ChangeTurnPhase(TurnPhases.PLAYER_TURN);
			executeAbilitiesCoroutine_ = null;
		}
	}
}