using System;
using System.Collections.Generic;
using Game.Hexagons;

namespace Game.Abilities {
	public abstract class AbilityTargetSelectionStrategy {
		public AbilityExecutionStrategy AbilityExecutionStrategy { get; protected set; }
		public event Action<HashSet<Hex>> TargetsSelected = delegate { };
		public AbilityTargetSelectionStrategy() { }
		protected void OnTargetSelected(HashSet<Hex> selectedHexes) => TargetsSelected?.Invoke(selectedHexes);
		public abstract void SelectTarget(HashSet<Hex> targets);
		public abstract void EndSelection();
	}
}
