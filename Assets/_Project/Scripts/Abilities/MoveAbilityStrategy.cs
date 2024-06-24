using System.Collections.Generic;
using Game.Components;
using Game.Hexagons;
using Game.Selection;
namespace Game.Abilities {
	public class MoveAbilityStrategy : AbilityExecutionStrategy {
		private GridMovementComponent gridMovementComponent_;
		public MoveAbilityStrategy(GridMovementComponent gridMovementComponent) {
			gridMovementComponent_ = gridMovementComponent;
		}
		public override void CastAbility(Hex selectedHex, AbilityData abilityData) {
			if (abilityData is MoveAbilityData moveData) {
				if (gridMovementComponent_.DestinationReachable(selectedHex, out List<Hex> path, moveData.Range)) {
					gridMovementComponent_.Move(path);
					OnAbilityExecuted();
				}
			}
		}

	}
}
