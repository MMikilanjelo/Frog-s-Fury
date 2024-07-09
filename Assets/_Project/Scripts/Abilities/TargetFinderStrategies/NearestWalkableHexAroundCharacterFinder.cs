using System;
using System.Collections.Generic;
using Game.Entities;
using Game.Utils.Helpers;
namespace Game.Abilities {
	public class NearestWalkableHexAroundCharacterFinder : AbilityTargetsFinderStrategy {
		private int searchRange_ = 1;
		private NearestWalkableHexAroundCharacterFinder() { }
		private Func<EntityTypes, bool> evaluateFunc_ = (character) => true;
		public override bool TryFindTargets(Entity seeker, out List<TargetData> data) {
			var targets = new List<TargetData>();
			var closesHexesNearCharacters = HexagonalGridHelper.FindClosestWalkableHexesNearCharactersWithinDistance(seeker.OccupiedHex, searchRange_);
			foreach (var (character, hex) in closesHexesNearCharacters) {
				if (evaluateFunc_(character.Data.Type)) {
					targets.Add(new TargetData {
						Hex = hex,
					});
				}
			}
			data = targets;
			return targets.Count > 0;
		}

		public class Builder {
			private readonly NearestWalkableHexAroundCharacterFinder pathDistanceHexFinder_ = new NearestWalkableHexAroundCharacterFinder();
			public Builder WithSearchRange(int searchRange) {
				pathDistanceHexFinder_.searchRange_ = searchRange;
				return this;
			}
			public Builder WithCharacterPriorityEvaluateFunction(Func<EntityTypes, bool> func) {
				pathDistanceHexFinder_.evaluateFunc_ = func;
				return this;
			}
			public NearestWalkableHexAroundCharacterFinder Build() {
				return pathDistanceHexFinder_;
			}
		}
	}
}

