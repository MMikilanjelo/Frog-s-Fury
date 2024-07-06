using Game.Entities;
using Game.Utils.Helpers;
namespace Game.Abilities {
	public class EntityInRangeFinder : AbilityTargetsFinderStrategy {
		private HexNodeFlags flags_ = HexNodeFlags.OCCUPIED_BY_ENEMY;
		public int range_ = 1;
		private EntityInRangeFinder() { }
		public override bool TryFindTargets(Entity seeker) {
			TargetsData.Clear();
			var possibleTargets = HexagonalGridHelper.FindHexesWithinAxialDistance(seeker.OccupiedHex, range_, flags_);
			foreach (var targetHex in possibleTargets) {
				TargetsData.Add(new TargetData {
					Entity = targetHex.OccupiedEntity,
				});
			}
			return TargetsData.Count > 0;
		}
		public class Builder {
			private readonly EntityInRangeFinder entityInRangeFinder_ = new();
			public Builder WithFlags(HexNodeFlags flag) {
				if (flag == HexNodeFlags.OCCUPIED_BY_CHARACTER || flag == HexNodeFlags.OCCUPIED_BY_ENEMY) {
					entityInRangeFinder_.flags_ = flag;
				}
				return this;
			}
			public Builder WithAttackRange(int range) {
				entityInRangeFinder_.range_ = range;
				return this;
			}
			public EntityInRangeFinder Build() => entityInRangeFinder_;
		}
	}
}
