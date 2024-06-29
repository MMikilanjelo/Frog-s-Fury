using Game.Components;
using Game.Hexagons;
namespace Game.Abilities {
	public class TargetedAbilityStrategy : IAbilityStrategy {
		private readonly AbilitySelectionStrategy abilitySelectionStrategy_;
		private readonly AbilityExecutionStrategy abilityExecutionStrategy_;
		private readonly TurnActionCounterComponent turnActionCounterComponent_;
		private AbilityData abilityData_;
		private TargetedAbilityStrategy(AbilitySelectionStrategy abilitySelectionStrategy, AbilityExecutionStrategy abilityExecutionStrategy, TurnActionCounterComponent turnActionCounterComponent) {
			abilitySelectionStrategy_ = abilitySelectionStrategy;
			abilityExecutionStrategy_ = abilityExecutionStrategy;
			turnActionCounterComponent_ = turnActionCounterComponent;

			abilitySelectionStrategy_.TargetSelected += (Hex selectedHex) => {
				abilitySelectionStrategy_?.EndSelection();
				abilityExecutionStrategy_?.CastAbility(selectedHex, abilityData_);
			};
			abilityExecutionStrategy_.AbilityExecuted += () => {
				turnActionCounterComponent_?.PerformAction(abilityData_.Cost);
			};
		}
		public void SetAbilityData(AbilityData abilityData) => abilityData_ = abilityData;
		public void CastAbility() => abilitySelectionStrategy_?.StartSelection(abilityData_);
		public void CancelAbility() => abilitySelectionStrategy_?.EndSelection();
		public class Builder {
			private AbilitySelectionStrategy abilitySelectionStrategy_;
			private AbilityExecutionStrategy abilityExecutionStrategy_;
			private TurnActionCounterComponent turnActionCounterComponent_;
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
			public TargetedAbilityStrategy Build() {
				return new TargetedAbilityStrategy(abilitySelectionStrategy_, abilityExecutionStrategy_, turnActionCounterComponent_);
			}
		}
	}
}
