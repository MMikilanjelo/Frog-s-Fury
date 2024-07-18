using System;

namespace Game.Abilities {
	public abstract class AbilityExecutionStrategy<T> where T : ITargetData {
		public abstract void CastAbility(T targetData);
		public event Action AbilityExecuted = delegate { };
		protected void OnAbilityExecuted() => AbilityExecuted?.Invoke();
	}
}




