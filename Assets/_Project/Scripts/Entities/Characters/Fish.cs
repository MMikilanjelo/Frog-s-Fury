using System.Collections.Generic;
using Game.Abilities;
using Game.Components;
using Game.Managers;
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

			var moveAbilitySelectionStrategy = new HexMouseSelectionStrategy.Builder()
				.WithCallbackSelectionResponseDecorator()
				.WithHighLightType(HighlightType.OUTLINE)
				.Build();

			var moveAbilityTargetFinderStrategy = new PathDistanceWalkableHexFinder.Builder()
				.WithSearchRange(5)
				.Build();

			var moveAbility = new TargetSelectionAbilityStrategy.Builder()
				.WithAbilityExecutionStrategy(moveAbilityStrategy)
				.WithAbilityTargetFinderStrategy(moveAbilityTargetFinderStrategy)
				.WithAbilityTargetSelectionStrategy(moveAbilitySelectionStrategy)
				.WithEntity(this)
				.WithAbilityCost(1)
				.Build();

			Abilities = new Dictionary<AbilityTypes, IAbilityStrategy>{
				{AbilityTypes.FISH_MOVE_ABILITY , moveAbility},
			};
		}
		public override int GetRemainingActions() => turnActionCounterComponent_.RemainingActions;
		public override bool CanPerformAbility(int actionCost) => turnActionCounterComponent_.CanPerformAction(actionCost);
		public override void PerformAbility(int abilityCost) => turnActionCounterComponent_.PerformAction(abilityCost);
	}
}

