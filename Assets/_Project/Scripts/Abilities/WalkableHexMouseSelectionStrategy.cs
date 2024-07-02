using System.Collections.Generic;
using Game.Entities;
using Game.Hexagons;
using Game.Managers;
using Game.Selection;
using UnityEngine;
namespace Game.Abilities {
	public class WalkableHexMouseSelectionStrategy : AbilitySelectionStrategy {
		private readonly WalkableHexSelectionResponseDecorator walkableHexSelectionResponseDecorator_;
		public WalkableHexMouseSelectionStrategy() {
			walkableHexSelectionResponseDecorator_ = new WalkableHexSelectionResponseDecorator.Builder()
				.WithCallback((Hex selectedHex) => {
					OnTargetSelected(selectedHex);
				})
				.Build();
		}
		public override void StartSelection(HashSet<Hex> targets) {
			if (AbilityData is not MoveAbilityData) {
				return;
			}
			HighlightManager.Instance.HighlightHexes(targets, AbilityData.HighlightType);
			SelectionManager.Instance.DecorateSelectionResponse(walkableHexSelectionResponseDecorator_);
		}
		public override void EndSelection() {
			HighlightManager.Instance.UnHighLightHexes();
			SelectionManager.Instance.ResetSelectionResponseToDefault();
		}
	}
}
