using Game.Commands;
using Game.Components;
using Game.Entities;

namespace Game.Abilities {
	public class MoveAndAttackAbilityExecutionStrategy : AbilityExecutionStrategy<TargetData<IDamageable>> {
		private GridMovementComponent gridMovementComponent_;
		public MoveAndAttackAbilityExecutionStrategy(GridMovementComponent gridMovementComponent) {
			gridMovementComponent_ = gridMovementComponent;
		}
		public override void CastAbility(TargetData<IDamageable> targetData) {
			var moveCommand = new MoveCommand.Builder()
				.WithGridMovementComponent(gridMovementComponent_)
				.WithDestination(targetData.Hex)
				.Build();
			var dealDamageCommand = new DealDamageCommand.Builder()
				.WithDamage(1)
				.WithTargetedEntity(targetData.Target)
				.Build();
			gridMovementComponent_.MovementFinished += () => dealDamageCommand.Execute();
			moveCommand.Execute();
			OnAbilityExecuted();
		}
	}
}

