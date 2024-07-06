using System;
using System.Collections.Generic;
using Game.Hexagons;

namespace Game.Abilities {
	public abstract class AbilityTargetSelectionStrategy {
		public event Action<TargetData> TargetSelected = delegate { };
		public AbilityTargetSelectionStrategy() { }
		protected void OnTargetSelected(TargetData selectedHex) => TargetSelected?.Invoke(selectedHex);
		public abstract void SelectTarget(List<TargetData> targets);
		public virtual void EndSelection() { }
	}
}
