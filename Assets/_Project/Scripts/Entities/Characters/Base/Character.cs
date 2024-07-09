using Game.Abilities;
using System.Collections.Generic;
namespace Game.Entities.Characters {
	public abstract class Character : Entity {
		public CharacterData Data { get; protected set; }
		public void SetCharacterData(CharacterData data) => Data = data;
	}
}
