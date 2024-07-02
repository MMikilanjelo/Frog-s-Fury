using System.Collections.Generic;
using System.Linq;
using Game.Abilities;
using Game.Core;
using Game.Entities.Enemies;
using Game.Managers;
using Game.Systems.AbilitySystem;
using UnityEngine;
namespace Game.Controllers.EnemyAISystem {
	public class EnemyAIController : MonoBehaviour {
		private AbilityModel<Enemy> model_;
		private void Awake() {
			model_ = new();
			model_.EntityAdded += (Enemy enemy, IList<Ability> abilities) => OnEnemyAdded(enemy, abilities);
			EventBinding<EnemySpawnedEvent> enemySpawnedEvent = new EventBinding<EnemySpawnedEvent>
				((EnemySpawnedEvent enemySpawnedEvent) => {
					OnEnemySpawned(enemySpawnedEvent);
				});
			EventBus<EnemySpawnedEvent>.Register(enemySpawnedEvent);
			TurnManager.Instance.StartEnemyTurn += () => {
				var ability = FindBestAbility();
				if (ability == null) {
					TurnManager.Instance.ChangeTurnPhase(TurnPhases.PLAYER_TURN);
					Debug.Log("No abilities that i can cast find");
					return;
				}
				ability.CastAbility();
			};
		}
		private void OnEnemyAdded(Enemy enemy, IList<Ability> abilities) {

		}
		private void OnEnemySpawned(EnemySpawnedEvent enemySpawnedEvent) {
			model_.Add(enemySpawnedEvent.enemyInstance);
		}
		private Ability FindBestAbility() {
			var enemy = model_.EntityAbilities.First();
			var abilities = enemy.Value;
			if (abilities[0].CanCastAbility()) {
				return abilities[0];
			}
			return null;
		}
	}
}
