using System.Collections.Generic;
using UnityEngine;
using Game.Core;
using Game.Entities.Enemies;
using System.Linq;
using Game.Entities.Characters;
using Game.Systems.AbilitySystem;
namespace Game.Systems {
	public class ResourceSystem : Singleton<ResourceSystem> {

		public IReadOnlyList<EnemyData> EnemyData;
		public IReadOnlyList<CharacterData> CharacterData;
		public IReadOnlyList<AbilityData> AbilityData;
		private const string ENEMY_RESOURCE_FOLDER = "Enemies";
		private const string CHARACTER_RESOURCE_FOLDER = "Characters";
		private const string ABILITIES_RESOURCE_FOLDER = "Abilities";
		private Dictionary<EnemyTypes, EnemyData> enemyDataCollection_ = new();
		private Dictionary<CharacterTypes, CharacterData> characterDataCollection_ = new();
		private Dictionary<AbilityTypes, AbilityData> abilitiesDataCollection_ = new();
		protected override void Awake() {
			base.Awake();
			AssembleResources();
		}
		private void AssembleResources() {
			EnemyData = Resources.LoadAll<EnemyData>(ENEMY_RESOURCE_FOLDER).ToList();
			CharacterData = Resources.LoadAll<CharacterData>(CHARACTER_RESOURCE_FOLDER).ToList();
			AbilityData = Resources.LoadAll<AbilityData>(ABILITIES_RESOURCE_FOLDER).ToList();

			abilitiesDataCollection_ = AbilityData.ToDictionary(ability => ability.Type, ability => ability);
			enemyDataCollection_ = EnemyData.ToDictionary(enemy => enemy.Type, enemy => enemy);

			characterDataCollection_ = CharacterData.ToDictionary(character => character.Type, character => character);
		}

		private bool TryGetData<K, T>(K key, Dictionary<K, T> collection, out T data) where T : class => collection.TryGetValue(key, out data);
		public bool TryGetEnemyData(EnemyTypes type, out EnemyData data) => TryGetData(type, enemyDataCollection_, out data);
		public bool TryGetCharacterData(CharacterTypes type, out CharacterData data) => TryGetData(type, characterDataCollection_, out data);
		public bool TryGetAbilityData(AbilityTypes type, out AbilityData data) => TryGetData(type, abilitiesDataCollection_, out data);
	}
}
