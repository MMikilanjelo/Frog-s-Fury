using Game.Abilities;
using System.Collections.Generic;
namespace Game.Entities.Characters {
	public abstract class Character : Entity {
		public List<AbilityData> AbilityData { get; protected set; }
		public CharacterData Data { get; protected set; }
		public void SetCharacterData(CharacterData characterData) => Data = characterData;
	}
}
