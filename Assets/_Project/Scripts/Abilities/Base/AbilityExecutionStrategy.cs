using System;
using System.Collections.Generic;

namespace Game.Abilities {
	public abstract class AbilityExecutionStrategy {
		public virtual void CastAbility(List<TargetData> targetData) { }
		public virtual void CastAbility(TargetData targetData) { }
		public event Action AbilityExecuted = delegate { };
		protected void OnAbilityExecuted() => AbilityExecuted?.Invoke();
	}
}


