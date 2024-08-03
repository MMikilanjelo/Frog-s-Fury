using System;
using System.Collections.Generic;

namespace Game.Abilities {
	public abstract class AbilityTargetSelectionStrategy<T> where T : TargetData {
		protected AbilityExecutionStrategy<T> AbilityExecutionStrategy;
		public abstract void SelectTarget(List<T> targets);
		public event Action TargetSelected = delegate { };
		protected void OnTargetSelected() => TargetSelected?.Invoke();
		public virtual void EndSelection() { }
	}
}
