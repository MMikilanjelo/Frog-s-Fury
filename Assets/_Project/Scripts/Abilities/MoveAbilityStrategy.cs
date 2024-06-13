using Game.Components;
using Game.Managers;
using Game.Selection;
namespace Game.Abilities {
	public class MoveAbilityStrategy : AbilityStrategy {
		private TurnActionCounterComponent turnActionCounterComponent_;
		private GridMovementComponent gridMovementComponent_;
		private SelectionResponseDecorator selectionResponseDecorator_;
		private MoveAbilityStrategy(GridMovementComponent gridMovementComponent, TurnActionCounterComponent turnActionCounterComponent) {
			gridMovementComponent_ = gridMovementComponent;
			turnActionCounterComponent_ = turnActionCounterComponent;
			ConnectGridMovementComponent();
			SetUpSelectionResponseDecorator();
		}
		private void SetUpSelectionResponseDecorator() {
			selectionResponseDecorator_ = new WalkableHexSelectionDecorator(SelectionManager.Instance.SelectionResponse,
				(SelectionData selectionData) => ExecuteAbility(selectionData));
		}
		private void ConnectGridMovementComponent() {
			gridMovementComponent_.MovementStarted += 
				() => turnActionCounterComponent_.PerformAction(AbilityData.AbilityCost);
		}
		public override void CastAbility() {
			SelectionManager.Instance.SetSelectionResponse(selectionResponseDecorator_);
		}
		public override void CancelAbility() {
			SelectionManager.Instance.SetSelectionResponse(selectionResponseDecorator_.WrappedResponse);
		}
		private void ExecuteAbility(SelectionData selectionData) {
			SelectionManager.Instance.SetSelectionResponse(selectionResponseDecorator_.WrappedResponse);
			gridMovementComponent_.Move(selectionData.SelectedHexNode);
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
