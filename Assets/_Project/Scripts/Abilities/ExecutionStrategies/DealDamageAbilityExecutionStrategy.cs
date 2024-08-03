using Game.Commands;
using Game.Entities;

namespace Game.Abilities {
	public class DealDamageAbilityExecutionStrategy : AbilityExecutionStrategy<TargetDataWithAdditionalTarget<IDamageable>> {
		private int damage_ = 1;
		private DealDamageAbilityExecutionStrategy() { }
		private void DealDamage(TargetDataWithAdditionalTarget<IDamageable> targetData) {
			
			if (targetData.Hex.OccupiedEntity == null) {
				return;
			}
			var dealDamageCommand = new DealDamageCommand.Builder()
				.WithDamage(damage_)
				.WithTargetedEntity(targetData.Target)
				.Build();
			dealDamageCommand.Execute();
		}
		public override void CastAbility(TargetDataWithAdditionalTarget<IDamageable> targetData) {
			DealDamage(targetData);
			OnAbilityExecuted();
		}
		public class Builder {
			private readonly DealDamageAbilityExecutionStrategy dealDamageAbilityExecutionStrategy_ = new();
			public Builder WithDamage(int damage) {
				if (damage > 0) {
					dealDamageAbilityExecutionStrategy_.damage_ = damage;
				}
				return this;
			}
			public DealDamageAbilityExecutionStrategy Build() => dealDamageAbilityExecutionStrategy_;
		}
	}
}

