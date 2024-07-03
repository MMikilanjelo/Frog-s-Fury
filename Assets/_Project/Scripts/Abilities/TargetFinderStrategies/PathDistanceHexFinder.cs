using Game.Entities;
using Game.Utils.Helpers;
namespace Game.Abilities {
	public class PathDistanceHexFinder : AbilityTargetFinderStrategy {
		private HexNodeFlags flags_ = HexNodeFlags.None;
		private int searchRange_ = 1;
		private PathDistanceHexFinder() { }

		public override void FindTargets(Entity seeker) {
			if (TryFindTargets(seeker)) {
				OnTargetsFind(Targets);
			}
		}

		public override bool TryFindTargets(Entity seeker) {
			Targets = HexagonalGridHelper.FindHexesWithinPathDistance(seeker.OccupiedHex, searchRange_, flags_);
			return Targets.Count > 0;
		}

		public class Builder {
			private readonly PathDistanceHexFinder pathDistanceHexFinder_ = new PathDistanceHexFinder();

			public Builder WithFlags(HexNodeFlags flags) {
				pathDistanceHexFinder_.flags_ = flags;
				return this;
			}
			public Builder WithSearchRange(int searchRange) {
				pathDistanceHexFinder_.searchRange_ = searchRange;
				return this;
			}
			public PathDistanceHexFinder Build() {
				return pathDistanceHexFinder_;
			}
		}
	}
}

