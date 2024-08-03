using System;

namespace Game.Abilities {
	public abstract class AbilityExecutionStrategy<T> where T : TargetData {
		public abstract void CastAbility(T targetData);
		public event Action AbilityExecuted = delegate { };
		protected void OnAbilityExecuted() => AbilityExecuted?.Invoke();
	}
	public interface IAbilityExecutionStrategy {
		void CastAbility();
		event Action AbilityExecuted;
	}
}




