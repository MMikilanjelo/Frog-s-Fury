using System.Collections.Generic;
using Game.Components;
namespace Game.Abilities {
	public class MoveAbilityExecutionStrategy : AbilityExecutionStrategy {
		private GridMovementComponent gridMovementComponent_;
		public MoveAbilityExecutionStrategy(GridMovementComponent gridMovementComponent) {
			gridMovementComponent_ = gridMovementComponent;
			gridMovementComponent_.MovementFinished += () => OnAbilityExecuted();
		}
		public override void CastAbility(TargetData targetData) {
			var path = gridMovementComponent_.FindPath(targetData.Hex);
			gridMovementComponent_.Move(path);
		}
	}
}

