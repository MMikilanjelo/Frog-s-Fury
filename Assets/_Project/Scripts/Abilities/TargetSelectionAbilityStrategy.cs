using System;
using System.Collections.Generic;
using Game.Entities;

namespace Game.Abilities {
	public class TargetSelectionAbilityStrategy : AbilityStrategy {
		private readonly AbilityExecutionStrategy abilityExecutionStrategy_;
		private readonly AbilityTargetsFinderStrategy abilityTargetFinderStrategy_;
		private readonly AbilityTargetSelectionStrategy abilityTargetSelectionStrategy_;
		private TargetSelectionAbilityStrategy(
										AbilityExecutionStrategy abilityExecutionStrategy,
										AbilityTargetsFinderStrategy abilityTargetFinderStrategy,
										AbilityTargetSelectionStrategy abilityTargetSelectionStrategy,
										Entity entity) {
			abilityTargetSelectionStrategy_ = abilityTargetSelectionStrategy;
			abilityExecutionStrategy_ = abilityExecutionStrategy;
			abilityTargetFinderStrategy_ = abilityTargetFinderStrategy;
			Entity = entity;
			ConnectAbilityExecutionStrategy();
			ConnectAbilityFinderStrategy();
			ConnectAbilityTargetSelectionStrategy();
		}
		private void ConnectAbilityTargetSelectionStrategy() {
			abilityTargetSelectionStrategy_.TargetSelected += (TargetData targetData) => {
				DisableAbility();
				abilityTargetSelectionStrategy_.EndSelection();
				abilityExecutionStrategy_.CastAbility(targetData);
			};
		}
		private void ConnectAbilityExecutionStrategy() {
			abilityExecutionStrategy_.AbilityExecuted += () => {
				Entity.PerformAbility(ExecutionCost);
				OnAbilityExecuted();
			};
		}
		private void ConnectAbilityFinderStrategy() {
			abilityTargetFinderStrategy_.TargetsFind += (List<TargetData> targetsData) => {
				abilityTargetSelectionStrategy_.SelectTarget(targetsData);
			};
		}

		public override void CastAbility() {
			if (abilityTargetFinderStrategy_.TryFindTargets(Entity, out List<TargetData> targets)) {
				abilityTargetFinderStrategy_.OnTargetsFind(targets);
			}
		}

		public override void CancelAbility() => abilityTargetSelectionStrategy_.EndSelection();

		public override bool CanCastAbility() {
			return abilityTargetFinderStrategy_.TryFindTargets(Entity, out List<TargetData> data) &&
										 Entity.CanPerformAbility(ExecutionCost) && Enabled;
		}




		public class Builder {
			private AbilityExecutionStrategy abilityExecutionStrategy_;
			private AbilityTargetsFinderStrategy abilityTargetFinderStrategy_;
			private AbilityTargetSelectionStrategy abilityTargetSelectionStrategy_;
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
			public Builder WithAbilityTargetSelectionStrategy(AbilityTargetSelectionStrategy abilityTargetSelectionStrategy) {
				abilityTargetSelectionStrategy_ = abilityTargetSelectionStrategy;
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

			public TargetSelectionAbilityStrategy Build() {
				if (abilityExecutionStrategy_ == null ||
						abilityTargetFinderStrategy_ == null ||
						entity_ == null) {
					throw new InvalidOperationException("Ability execution strategy, target finder strategy, and entity must be set before building the strategy.");
				}

				TargetSelectionAbilityStrategy targetSelectionAbilityStrategy = new(
					abilityExecutionStrategy_,
					abilityTargetFinderStrategy_,
					abilityTargetSelectionStrategy_,
					entity_) {
					ExecutionCost = cost_
				};

				return targetSelectionAbilityStrategy;
			}
		}
	}
}
