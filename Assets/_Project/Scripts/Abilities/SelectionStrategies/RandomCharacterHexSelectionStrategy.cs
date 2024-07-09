using System.Collections.Generic;
using System.Linq;

namespace Game.Abilities {
	public class RandomCharacterHexSelectionStrategy : AbilityTargetSelectionStrategy {
		public override void EndSelection() {
		}
		public override void SelectTarget(List<TargetData> targets) {
			OnTargetSelected(targets.First());
		}
	}
}