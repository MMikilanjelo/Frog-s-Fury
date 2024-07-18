
using System.Collections.Generic;
using Game.Core;
using Game.Entities;
using Game.Entities.Characters;
using Game.Entities.Enemies;
using Game.Hexagons;
using Game.Systems.SpawnSystem;

namespace Game.Managers {
	public class UnitManager : Singleton<UnitManager> {
		public List<Enemy> Enemies { get; private set; } = new();
		public List<Character> Characters { get; private set; } = new();
		private IEntityFactory<Enemy, EntityTypes> enemyFactory_;
		private IEntityFactory<Character, EntityTypes> characterFactory_;

		protected override void Awake() {
			enemyFactory_ = new EnemyFactory();
			characterFactory_ = new CharacterFactory();
			base.Awake();
		}

		public Enemy SpawnEnemy(Hex hexNode, EntityTypes enemyType) {
			var enemy = enemyFactory_.Spawn(hexNode, enemyType);
			if (enemy != null) {
				Enemies.Add(enemy);
				AbilityResourcesManager.Instance.Add(enemy);
				EventBus<EnemySpawnedEvent>.Raise(new EnemySpawnedEvent {
					enemyInstance = enemy,
				});
				return enemy;
			}
			return null;
		}

		public Character SpawnCharacter(Hex hexNode, EntityTypes characterType) {
			var character = characterFactory_.Spawn(hexNode, characterType);
			if (character != null) {
				Characters.Add(character);
				AbilityResourcesManager.Instance.Add(character);
				EventBus<CharacterSpawnedEvent>.Raise(new CharacterSpawnedEvent {
					characterInstance = character,
				});
				return character;
			}
			return null;
		}
		public void DestroyEnemy(Enemy enemy) {
			if (Enemies.Remove(enemy)) {
				EventBus<EnemyDestroyedEvent>.Raise(new EnemyDestroyedEvent {
					enemyInstance = enemy,
				});
				enemy.OccupiedHex.SetOccupiedEntity(null);
				enemy.SetOccupiedHex(null);
				Destroy(enemy.gameObject);
			}
		}

		public void DestroyCharacter(Character character) {
			if (Characters.Remove(character)) {
				EventBus<CharacterDestroyedEvent>.Raise(new CharacterDestroyedEvent {
					characterInstance = character,
				});
				character.OccupiedHex.SetOccupiedEntity(null);
				character.SetOccupiedHex(null);
				Destroy(character.gameObject);
			}
		}
	}

	public struct CharacterSpawnedEvent : IEvent {
		public Character characterInstance;
	}

	public struct EnemySpawnedEvent : IEvent {
		public Enemy enemyInstance;
	}
	public struct CharacterDestroyedEvent : IEvent {
		public Character characterInstance;
	}

	public struct EnemyDestroyedEvent : IEvent {
		public Enemy enemyInstance;
	}

}