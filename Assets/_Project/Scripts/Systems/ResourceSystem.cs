using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using Game.Core;
using Game.Entities.Enemies;
using Game.Entities.Characters;
using Game.Abilities;
using Game.Managers;
using Game.Highlight;
namespace Game.Systems {
	public class ResourceSystem : Singleton<ResourceSystem> {
		public IReadOnlyList<EnemyData> EnemyData;
		public IReadOnlyList<CharacterData> CharacterData;
		public IReadOnlyList<AbilityData> AbilityData;
		public IReadOnlyList<HighlightData> HighlightData;
		private const string ENEMY_RESOURCE_FOLDER = "Enemies";
		private const string CHARACTER_RESOURCE_FOLDER = "Characters";
		private const string ABILITIES_RESOURCE_FOLDER = "Abilities";
		private const string HEX_HIGHLIGHT_RESOURCE_FOLDER = "Highlight";
		private Dictionary<EnemyTypes, EnemyData> enemyDataCollection_ = new();
		private Dictionary<CharacterTypes, CharacterData> characterDataCollection_ = new();
		private Dictionary<AbilityTypes, AbilityData> abilitiesDataCollection_ = new();
		private Dictionary<HighlightType, HighlightData> highlightDataCollection_ = new();
		protected override void Awake() {
			base.Awake();
			AssembleResources();
		}
		private void AssembleResources() {
			EnemyData = Resources.LoadAll<EnemyData>(ENEMY_RESOURCE_FOLDER).ToList();
			CharacterData = Resources.LoadAll<CharacterData>(CHARACTER_RESOURCE_FOLDER).ToList();
			AbilityData = Resources.LoadAll<AbilityData>(ABILITIES_RESOURCE_FOLDER).ToList();
			HighlightData = Resources.LoadAll<HighlightData>(HEX_HIGHLIGHT_RESOURCE_FOLDER).ToList();
			
			abilitiesDataCollection_ = AbilityData.ToDictionary(ability => ability.Type, ability => ability);
			enemyDataCollection_ = EnemyData.ToDictionary(enemy => enemy.Type, enemy => enemy);
			characterDataCollection_ = CharacterData.ToDictionary(character => character.Type, character => character);
			highlightDataCollection_ = HighlightData.ToDictionary(highlight => highlight.Type, highlight => highlight);
		}
		private bool TryGetData<K, T>(K key, Dictionary<K, T> collection, out T data) where T : class => collection.TryGetValue(key, out data);
		public bool TryGetEnemyData(EnemyTypes type, out EnemyData data) => TryGetData(type, enemyDataCollection_, out data);
		public bool TryGetCharacterData(CharacterTypes type, out CharacterData data) => TryGetData(type, characterDataCollection_, out data);
		public bool TryGetAbilityData(AbilityTypes type, out AbilityData data) => TryGetData(type, abilitiesDataCollection_, out data);
		public bool TryGetHighlightData(HighlightType type, out HighlightData data) => TryGetData(type, highlightDataCollection_, out data);
	}
}
