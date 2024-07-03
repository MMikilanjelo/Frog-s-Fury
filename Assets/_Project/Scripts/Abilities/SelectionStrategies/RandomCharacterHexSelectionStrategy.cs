using System.Collections.Generic;
using System.Linq;
using Game.Hexagons;
using Game.Utils.Helpers;

namespace Game.Abilities {
	public class RandomCharacterHexSelectionStrategy : AbilityTargetSelectionStrategy {
		public override void EndSelection() {
		}
		public override void SelectTarget(HashSet<Hex> targets) {
			OnTargetSelected(
				new HashSet<Hex> { HexagonalGridHelper.GetFirstWalkableHex(targets.ElementAt(0).Neighbors) }
				);
		}
	}
}