using Game.Selection;

namespace Game.Abilities {
	public abstract class AbilityStrategy {
		protected AbilityData AbilityData;
		public void SetAbilityData(AbilityData abilityData) => AbilityData = abilityData;
		public abstract void CastAbility(SelectionData selectionData);
	}
}
