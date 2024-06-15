using Game.Abilities;
using System.Collections.Generic;
namespace Game.Entities.Characters {
	public abstract class Character : Entity, IActionPerformer {
		public CharacterData CharacterData { get; protected set; }
		public List<AbilityData> AbilityData { get; protected set; }
		public IReadOnlyDictionary<AbilityTypes , AbilitySelectionStrategy> CharacterAbilities {get;protected set;}
		public abstract bool CanPerformAction(int actionCost);
		public abstract int GetRemainingActions();
		public void SetCharacterData(CharacterData characterData) => CharacterData = characterData;
	}
}
