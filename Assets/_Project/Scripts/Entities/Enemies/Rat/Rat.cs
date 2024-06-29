using UnityEngine;
using Game.Components;
using Game.Core.Logic;
using Game.Abilities;
using Game.Systems;
using Game.Managers;
namespace Game.Entities.Enemies {
	public class Rat : Enemy, IActionPerformer {
		#region SerializeFields
		[SerializeField, Range(1, 4)] private int actionsCount_;

		#endregion
		private TurnActionCounterComponent turnActionCounterComponent_;
		private GridMovementComponent gridMovementComponent_;
		private readonly StateMachine stateMachine_ = new();
		private void Awake() {

			gridMovementComponent_ = new GridMovementComponent(this);
			turnActionCounterComponent_ = new TurnActionCounterComponent(actionsCount_);
			var moveAbilityExecutionStrategy = new MoveAbilityExecutionStrategy(gridMovementComponent_);
			var moveAbilitySelectionStrategy = new NearestCharacterTileSelectionStrategy.Builder()
				.WithEntity(this)
				.Build();
			var moveAbilityStrategy = new TargetedAbilityStrategy.Builder()
				.WithAbilitySelectionStrategy(moveAbilitySelectionStrategy)
				.WithAbilityExecutionStrategy(moveAbilityExecutionStrategy)
				.Build();
			if (ResourceSystem.Instance.TryGetAbilityData(AbilityTypes.RAT_MOVE_ABILITY, out AbilityData abilityData)) {
				moveAbilityStrategy.SetAbilityData(abilityData);
			}
			TurnManager.Instance.StartEnemyTurn += () => moveAbilityStrategy.CastAbility();

		}
		private void Move() {
		}
		private void At(IState from, IState to, IPredicate condition) => stateMachine_.AddTransition(from, to, condition);

		public bool CanPerformAction(int actionCost) => turnActionCounterComponent_.CanPerformAction(actionCost);
		public int GetRemainingActions() => turnActionCounterComponent_.RemainingActions;
	}
}
