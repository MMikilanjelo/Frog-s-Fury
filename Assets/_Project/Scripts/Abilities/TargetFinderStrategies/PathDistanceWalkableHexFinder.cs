
using System.Collections.Generic;
using Game.Entities;
using Game.Hexagons;
using Game.Utils.Helpers;

namespace Game.Abilities {
	public class PathDistanceWalkableHexFinder : AbilityTargetsFinderStrategy<TargetData> {
		private int searchRange_ = 1;

		private PathDistanceWalkableHexFinder() { }

		public override bool TryFindTargets(Entity seeker, out List<TargetData> data) {
			var targets = new List<TargetData>();
			var walkableHexes = HexagonalGridHelper.FindWalkableHexesWithinPathDistance(seeker.OccupiedHex, searchRange_);
			foreach (var targetHex in walkableHexes) {
				targets.Add(new TargetData(targetHex));
			}
			data = targets;
			return targets.Count > 0;
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
