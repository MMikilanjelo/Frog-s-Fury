using System.Collections.Generic;
using Game.Core;
using Game.Entities.Characters;
using Game.Entities.Enemies;
using Game.Hexagons;
using Game.Systems.SpawnSystem;
namespace Game.Managers {
	public class UnitManager : Singleton<UnitManager> {

		public Dictionary<EnemyTypes, Enemy> Enemies { get; private set; } = new();
		public Dictionary<CharacterTypes, Character> Characters { get; private set; } = new();

		private IEntityFactory<Enemy, EnemyTypes> enemyFactory_;
		private IEntityFactory<Character, CharacterTypes> characterFactory_;

		protected override void Awake() {
			enemyFactory_ = new EnemyFactory();
			characterFactory_ = new CharacterFactory();
			base.Awake();
		}
		public void SpawnEntity(HexNode hexNode, EnemyTypes enemyType) {
			var enemy = enemyFactory_.Spawn(hexNode, enemyType);
			if (enemy != null) {
				Enemies.Add(enemyType, enemy);
				EventBus<EnemySpawnedEvent>.Raise(new EnemySpawnedEvent {
					enemyInstance = enemy,
				});
			}
		}
		public void SpawnEntity(HexNode hexNode, CharacterTypes characterType) {
			var character = characterFactory_.Spawn(hexNode, characterType);
			if (character != null) {
				Characters.Add(characterType, character);
				EventBus<CharacterSpawnedEvent>.Raise(new CharacterSpawnedEvent {
					characterInstance = character,
				});
			}
		}
	}
	public struct CharacterSpawnedEvent : IEvent {
		public Character characterInstance;
	}
	public struct EnemySpawnedEvent : IEvent {
		public Enemy enemyInstance;
	}
}
