using Game.Commands;
using Game.Components;
using Game.Entities;

namespace Game.Abilities {
	public class MoveAndAttackAbilityExecutionStrategy : AbilityExecutionStrategy<TargetDataWithAdditionalTarget<IDamageable>> {
		private readonly GridMovementComponent gridMovementComponent_;
		public MoveAndAttackAbilityExecutionStrategy(GridMovementComponent gridMovementComponent) {
			gridMovementComponent_ = gridMovementComponent;
		}
		public override void CastAbility(TargetDataWithAdditionalTarget<IDamageable> targetData) {
			var moveCommand = new MoveCommand.Builder()
				.WithGridMovementComponent(gridMovementComponent_)
				.WithDestination(targetData.Hex)
				.Build();
			var dealDamageCommand = new DealDamageCommand.Builder()
				.WithDamage(1)
				.WithTargetedEntity(targetData.Target)
				.Build();
			void OnMovementFinished() {
				dealDamageCommand.Execute();
				gridMovementComponent_.MovementFinished -= OnMovementFinished;
			}
			gridMovementComponent_.MovementFinished += OnMovementFinished;
			moveCommand.Execute();
			OnAbilityExecuted();
		}
	}



}

