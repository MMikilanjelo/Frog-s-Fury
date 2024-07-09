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
				EventBus<EntitySpawnedEvent>.Raise(new EntitySpawnedEvent {
					entityInstance = enemy,
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
				EventBus<EntitySpawnedEvent>.Raise(new EntitySpawnedEvent {
					entityInstance = character,
				});
				return character;
			}
			return null;
		}
	}
	public struct CharacterSpawnedEvent : IEvent {
		public Character characterInstance;
	}
	public struct EnemySpawnedEvent : IEvent {
		public Enemy enemyInstance;
	}
	public struct EntitySpawnedEvent : IEvent {
		public Entity entityInstance;
	}
}
