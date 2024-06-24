using System;
using Game.Hexagons;

namespace Game.Abilities {
	public abstract class AbilitySelectionStrategy {
		public AbilityExecutionStrategy AbilityExecutionStrategy { get; protected set; }
		public event Action<Hex> TargetSelected = delegate { };
		public AbilitySelectionStrategy() { }
		protected void OnTargetSelected(Hex selectedHex){
			TargetSelected?.Invoke(selectedHex);
		}
		public abstract void StartSelection(AbilityData abilityData);
		public abstract void EndSelection();
	}
}
