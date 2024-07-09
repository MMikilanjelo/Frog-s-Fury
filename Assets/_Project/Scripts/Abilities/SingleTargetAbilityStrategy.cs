using System;
using System.Collections.Generic;
using System.Linq;
using Game.Entities;

namespace Game.Abilities {
	public class SingleTargetAbilityStrategy : AbilityStrategy {
		private readonly AbilityExecutionStrategy abilityExecutionStrategy_;
		private readonly AbilityTargetsFinderStrategy abilityTargetFinderStrategy_;
		private SingleTargetAbilityStrategy(
						AbilityExecutionStrategy abilityExecutionStrategy,
						AbilityTargetsFinderStrategy abilityTargetFinderStrategy,
						Entity entity) {

			abilityExecutionStrategy_ = abilityExecutionStrategy;
			abilityTargetFinderStrategy_ = abilityTargetFinderStrategy;
			Entity = entity;

			abilityExecutionStrategy_.AbilityExecuted += () => {
				Entity.PerformAbility(ExecutionCost);
				OnAbilityExecuted();
			};

			abilityTargetFinderStrategy_.TargetsFind += (List<TargetData> targetsData) => {
				DisableAbility();
				abilityExecutionStrategy_?.CastAbility(targetsData);
			};
		}
		public override void CastAbility() {
			if (abilityTargetFinderStrategy_.TryFindTargets(Entity, out List<TargetData> targets)) {
				abilityTargetFinderStrategy_.OnTargetsFind(targets);
			}
		}
		public override bool CanCastAbility() {
			return abilityTargetFinderStrategy_.TryFindTargets(Entity, out List<TargetData> data) &&
						 Entity.CanPerformAbility(ExecutionCost) && Enabled;
		}


		public class Builder {
			private AbilityExecutionStrategy abilityExecutionStrategy_;
			private AbilityTargetsFinderStrategy abilityTargetFinderStrategy_;
			private Entity entity_;
			private int cost_ = 1;
			public Builder WithAbilityExecutionStrategy(AbilityExecutionStrategy abilityExecutionStrategy) {
				abilityExecutionStrategy_ = abilityExecutionStrategy;
				return this;
			}
			public Builder WithAbilityTargetFinderStrategy(AbilityTargetsFinderStrategy abilityTargetFinderStrategy) {
				abilityTargetFinderStrategy_ = abilityTargetFinderStrategy;
				return this;
			}
			public Builder WithAbilityCost(int cost) {
				if (cost > 0) {
					cost_ = cost;
				}
				return this;
			}
			public Builder WithEntity(Entity entity) {
				entity_ = entity;
				return this;
			}
			public SingleTargetAbilityStrategy Build() {
				if (abilityExecutionStrategy_ == null ||
						abilityTargetFinderStrategy_ == null ||
						entity_ == null) {
					throw new InvalidOperationException("Ability execution strategy, target finder strategy, and entity must be set before building the strategy.");
				}
				SingleTargetAbilityStrategy targetedAbilityStrategy = new SingleTargetAbilityStrategy(
						abilityExecutionStrategy_,
						abilityTargetFinderStrategy_,
						entity_) {
					ExecutionCost = cost_
				};
				return targetedAbilityStrategy;
			}
		}

	}
}