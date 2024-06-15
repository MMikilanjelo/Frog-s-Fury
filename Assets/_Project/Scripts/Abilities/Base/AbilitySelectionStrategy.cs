using UnityEngine;

namespace Game.Abilities {
	public abstract class AbilitySelectionStrategy {
		public AbilityStrategy AbilityStrategy { get; protected set; }
		public AbilitySelectionStrategy(AbilityStrategy abilityStrategy) {
			AbilityStrategy = abilityStrategy;
		}
		public abstract void StartSelection();
		public abstract void EndSelection();
	}
}
