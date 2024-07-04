using Game.Entities;
using Game.Utils.Helpers;
namespace Game.Abilities {
	public class PathDistanceWalkableHexFinder : AbilityMultipleTargetsFinderStrategy {
		private int searchRange_ = 1;
		private PathDistanceWalkableHexFinder() { }

		public override void FindTargets(Entity seeker) {
			if (TryFindTargets(seeker)) {
				OnTargetsFind(Targets);
			}
		}
		public override bool TryFindTargets(Entity seeker) {
			Targets = HexagonalGridHelper.FindWalkableHexesWithinPathDistance(seeker.OccupiedHex, searchRange_);
			return Targets.Count > 0;
		}

		public class Builder {
			private readonly PathDistanceWalkableHexFinder pathDistanceHexFinder_ = new PathDistanceWalkableHexFinder();
			public Builder WithSearchRange(int searchRange) {
				pathDistanceHexFinder_.searchRange_ = searchRange;
				return this;
			}
			public PathDistanceWalkableHexFinder Build() {
				return pathDistanceHexFinder_;
			}
		}
	}
}

