using System;
using System.Collections.Generic;

namespace Game.Abilities {
	public abstract class AbilityTargetSelectionStrategy<T> where T : TargetData {
		public event Action<T> TargetSelected = delegate { };
		protected void OnTargetSelected(T selectedTarget) => TargetSelected?.Invoke(selectedTarget);
		public abstract void SelectTarget(List<T> targets);
		public virtual void EndSelection() { }
	}
}
