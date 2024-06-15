using UnityEngine;
using Game.Entities.Characters;
using Game.Hexagons;

namespace Game.Systems.SpawnSystem
{
	public class CharacterFactory : IEntityFactory<Character, CharacterTypes>
	{
		public Character Spawn(Hex hex, CharacterTypes entityTypeEnum)
		{
			if (ResourceSystem.Instance.TryGetCharacterData(entityTypeEnum, out CharacterData data))
			{
				var characterInstance = GameObject.Instantiate(data.Prefab, hex.WorldPosition, Quaternion.identity).GetComponent<Character>();
				characterInstance.SetCharacterData(data);
				characterInstance.SetOccupiedHex(hex);
				hex.SetOccupiedEntity(characterInstance);
				return characterInstance;
			}
			return null;
		}
	}
}