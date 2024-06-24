using System.Collections.Generic;
using Game.Hexagons;
using Game.Managers;
using Game.Selection;
using Game.Utils.Helpers;
namespace Game.Abilities {
	public class WalkableTileSelectionStrategy : AbilitySelectionStrategy {
		private readonly WalkableHexSelectionResponseDecorator walkableHexSelectionResponseDecorator_;
		public WalkableTileSelectionStrategy() {
			walkableHexSelectionResponseDecorator_ = new WalkableHexSelectionResponseDecorator(
				(Hex selectedHex) => {
					OnTargetSelected(selectedHex);
				});
		}
		public override void StartSelection(AbilityData abilityData) {
			if (abilityData is MoveAbilityData moveAbilityData) {
				HashSet<Hex> walkableHexes = HexagonalGridHelper.FindHexNodesWithinDistance(SelectionManager.Instance.SelectedCharacter.OccupiedHex, moveAbilityData.Range, HexNodeFlags.Walkable);
				HighlightManager.Instance.HighlightHexes(walkableHexes, abilityData.HighlightType);
				SelectionManager.Instance.DecorateSelectionResponse(walkableHexSelectionResponseDecorator_);
			}
		}
		public override void EndSelection() {
			HighlightManager.Instance.UnHighLightHexes();
			SelectionManager.Instance.UnDecorateSelectionResponse(walkableHexSelectionResponseDecorator_);
		}
	}
}
