
using System.Collections.Generic;
using Game.Entities;
using Game.Utils.Helpers;

namespace Game.Abilities {
	public class NearestWalkableHexAroundEntityFinder<T> : AbilityTargetsFinderStrategy<TargetDataWithAdditionalTarget<T>> where T : class {
		private int searchRange_ = 1;
		private Fraction fraction_ = Fraction.NONE;
		private NearestWalkableHexAroundEntityFinder() { }
		public override bool TryFindTargets(Entity seeker, out List<TargetDataWithAdditionalTarget<T>> data) {
			var targets = new List<TargetDataWithAdditionalTarget<T>>();
			var closestHexesAroundEntities = HexagonalGridHelper.FindClosestWalkableHexesNearEntityWithinDistance(seeker.OccupiedHex, searchRange_, fraction_);
			foreach (var (entity, hex) in closestHexesAroundEntities) {
				if (entity is T castEntity) {
					TargetDataWithAdditionalTarget<T> targetData = new TargetDataWithAdditionalTarget<T>(hex, castEntity);
					targets.Add(targetData);
				}
			}
			data = targets;
			return targets.Count > 0;
		}
		public class Builder {
			private readonly NearestWalkableHexAroundEntityFinder<T> entityInRangeFinder_ = new NearestWalkableHexAroundEntityFinder<T>();

			public Builder WithSearchRange(int searchRange) {
				entityInRangeFinder_.searchRange_ = searchRange;
				return this;
			}

			public Builder WithFraction(Fraction fraction) {
				entityInRangeFinder_.fraction_ = fraction;
				return this;
			}
			public NearestWalkableHexAroundEntityFinder<T> Build() => entityInRangeFinder_;
		}
	}
}