using System;
using Game.Entities;
using Game.Entities.Characters;
using Game.Utils.Helpers;
namespace Game.Abilities {
	public class NearestWalkableHexAroundCharacterFinder : AbilitySingleTargetFinderStrategy {
		private int searchRange_ = 1;
		private NearestWalkableHexAroundCharacterFinder() { }
		private Func<CharacterTypes, bool> evaluateFunc_ = (character) => true;
		public override void FindTarget(Entity seeker) {
			if (TryFindTarget(seeker)) {
				OnTargetsFind(Target);
			}
		}
		public override bool TryFindTarget(Entity seeker) {
			Target = null;
			var closesHexesNearCharacters = HexagonalGridHelper.FindClosestWalkableHexesNearCharactersWithinDistance(seeker.OccupiedHex, searchRange_);
			foreach (var (character, hex) in closesHexesNearCharacters) {
				if (evaluateFunc_(character.Data.Type)) {
					Target = hex;
				}
			}
			return Target != null;
		}

		public class Builder {
			private readonly NearestWalkableHexAroundCharacterFinder pathDistanceHexFinder_ = new NearestWalkableHexAroundCharacterFinder();
			public Builder WithSearchRange(int searchRange) {
				pathDistanceHexFinder_.searchRange_ = searchRange;
				return this;
			}
			public Builder WithCharacterPriorityEvaluateFunction(Func<CharacterTypes, bool> func) {
				pathDistanceHexFinder_.evaluateFunc_ = func;
				return this;
			}
			public NearestWalkableHexAroundCharacterFinder Build() {
				return pathDistanceHexFinder_;
			}
		}
	}
}

