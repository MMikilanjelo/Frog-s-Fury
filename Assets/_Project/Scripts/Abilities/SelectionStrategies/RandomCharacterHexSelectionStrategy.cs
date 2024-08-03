
using System.Collections.Generic;

namespace Game.Abilities {
	public class RandomCharacterHexSelectionStrategy<T> : AbilityTargetSelectionStrategy<T> where T : TargetData {

		private AbilityExecutionStrategy<T> abilityExecutionStrategy_;
		public RandomCharacterHexSelectionStrategy(AbilityExecutionStrategy<T> abilityExecutionStrategy) {
			abilityExecutionStrategy_ = abilityExecutionStrategy;
		}
		public override void EndSelection() {
		}

		public override void SelectTarget(List<T> targets) {
			if (targets == null || targets.Count == 0) {
				return;
			}

			int randomIndex = UnityEngine.Random.Range(0, targets.Count);
			abilityExecutionStrategy_.CastAbility(targets[randomIndex]);
			OnTargetSelected();
		}
	}
}
