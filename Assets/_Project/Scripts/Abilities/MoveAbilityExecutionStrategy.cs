using System.Collections.Generic;
using Game.Components;
using Game.Hexagons;
namespace Game.Abilities {
	public class MoveAbilityExecutionStrategy : AbilityExecutionStrategy {
		private GridMovementComponent gridMovementComponent_;
		public MoveAbilityExecutionStrategy(GridMovementComponent gridMovementComponent) {
			gridMovementComponent_ = gridMovementComponent;
		}
		public override void CastAbility(Hex selectedHex, AbilityData abilityData) {
			if (abilityData is MoveAbilityData moveData) {
				if (gridMovementComponent_.TryFindPathToDestination(selectedHex, out List<Hex> path, moveData.Range)) {
					gridMovementComponent_.Move(path);
					OnAbilityExecuted();
				}
			}
		}
		//TODO 
	}
}
