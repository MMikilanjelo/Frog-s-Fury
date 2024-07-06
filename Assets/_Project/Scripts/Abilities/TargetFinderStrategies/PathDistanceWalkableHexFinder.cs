using Game.Entities;
using Game.Utils.Helpers;
namespace Game.Abilities {
	public class PathDistanceWalkableHexFinder : AbilityTargetsFinderStrategy {
		private int searchRange_ = 1;
		private PathDistanceWalkableHexFinder() { }
		public override bool TryFindTargets(Entity seeker) {
			TargetsData.Clear();
			var targets = HexagonalGridHelper.FindWalkableHexesWithinPathDistance(seeker.OccupiedHex, searchRange_);
			foreach (var targetHex in targets) {
				TargetsData.Add(new TargetData {
					Hex = targetHex,
				});
			}
			return TargetsData.Count > 0;
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

