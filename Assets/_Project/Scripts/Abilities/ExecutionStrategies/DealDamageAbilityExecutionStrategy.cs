using System.Collections.Generic;
using Game.Components;

namespace Game.Abilities {
	public class DealDamageAbilityExecutionStrategy : AbilityExecutionStrategy {
		private int damage_ = 1;
		private DealDamageAbilityExecutionStrategy() { }
		private void DealDamage(TargetData target) {
			if (target.Hex.OccupiedEntity == null) {
				return;
			}
			if (target.Hex.OccupiedEntity.TryGetComponent(out HealthComponent healthComponent)) {
				healthComponent.DealDamage(damage_);
			}
		}
		public override void CastAbility(List<TargetData> targetData) {
			foreach (var target in targetData) {
				DealDamage(target);
			}
			OnAbilityExecuted();
		}
		public override void CastAbility(TargetData targetData) {
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

