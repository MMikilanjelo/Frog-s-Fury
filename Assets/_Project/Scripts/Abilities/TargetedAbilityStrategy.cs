
using System;
using System.Collections.Generic;
using Game.Components;
using Game.Entities;
using Game.Hexagons;

namespace Game.Abilities {
	public class TargetedAbilityStrategy : IAbilityStrategy {
		private readonly AbilitySelectionStrategy abilitySelectionStrategy_;
		private readonly AbilityExecutionStrategy abilityExecutionStrategy_;
		private readonly AbilityTargetFinderStrategy abilityTargetFinderStrategy_;
		private readonly TurnActionCounterComponent turnActionCounterComponent_;
		private AbilityData abilityData_;
		private readonly Entity entity_;

		private TargetedAbilityStrategy(
				AbilitySelectionStrategy abilitySelectionStrategy,
				AbilityExecutionStrategy abilityExecutionStrategy,
				AbilityTargetFinderStrategy abilityTargetFinderStrategy,
				TurnActionCounterComponent turnActionCounterComponent,
				Entity entity) {
			abilityExecutionStrategy_ = abilityExecutionStrategy;
			abilitySelectionStrategy_ = abilitySelectionStrategy;
			abilityTargetFinderStrategy_ = abilityTargetFinderStrategy;
			turnActionCounterComponent_ = turnActionCounterComponent;
			entity_ = entity;

			abilitySelectionStrategy_.TargetSelected += (Hex selectedHex) => {
				if (abilityTargetFinderStrategy_.ValidateTarget(selectedHex)) {
					abilityExecutionStrategy_?.CastAbility(selectedHex);
				}
			};

			abilityExecutionStrategy_.AbilityExecuted += () => {
				abilitySelectionStrategy_?.EndSelection();
				turnActionCounterComponent_?.PerformAction(abilityData_.Cost);
			};

			abilityTargetFinderStrategy_.TargetsFind += (HashSet<Hex> targets) => {
				abilitySelectionStrategy_.StartSelection(targets);
			};
		}

		public void SetAbilityData(AbilityData abilityData) {
			abilityData_ = abilityData;
			abilityExecutionStrategy_.SetAbilityData(abilityData_);
			abilitySelectionStrategy_.SetAbilityData(abilityData_);
			abilityTargetFinderStrategy_.SetAbilityData(abilityData_);
		}

		public void CastAbility() => abilityTargetFinderStrategy_.FindTargets(entity_);
		public void CancelAbility() => abilitySelectionStrategy_?.EndSelection();
		public bool CanCastAbility() => abilityTargetFinderStrategy_.TryFindTargets(entity_);
		public class Builder {
			private AbilitySelectionStrategy abilitySelectionStrategy_;
			private AbilityExecutionStrategy abilityExecutionStrategy_;
			private AbilityTargetFinderStrategy abilityTargetFinderStrategy_;
			private TurnActionCounterComponent turnActionCounterComponent_;
			private Entity entity_;

			public Builder WithAbilitySelectionStrategy(AbilitySelectionStrategy abilitySelectionStrategy) {
				abilitySelectionStrategy_ = abilitySelectionStrategy;
				return this;
			}

			public Builder WithAbilityExecutionStrategy(AbilityExecutionStrategy abilityExecutionStrategy) {
				abilityExecutionStrategy_ = abilityExecutionStrategy;
				return this;
			}

			public Builder WithTurnActionCounterComponent(TurnActionCounterComponent turnActionCounterComponent) {
				turnActionCounterComponent_ = turnActionCounterComponent;
				return this;
			}

			public Builder WithAbilityTargetFinderStrategy(AbilityTargetFinderStrategy abilityTargetFinderStrategy) {
				abilityTargetFinderStrategy_ = abilityTargetFinderStrategy;
				return this;
			}

			public Builder WithEntity(Entity entity) {
				entity_ = entity;
				return this;
			}

			public TargetedAbilityStrategy Build() {
				if (abilitySelectionStrategy_ == null ||
						abilityExecutionStrategy_ == null ||
						abilityTargetFinderStrategy_ == null ||
						turnActionCounterComponent_ == null ||
						entity_ == null) {
					throw new InvalidOperationException("All components must be set before building the strategy.");
				}
				return new TargetedAbilityStrategy(
						abilitySelectionStrategy_,
						abilityExecutionStrategy_,
						abilityTargetFinderStrategy_,
						turnActionCounterComponent_,
						entity_);
			}
		}
	}
}