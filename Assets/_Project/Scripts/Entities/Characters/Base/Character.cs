using System.Collections.Generic;

using Game.Commands;
using Game.Systems.AbilitySystem;

namespace Game.Entities.Characters {
	public abstract class Character : Entity {
		public CharacterData CharacterData{get;protected set;}
		public IReadOnlyDictionary<AbilityTypes , ICommand > CharacterAbilities {get;protected set;}

		public void SetCharacterData(CharacterData characterData) => CharacterData = characterData;
	}
}
