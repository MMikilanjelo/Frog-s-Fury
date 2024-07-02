using System;
using System.Collections.Generic;
using Game.Hexagons;

namespace Game.Abilities {
	public abstract class AbilitySelectionStrategy {
		public AbilityExecutionStrategy AbilityExecutionStrategy { get; protected set; }
		public event Action<Hex> TargetSelected = delegate { };
		protected AbilityData AbilityData;
		public AbilitySelectionStrategy() { }
		public void SetAbilityData(AbilityData abilityData) => AbilityData = abilityData;
		protected void OnTargetSelected(Hex selectedHex) => TargetSelected?.Invoke(selectedHex);
		public abstract void StartSelection(HashSet<Hex> targets);
		public abstract void EndSelection();
	}
}
