
using System;
using System.Collections.Generic;
using Game.Entities;
using Game.Utils.Helpers;

namespace Game.Abilities {
	public class NearestWalkableHexAroundCharacterFinder<T> : AbilityTargetsFinderStrategy<T> where T :  class ,ITargetData {
		private int searchRange_ = 1;
		private Fraction fraction_ = Fraction.NONE;

		private NearestWalkableHexAroundCharacterFinder() { }

		public override bool TryFindTargets(Entity seeker, out List<T> data) {
			var targets = new List<T>();
			var closestHexesAroundEntities = HexagonalGridHelper.FindClosestWalkableHexesNearEntityWithinDistance(seeker.OccupiedHex, searchRange_, fraction_);
			foreach (var (entity, hex) in closestHexesAroundEntities) {
				T targetData = Activator.CreateInstance(typeof(T), hex, entity) as T;
				targets.Add(targetData);
			}
			data = targets;
			return targets.Count > 0;
		}

		public class Builder {
			private readonly NearestWalkableHexAroundCharacterFinder<T> entityInRangeFinder_ = new NearestWalkableHexAroundCharacterFinder<T>();

			public Builder WithSearchRange(int searchRange) {
				entityInRangeFinder_.searchRange_ = searchRange;
				return this;
			}

			public Builder WithFraction(Fraction fraction) {
				entityInRangeFinder_.fraction_ = fraction;
				return this;
			}
			public NearestWalkableHexAroundCharacterFinder<T> Build() => entityInRangeFinder_;
		}
	}
}