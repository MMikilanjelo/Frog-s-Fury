using System;
using System.Collections.Generic;
using Game.Entities;
using Game.Hexagons;

namespace Game.Abilities {
	public class TargetedAbilityStrategy : IAbilityStrategy {
		private readonly AbilityTargetSelectionStrategy abilityTargetSelectionStrategy_;
		private readonly AbilityExecutionStrategy abilityExecutionStrategy_;
		private readonly AbilityTargetFinderStrategy abilityTargetFinderStrategy_;
		private readonly Entity entity_;
		private int executionCost_ = 1;

		private TargetedAbilityStrategy(
						AbilityTargetSelectionStrategy abilitySelectionStrategy,
						AbilityExecutionStrategy abilityExecutionStrategy,
						AbilityTargetFinderStrategy abilityTargetFinderStrategy,
						Entity entity) {

			abilityExecutionStrategy_ = abilityExecutionStrategy;
			abilityTargetSelectionStrategy_ = abilitySelectionStrategy;
			abilityTargetFinderStrategy_ = abilityTargetFinderStrategy;
			entity_ = entity;

			abilityTargetSelectionStrategy_.TargetsSelected += (HashSet<Hex> selectedHexes) => {
				abilityExecutionStrategy_?.CastAbility(selectedHexes);
			};

			abilityExecutionStrategy_.AbilityExecuted += () => {
				entity_.PerformAbility(executionCost_);
				abilityTargetSelectionStrategy_.EndSelection();
			};

			abilityTargetFinderStrategy_.TargetsFind += (HashSet<Hex> targets) => {
				abilityTargetSelectionStrategy_?.SelectTarget(targets);
			};
		}
		public void CastAbility() => abilityTargetFinderStrategy_.FindTargets(entity_);
		public void CancelAbility() => abilityTargetSelectionStrategy_?.EndSelection();
		public bool CanCastAbility() {
			return abilityTargetFinderStrategy_.TryFindTargets(entity_) &&
						 entity_.CanPerformAbility(executionCost_);
		}
		public class Builder {
			private AbilityTargetSelectionStrategy abilityTargetSelectionStrategy_;
			private AbilityExecutionStrategy abilityExecutionStrategy_;
			private AbilityTargetFinderStrategy abilityTargetFinderStrategy_;
			private Entity entity_;
			private int cost_ = 1;
			public Builder WithAbilitySelectionStrategy(AbilityTargetSelectionStrategy abilitySelectionStrategy) {
				abilityTargetSelectionStrategy_ = abilitySelectionStrategy;
				return this;
			}

			public Builder WithAbilityExecutionStrategy(AbilityExecutionStrategy abilityExecutionStrategy) {
				abilityExecutionStrategy_ = abilityExecutionStrategy;
				return this;
			}
			public Builder WithAbilityTargetFinderStrategy(AbilityTargetFinderStrategy abilityTargetFinderStrategy) {
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

			public TargetedAbilityStrategy Build() {
				if (abilityExecutionStrategy_ == null ||
						abilityTargetFinderStrategy_ == null ||
						abilityTargetSelectionStrategy_ == null ||
						entity_ == null) {
					throw new InvalidOperationException("Ability execution strategy, target finder strategy, and entity must be set before building the strategy.");
				}
				TargetedAbilityStrategy targetedAbilityStrategy = new TargetedAbilityStrategy(
						abilityTargetSelectionStrategy_,
						abilityExecutionStrategy_,
						abilityTargetFinderStrategy_,
						entity_) {
					executionCost_ = cost_
				};
				return targetedAbilityStrategy;
			}
		}
	}
}