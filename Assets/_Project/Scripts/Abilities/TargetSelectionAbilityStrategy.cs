
using System;
using System.Collections.Generic;
using Game.Entities;

namespace Game.Abilities {
	public class TargetSelectionAbilityStrategy<T> : AbilityStrategy<T> where T : class , ITargetData {
		private readonly AbilityExecutionStrategy<T> abilityExecutionStrategy_;
		private readonly AbilityTargetsFinderStrategy<T> abilityTargetFinderStrategy_;
		private readonly AbilityTargetSelectionStrategy<T> abilityTargetSelectionStrategy_;

		private TargetSelectionAbilityStrategy(
						AbilityExecutionStrategy<T> abilityExecutionStrategy,
						AbilityTargetsFinderStrategy<T> abilityTargetFinderStrategy,
						AbilityTargetSelectionStrategy<T> abilityTargetSelectionStrategy,
						AbilityPerformer abilityPerformer) {

			abilityExecutionStrategy_ = abilityExecutionStrategy;
			abilityTargetFinderStrategy_ = abilityTargetFinderStrategy;
			abilityTargetSelectionStrategy_ = abilityTargetSelectionStrategy;
			AbilityPerformer = abilityPerformer;

			ConnectAbilityExecutionStrategy();
			ConnectAbilityFinderStrategy();
			ConnectAbilityTargetSelectionStrategy();
		}

		private void ConnectAbilityTargetSelectionStrategy() {
			abilityTargetSelectionStrategy_.TargetSelected += (T targetData) => {
				OnAbilityCasted();
				abilityTargetSelectionStrategy_.EndSelection();
				abilityExecutionStrategy_.CastAbility(targetData);
			};
		}

		private void ConnectAbilityExecutionStrategy() {
			abilityExecutionStrategy_.AbilityExecuted += () => {
				AbilityPerformer.PerformAbility(ExecutionCost);
				OnAbilityExecuted();
			};
		}

		private void ConnectAbilityFinderStrategy() {
			abilityTargetFinderStrategy_.TargetsFind += (List<T> targetsData) => {
				abilityTargetSelectionStrategy_.SelectTarget(targetsData);
			};
		}

		public override void CastAbility() {
			if (abilityTargetFinderStrategy_.TryFindTargets(AbilityPerformer, out List<T> targets)) {
				abilityTargetFinderStrategy_.OnTargetsFind(targets);
			}
		}

		public override void CancelAbility() => abilityTargetSelectionStrategy_.EndSelection();

		public override bool CanCastAbility() {
			return abilityTargetFinderStrategy_.TryFindTargets(AbilityPerformer, out List<T> data) &&
						 AbilityPerformer.CanPerformAbility(ExecutionCost) && Enabled;
		}

		public class Builder {
			private AbilityExecutionStrategy<T> abilityExecutionStrategy_;
			private AbilityTargetsFinderStrategy<T> abilityTargetFinderStrategy_;
			private AbilityTargetSelectionStrategy<T> abilityTargetSelectionStrategy_;
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

			public Builder WithAbilityTargetSelectionStrategy(AbilityTargetSelectionStrategy<T> abilityTargetSelectionStrategy) {
				abilityTargetSelectionStrategy_ = abilityTargetSelectionStrategy;
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

			public TargetSelectionAbilityStrategy<T> Build() {
				if (abilityExecutionStrategy_ == null ||
						abilityTargetFinderStrategy_ == null ||
						abilityTargetSelectionStrategy_ == null ||
						abilityPerformer_ == null) {
					throw new InvalidOperationException("Ability execution strategy, target finder strategy, target selection strategy, and ability performer must be set before building the strategy.");
				}

				TargetSelectionAbilityStrategy<T> targetSelectionAbilityStrategy = new TargetSelectionAbilityStrategy<T>(
						abilityExecutionStrategy_,
						abilityTargetFinderStrategy_,
						abilityTargetSelectionStrategy_,
						abilityPerformer_) {
					ExecutionCost = cost_
				};

				return targetSelectionAbilityStrategy;
			}
		}
	}
}
