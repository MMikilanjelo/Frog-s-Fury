
using UnityEngine;
using Game.Entities.Characters;
using Game.Hexagons;
namespace Game.Systems.SpawnSystem {
	public class CharacterFactory : IEntityFactory<Character , CharacterTypes> {
		public Character Spawn(HexNode hexNode, CharacterTypes entityTypeEnum) {
			if (ResourceSystem.Instance.TryGetCharacterData(entityTypeEnum, out CharacterData data)) {
				var characterInstance = GameObject.Instantiate(data.Prefab, hexNode.Position , Quaternion.identity) as Character;
				
				characterInstance.SetOccupiedHexNode(hexNode);
				hexNode.SetOccupiedEntity(characterInstance);
				characterInstance.SetCharacterData(data);
				
				return characterInstance;
			}
			return null;
		}
	}
}
