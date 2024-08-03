using Game.Commands;
using Game.Components;
namespace Game.Abilities {
	public class MoveAbilityExecutionStrategy<T> : AbilityExecutionStrategy<T> where T : TargetData {
		private GridMovementComponent gridMovementComponent_;
		public MoveAbilityExecutionStrategy(GridMovementComponent gridMovementComponent) {
			gridMovementComponent_ = gridMovementComponent;
		}
		public override void CastAbility(T targetData) {
			var moveCommand = new MoveCommand.Builder()
				.WithGridMovementComponent(gridMovementComponent_)
				.WithDestination(targetData.Hex)
				.Build();
			moveCommand.Execute();
			OnAbilityExecuted();
		}
	}
}

