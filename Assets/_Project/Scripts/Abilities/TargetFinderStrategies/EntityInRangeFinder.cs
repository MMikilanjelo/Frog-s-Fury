
using System.Collections.Generic;
using Game.Entities;
using Game.Utils.Helpers;

namespace Game.Abilities {
	public class EntityInRangeFinder<T> : AbilityTargetsFinderStrategy<TargetDataWithAdditionalTarget<T>> where T : class {
		private Fraction fraction_ = Fraction.ENEMY;
		private int range_ = 1;

		private EntityInRangeFinder() { }

		public override bool TryFindTargets(Entity seeker, out List<TargetDataWithAdditionalTarget<T>> data) {
			var targets = new List<TargetDataWithAdditionalTarget<T>>();
			var possibleTargets = HexagonalGridHelper.FindHexesWithinAxialDistance(seeker.OccupiedHex, range_, fraction_);
			foreach (var targetHex in possibleTargets) {
				if (targetHex.OccupiedEntity is T additionalTarget) {
					TargetDataWithAdditionalTarget<T> targetData = new TargetDataWithAdditionalTarget<T>(targetHex, additionalTarget);
					targets.Add(targetData);
				}
			}
			data = targets;
			return targets.Count > 0;
		}

		public class Builder {
			private readonly EntityInRangeFinder<T> entityInRangeFinder_ = new EntityInRangeFinder<T>();

			public Builder WithFractionChecker(Fraction fraction) {
				entityInRangeFinder_.fraction_ = fraction;
				return this;
			}

			public Builder WithSearchRange(int range) {
				entityInRangeFinder_.range_ = range;
				return this;
			}

			public EntityInRangeFinder<T> Build() => entityInRangeFinder_;
		}
	}
}
