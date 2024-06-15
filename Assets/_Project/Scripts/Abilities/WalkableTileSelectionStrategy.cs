
using Game.Managers;
using Game.Selection;
using Game.Utils.Helpers;

namespace Game.Abilities {
	public class WalkableTileSelectionStrategy : AbilitySelectionStrategy {
		private readonly WalkableHexSelectionResponseDecorator walkableHexSelectionResponseDecorator_;
		public WalkableTileSelectionStrategy(AbilityStrategy abilityStrategy) : base(abilityStrategy) {
			walkableHexSelectionResponseDecorator_ = new WalkableHexSelectionResponseDecorator(
				(SelectionData data) => {
					EndSelection();
					abilityStrategy.CastAbility(data);
				});
		}
		public override void StartSelection() {
			var nodes = HexagonalGridHelper.FindHexNodesWithinDistance(SelectionManager.Instance.SelectedCharacter.OccupiedHex , 3 , HexNodeFlags.Walkable);
			SelectionManager.Instance.DecorateSelectionResponse(walkableHexSelectionResponseDecorator_);
		}
		public override void EndSelection() => SelectionManager.Instance.UnDecorateSelectionResponse(walkableHexSelectionResponseDecorator_);
	}
}
