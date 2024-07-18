
using System;
using System.Collections.Generic;
using Game.Entities;
using Game.Utils.Helpers;

namespace Game.Abilities {
	public class EntityInRangeFinder<T> : AbilityTargetsFinderStrategy<T> where T : class, ITargetData {
		private Fraction fraction_ = Fraction.ENEMY;
		private int range_ = 1;

		private EntityInRangeFinder() { }

		public override bool TryFindTargets(Entity seeker, out List<T> data) {
			var targets = new List<T>();
			var possibleTargets = HexagonalGridHelper.FindHexesWithinAxialDistance(seeker.OccupiedHex, range_, fraction_);
			foreach (var targetHex in possibleTargets) {
				T targetData = Activator.CreateInstance(typeof(T), targetHex, targetHex.OccupiedEntity) as T;
				targets.Add(targetData);
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
