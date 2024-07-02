using System.Collections.Generic;

using Game.Abilities;
using Game.Components;

using UnityEngine;

namespace Game.Entities.Characters {
	public class Fish : Character {
		#region SerializeFields
		[SerializeField, Range(1, 4)] private int actionsCount_;

		#endregion
		#region Components
		private TurnActionCounterComponent turnActionCounterComponent_;
		private GridMovementComponent gridMovementComponent_;
		#endregion
		private void Awake() {
			gridMovementComponent_ = new GridMovementComponent(this);
			turnActionCounterComponent_ = new TurnActionCounterComponent(actionsCount_);
			var moveAbilityStrategy = new MoveAbilityExecutionStrategy(gridMovementComponent_);
			var moveAbilitySelectionStrategy = new WalkableHexMouseSelectionStrategy();
			var moveAbilityTargetFinderStrategy = new WalkableHexFinderStrategy();
			
			var moveAbility = new TargetedAbilityStrategy.Builder()
				.WithAbilityExecutionStrategy(moveAbilityStrategy)
				.WithAbilitySelectionStrategy(moveAbilitySelectionStrategy)
				.WithAbilityTargetFinderStrategy(moveAbilityTargetFinderStrategy)
				.WithEntity(this)
				.WithTurnActionCounterComponent(turnActionCounterComponent_)
				.Build();
			
			Abilities = new Dictionary<AbilityTypes, IAbilityStrategy>{
				{AbilityTypes.FISH_MOVE_ABILITY , moveAbility},
			};
		}
		public override int GetRemainingActions() => turnActionCounterComponent_.RemainingActions;
		public override bool CanPerformAction(int actionCost) => turnActionCounterComponent_.CanPerformAction(actionCost);
	}
}

