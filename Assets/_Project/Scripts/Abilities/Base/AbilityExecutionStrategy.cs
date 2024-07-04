using System;
using System.Collections.Generic;
using Game.Hexagons;

namespace Game.Abilities {
	public abstract class AbilityExecutionStrategy {
		public virtual void CastAbility(HashSet<Hex> targets) { }
		public virtual void CastAbility(Hex target) { }
		public event Action AbilityExecuted;
		protected void OnAbilityExecuted() => AbilityExecuted?.Invoke();

	}
}
