using System.Collections.Generic;
using System.Linq;
using Game.Hexagons;

namespace Game.Abilities {
	public class RandomCharacterHexSelectionStrategy : AbilityTargetSelectionStrategy {
		public override void EndSelection() {
		}
		public override void SelectTarget(HashSet<Hex> targets) {
			OnTargetSelected(targets.First());
		}
	}
}