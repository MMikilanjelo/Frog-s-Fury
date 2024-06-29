using System;
using Game.Entities;
using Game.Managers;
using Game.Utils.Helpers;
using UnityEngine;

namespace Game.Abilities {
	public class NearestCharacterTileSelectionStrategy : AbilitySelectionStrategy {
		private Action action_ = delegate { };
		private Entity entity_;
		private NearestCharacterTileSelectionStrategy() { }
		public override void StartSelection(AbilityData abilityData) {
			if (entity_ == null) {
				return;
			}
			if (abilityData is MoveAbilityData moveAbilityData) {
				var nearestHex = HexagonalGridHelper.FindNearestOccupiedHexes(entity_.OccupiedHex, UnitManager.Instance.Characters.Values);
				if (nearestHex != null) {
					OnTargetSelected(nearestHex);
				}
			}
		}

		public class Builder {
			private readonly NearestCharacterTileSelectionStrategy nearestCharacterTileSelectionStrategy_ = new();
			public Builder WithAction(Action action) {
				nearestCharacterTileSelectionStrategy_.action_ = action;
				return this;
			}
			public Builder WithEntity(Entity entity) {
				nearestCharacterTileSelectionStrategy_.entity_ = entity;
				return this;
			}
			public NearestCharacterTileSelectionStrategy Build() {
				return nearestCharacterTileSelectionStrategy_;
			}
		}
	}
}
