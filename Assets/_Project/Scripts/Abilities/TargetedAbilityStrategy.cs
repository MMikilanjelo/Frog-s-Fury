
using System;
using System.Collections.Generic;
using Game.Entities;

namespace Game.Abilities {
	public class TargetedAbilityStrategy<T> : AbilityStrategy<T> where T : TargetData {
		private readonly AbilityExecutionStrategy<T> abilityExecutionStrategy_;
		private readonly AbilityTargetsFinderStrategy<T> abilityTargetFinderStrategy_;

		private TargetedAbilityStrategy(
				AbilityExecutionStrategy<T> abilityExecutionStrategy,
				AbilityTargetsFinderStrategy<T> abilityTargetFinderStrategy,
				AbilityPerformer abilityPerformer) {

			abilityExecutionStrategy_ = abilityExecutionStrategy;
			abilityTargetFinderStrategy_ = abilityTargetFinderStrategy;
			AbilityPerformer = abilityPerformer;

			ConnectAbilityExecutionStrategy();
		}

		private void ConnectAbilityExecutionStrategy() {
			abilityExecutionStrategy_.AbilityExecuted += () => {
				AbilityPerformer.PerformAbility(ExecutionCost);
				OnAbilityExecuted();
			};
		}


		public override void CastAbility() {
			if (abilityTargetFinderStrategy_.TryFindTargets(AbilityPerformer, out List<T> targets)) {
				OnAbilityCasted();
				foreach (var target in targets) {
					abilityExecutionStrategy_.CastAbility(target);
				}
			}
		}

		public override bool CanCastAbility() {
			return abilityTargetFinderStrategy_.TryFindTargets(AbilityPerformer, out List<T> data) &&
						 AbilityPerformer.CanPerformAbility(ExecutionCost) && Enabled;
		}

		public class Builder {
			private AbilityExecutionStrategy<T> abilityExecutionStrategy_;
			private AbilityTargetsFinderStrategy<T> abilityTargetFinderStrategy_;
			private AbilityPerformer abilityPerformer_;
			private int cost_ = 1;

			public Builder WithAbilityExecutionStrategy(AbilityExecutionStrategy<T> abilityExecutionStrategy) {
				abilityExecutionStrategy_ = abilityExecutionStrategy;
				return this;
			}

			public Builder WithAbilityTargetFinderStrategy(AbilityTargetsFinderStrategy<T> abilityTargetFinderStrategy) {
				abilityTargetFinderStrategy_ = abilityTargetFinderStrategy;
				return this;
			}

			public Builder WithAbilityCost(int cost) {
				if (cost > 0) {
					cost_ = cost;
				}
				return this;
			}

			public Builder WithAbilityPerformer(AbilityPerformer abilityPerformer) {
				abilityPerformer_ = abilityPerformer;
				return this;
			}

			public TargetedAbilityStrategy<T> Build() {
				if (abilityExecutionStrategy_ == null ||
						abilityTargetFinderStrategy_ == null ||
						abilityPerformer_ == null) {
					throw new InvalidOperationException("Ability execution strategy, target finder strategy, and abilityPerformer must be set before building the strategy.");
				}

				TargetedAbilityStrategy<T> multiTargetedAbilityStrategy = new TargetedAbilityStrategy<T>(
						abilityExecutionStrategy_,
						abilityTargetFinderStrategy_,
						abilityPerformer_) {
					ExecutionCost = cost_
				};

				return multiTargetedAbilityStrategy;
			}
		}
	}
}
