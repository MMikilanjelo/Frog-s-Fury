using Game.Components;
using UnityEngine;

namespace Game.Abilities {
	public class DealDamageAbilityExecutionStrategy : AbilityExecutionStrategy {
		private int damage_ = 1;
		private DealDamageAbilityExecutionStrategy() { }
		public override void CastAbility(TargetData targetData) {
			if (targetData.Entity == null) {
				return;
			}
			if (targetData.Entity.TryGetComponent(out HealthComponent healthComponent)) {
				healthComponent.DealDamage(damage_);
			}
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

