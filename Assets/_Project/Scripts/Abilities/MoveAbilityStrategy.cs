using System.Collections.Generic;
using Game.Components;
using Game.Hexagons;
using Game.Selection;
namespace Game.Abilities {
	public class MoveAbilityStrategy : AbilityStrategy {
		private TurnActionCounterComponent turnActionCounterComponent_;
		private GridMovementComponent gridMovementComponent_;
		private MoveAbilityStrategy(GridMovementComponent gridMovementComponent, TurnActionCounterComponent turnActionCounterComponent) {
			gridMovementComponent_ = gridMovementComponent;
			turnActionCounterComponent_ = turnActionCounterComponent;
		}

		public override void CastAbility(SelectionData selectionData) {
			if(gridMovementComponent_.DestinationReachable(selectionData.SelectedHex , out List<Hex> path)){
				turnActionCounterComponent_.PerformAction(AbilityData.AbilityCost);
				gridMovementComponent_.Move(path);
			}
		}

		public class Builder {
			private GridMovementComponent gridMovementComponent_;
			private TurnActionCounterComponent turnActionCounterComponent_;
			public Builder WithTurnActionCounterComponent(TurnActionCounterComponent turnActionCounterComponent) {
				turnActionCounterComponent_ = turnActionCounterComponent;
				return this;
			}
			public Builder WithGridMovementComponent(GridMovementComponent gridMovementComponent) {
				gridMovementComponent_ = gridMovementComponent;
				return this;
			}
			public MoveAbilityStrategy Build() {
				return new MoveAbilityStrategy(gridMovementComponent_, turnActionCounterComponent_);
			}
		}
	}
}
