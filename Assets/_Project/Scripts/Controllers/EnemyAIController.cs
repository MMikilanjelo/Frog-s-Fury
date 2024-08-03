using System.Collections;
using System.Collections.Generic;
using Game.Entities.Enemies;
using Game.Managers;
using UnityEngine;


namespace Game.Controllers {
	public class EnemyAIController : MonoBehaviour {
		private List<Enemy> enemies_ => UnitManager.Instance.Enemies;
		private Coroutine executeAbilitiesCoroutine_;

		private void OnEnable() {
			TurnManager.Instance.EnemyTurn += StartEnemyTurn;
		}
		private void OnDisable() {
			TurnManager.Instance.EnemyTurn -= StartEnemyTurn;
		}

		private void StartEnemyTurn() {
			if (executeAbilitiesCoroutine_ != null) {
				StopCoroutine(executeAbilitiesCoroutine_);
			}
			executeAbilitiesCoroutine_ = StartCoroutine(ExecuteAbilities());
		}
		private IEnumerator ExecuteAbilities() {
			foreach (var enemy in enemies_) {
				foreach (var ability in enemy.Abilities.Values) {
					if (ability.CanCastAbility()) {
						ability.CastAbility();
						yield return new WaitUntil(() => ability.Executed);
					}
				}
			}
			yield return new WaitForSeconds(1.0f);
			TurnManager.Instance.ChangeTurnPhase(TurnPhases.PLAYER_TURN);
			executeAbilitiesCoroutine_ = null;
		}
	}
}