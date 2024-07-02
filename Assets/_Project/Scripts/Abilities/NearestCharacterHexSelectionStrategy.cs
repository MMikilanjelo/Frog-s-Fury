
using System.Collections.Generic;
using Game.Entities;
using Game.Hexagons;
using Game.Managers;
using Game.Utils.Helpers;

namespace Game.Abilities {
	public class NearestCharacterHexSelectionStrategy : AbilitySelectionStrategy {
		private Entity entity_;
		private NearestCharacterHexSelectionStrategy() { }
		public override void EndSelection() { }
		public override void StartSelection(HashSet<Hex> targets) {
			if (entity_ == null || AbilityData is not MoveAbilityData) {
				return;
			}
			var target = FindNearestOccupiedHex();
			if (target != null) {
				OnTargetSelected(target);
			}
		}
		private Hex FindNearestOccupiedHex() {
			if (AbilityData is MoveAbilityData moveAbilityData) {
				return HexagonalGridHelper.FindNearestOccupiedHexWithInRange(
						entity_.OccupiedHex,
						UnitManager.Instance.Characters.Values,
						moveAbilityData.Range
				);
			}
			return null;
		}

		public class Builder {
			private readonly NearestCharacterHexSelectionStrategy nearestCharacterHexSelectionStrategy_ = new();

			public Builder WithEntity(Entity entity) {
				nearestCharacterHexSelectionStrategy_.entity_ = entity;
				return this;
			}

			public NearestCharacterHexSelectionStrategy Build() {
				return nearestCharacterHexSelectionStrategy_;
			}
		}
	}
}