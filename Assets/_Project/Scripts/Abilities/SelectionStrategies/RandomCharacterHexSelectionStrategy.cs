
using System.Collections.Generic;

namespace Game.Abilities {
	public class RandomCharacterHexSelectionStrategy<T> : AbilityTargetSelectionStrategy<T> where T : class ,ITargetData {
		public override void EndSelection() {
			// No action needed for this method in this strategy
		}

		public override void SelectTarget(List<T> targets) {
			if (targets == null || targets.Count == 0) {
				return;
			}

			int randomIndex = UnityEngine.Random.Range(0, targets.Count);
			OnTargetSelected(targets[randomIndex]);
		}
	}
}
