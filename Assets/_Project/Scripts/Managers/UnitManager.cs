using System.Collections.Generic;
using Game.Core;
using Game.Entities;
using Game.Entities.Characters;
using Game.Entities.Enemies;
using Game.Hexagons;
using Game.Systems.AbilitySystem;
using Game.Systems.SpawnSystem;
namespace Game.Managers {
	public class UnitManager : Singleton<UnitManager> {

		public List<Enemy> Enemies { get; private set; } = new();
		public List<Character> Characters { get; private set; } = new();
		public AbilityModel AbilityModel { get; private set; } = new();
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
				AbilityModel.Add(enemy);
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
				AbilityModel.Add(character);
				EventBus<CharacterSpawnedEvent>.Raise(new CharacterSpawnedEvent {
					characterInstance = character,
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
}
