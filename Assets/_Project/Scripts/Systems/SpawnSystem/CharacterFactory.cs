using UnityEngine;
using Game.Entities.Characters;
using Game.Hexagons;
using Game.Entities;

namespace Game.Systems.SpawnSystem {
	public class CharacterFactory : IEntityFactory<Character, EntityTypes> {
		public Character Spawn(Hex hex, EntityTypes entityTypeEnum) {
			if (ResourceSystem.Instance.TryGetEntityData(entityTypeEnum, out EntityData data)) {
				var characterInstance = GameObject.Instantiate(data.Prefab, hex.WorldPosition, Quaternion.identity).GetComponent<Character>();
				characterInstance.SetCharacterData(data as CharacterData);
				characterInstance.SetOccupiedHex(hex);
				hex.SetOccupiedEntity(characterInstance);
				return characterInstance;
			}
			return null;
		}
	}
}