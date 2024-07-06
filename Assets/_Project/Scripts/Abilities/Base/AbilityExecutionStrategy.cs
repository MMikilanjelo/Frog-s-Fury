using System;

namespace Game.Abilities {
	public abstract class AbilityExecutionStrategy {
		public abstract void CastAbility(TargetData targetData);
		public event Action AbilityExecuted;
		protected void OnAbilityExecuted() => AbilityExecuted?.Invoke();
	}
}


