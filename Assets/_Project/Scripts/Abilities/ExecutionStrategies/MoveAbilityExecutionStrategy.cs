using Game.Components;
using Game.Hexagons;
namespace Game.Abilities {
	public class MoveAbilityExecutionStrategy : AbilityExecutionStrategy {
		private GridMovementComponent gridMovementComponent_;
		public MoveAbilityExecutionStrategy(GridMovementComponent gridMovementComponent) {
			gridMovementComponent_ = gridMovementComponent;
		}
		public override void CastAbility(TargetData targetData) {
			var path = gridMovementComponent_.FindPath(targetData.Hex);
			gridMovementComponent_.Move(path);
			OnAbilityExecuted();
		}
	}
}

