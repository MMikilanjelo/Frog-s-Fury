using System;
using System.Collections.Generic;
using Game.Hexagons;

namespace Game.Abilities {
	public abstract class AbilityTargetSelectionStrategy {
		public AbilityExecutionStrategy AbilityExecutionStrategy { get; protected set; }
		public event Action<Hex> TargetSelected = delegate { };
		public AbilityTargetSelectionStrategy() { }
		protected void OnTargetSelected(Hex selectedHex) => TargetSelected?.Invoke(selectedHex);
		public abstract void SelectTarget(HashSet<Hex> targets);
		public abstract void EndSelection();
	}
	
}
