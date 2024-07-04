using System;
using System.Collections.Generic;
using Game.Entities;
using Game.Hexagons;

namespace Game.Abilities {
	public class MultiTargetedAbilityStrategy : IAbilityStrategy {
		private readonly AbilityExecutionStrategy abilityExecutionStrategy_;
		private readonly AbilityMultipleTargetsFinderStrategy abilityTargetFinderStrategy_;
		private readonly Entity entity_;
		private int executionCost_ = 1;
		private MultiTargetedAbilityStrategy(
										AbilityExecutionStrategy abilityExecutionStrategy,
										AbilityMultipleTargetsFinderStrategy abilityTargetFinderStrategy,
										Entity entity) {

			abilityExecutionStrategy_ = abilityExecutionStrategy;
			abilityTargetFinderStrategy_ = abilityTargetFinderStrategy;
			entity_ = entity;
			ConnectAbilityExecutionStrategy();
			ConnectAbilityFinderStrategy();
		}

		private void ConnectAbilityExecutionStrategy() {

			abilityExecutionStrategy_.AbilityExecuted += () => {
				entity_.PerformAbility(executionCost_);
			};

		}
		private void ConnectAbilityFinderStrategy() {
			abilityTargetFinderStrategy_.TargetsFind += (HashSet<Hex> targets) => {
				abilityExecutionStrategy_.CastAbility(targets);
			};
		}

		public void CastAbility() => abilityTargetFinderStrategy_.FindTargets(entity_);

		public void CancelAbility() { }

		public bool CanCastAbility() {
			return abilityTargetFinderStrategy_.TryFindTargets(entity_) &&
										 entity_.CanPerformAbility(executionCost_);
		}

		public class Builder {
			private AbilityExecutionStrategy abilityExecutionStrategy_;
			private AbilityMultipleTargetsFinderStrategy abilityTargetFinderStrategy_;
			private Entity entity_;
			private int cost_ = 1;

			public Builder WithAbilityExecutionStrategy(AbilityExecutionStrategy abilityExecutionStrategy) {
				abilityExecutionStrategy_ = abilityExecutionStrategy;
				return this;
			}

			public Builder WithAbilityTargetFinderStrategy(AbilityMultipleTargetsFinderStrategy abilityTargetFinderStrategy) {
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

			public MultiTargetedAbilityStrategy Build() {
				if (abilityExecutionStrategy_ == null ||
						abilityTargetFinderStrategy_ == null ||
						entity_ == null) {
					throw new InvalidOperationException("Ability execution strategy, target finder strategy, and entity must be set before building the strategy.");
				}

				MultiTargetedAbilityStrategy multiTargetedAbilityStrategy = new MultiTargetedAbilityStrategy(
								abilityExecutionStrategy_,
								abilityTargetFinderStrategy_,
								entity_) {
					executionCost_ = cost_
				};

				return multiTargetedAbilityStrategy;
			}
		}
	}
}
