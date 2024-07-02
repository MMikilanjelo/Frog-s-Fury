using Game.Abilities;
using System.Collections.Generic;
namespace Game.Entities.Characters {
	public abstract class Character : Entity, IActionPerformer {
		public CharacterData Data { get; protected set; }
		public List<AbilityData> AbilityData { get; protected set; }
		public abstract bool CanPerformAction(int actionCost);
		public abstract int GetRemainingActions();
		public void SetCharacterData(CharacterData characterData) => Data = characterData;
	}
}
