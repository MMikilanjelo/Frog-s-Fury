using Game.Entities;
using Game.Hexagons;
using Game.Utils.Helpers;

namespace Game.Abilities {
	public class WalkableHexFinderStrategy : AbilityTargetFinderStrategy {
		public override void FindTargets(Entity seeker) {
			if (TryFindTargets(seeker)) {
				OnTargetsFind(Targets);
			}
		}
		public override bool ValidateTarget(Hex target) => Targets.Contains(target);
		public override bool TryFindTargets(Entity seeker) {
			Targets.Clear();
			if (AbilityData is MoveAbilityData moveAbilityData) {
				Targets = HexagonalGridHelper.FindHexesWithinPathDistance(seeker.OccupiedHex, moveAbilityData.Range, HexNodeFlags.Walkable);
				return Targets.Count > 0;
			}
			return false;

		}
	}
}

