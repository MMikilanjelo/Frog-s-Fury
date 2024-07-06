using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using Game.Core;
using Game.Entities.Enemies;
using Game.Entities.Characters;
using Game.Abilities;
using Game.Managers;
using Game.Highlight;
using Game.Entities;
namespace Game.Systems {
	public class ResourceSystem : Singleton<ResourceSystem> {
		public IReadOnlyList<EnemyData> EnemyData;
		public IReadOnlyList<EntityData> EntityData;
		public IReadOnlyList<AbilityData> AbilityData;
		public IReadOnlyList<HighlightData> HighlightData;
		private const string ENTITY_RESOURCES_FOLDER = "ENTITIES";
		private const string ABILITIES_RESOURCE_FOLDER = "Abilities";
		private const string HEX_HIGHLIGHT_RESOURCE_FOLDER = "Highlight";
		private Dictionary<EntityTypes, EntityData> entityDataCollection_ = new();
		private Dictionary<AbilityTypes, AbilityData> abilitiesDataCollection_ = new();
		private Dictionary<HighlightType, HighlightData> highlightDataCollection_ = new();
		protected override void Awake() {
			base.Awake();
			AssembleResources();
		}
		private void AssembleResources() {
			EntityData = Resources.LoadAll<EntityData>(ENTITY_RESOURCES_FOLDER).ToList();
			AbilityData = Resources.LoadAll<AbilityData>(ABILITIES_RESOURCE_FOLDER).ToList();
			HighlightData = Resources.LoadAll<HighlightData>(HEX_HIGHLIGHT_RESOURCE_FOLDER).ToList();

			abilitiesDataCollection_ = AbilityData.ToDictionary(ability => ability.Type, ability => ability);
			entityDataCollection_ = EntityData.ToDictionary(entity => entity.Type, entity => entity);
			highlightDataCollection_ = HighlightData.ToDictionary(highlight => highlight.Type, highlight => highlight);
		}
		private bool TryGetData<K, T>(K key, Dictionary<K, T> collection, out T data) where T : class => collection.TryGetValue(key, out data);
		public bool TryGetEntityData(EntityTypes type, out EntityData data) => TryGetData(type, entityDataCollection_, out data);
		public bool TryGetAbilityData(AbilityTypes type, out AbilityData data) => TryGetData(type, abilitiesDataCollection_, out data);
		public bool TryGetHighlightData(HighlightType type, out HighlightData data) => TryGetData(type, highlightDataCollection_, out data);
	}
}
