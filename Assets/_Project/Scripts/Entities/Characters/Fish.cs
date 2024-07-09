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
			var attackAbilityExecutionStrategy = new DealDamageAbilityExecutionStrategy.Builder()
				.WithDamage(6)
				.Build();

			var attackAbilitySelectionStrategy = new HexMouseSelectionStrategy.Builder()
				.WithCallbackSelectionResponseDecorator()
				.WithHighLightType(HighlightType.ATTACK)
				.Build();
			var attackAbilityTargetFinderStrategy = new EntityInRangeFinder.Builder()
				.WithSearchRange(5)
				.Build();
			var attackAbility = new TargetSelectionAbilityStrategy.Builder()
				.WithAbilityExecutionStrategy(attackAbilityExecutionStrategy)
				.WithAbilityTargetFinderStrategy(attackAbilityTargetFinderStrategy)
				.WithAbilityTargetSelectionStrategy(attackAbilitySelectionStrategy)
				.WithEntity(this)
				.WithAbilityCost(1)
				.Build();
			Abilities = new Dictionary<AbilityTypes, AbilityStrategy>{
				{AbilityTypes.FISH_MOVE_ABILITY , moveAbility},
				{AbilityTypes.FISH_ATTACK_ABILITY , attackAbility},
			};
			TurnManager.Instance.StartPlayerTurn += () => {
				foreach (var ability in Abilities.Values) {
					ability.EnableAbility();
				}
			};
		}
		public override int GetRemainingActions() => turnActionCounterComponent_.RemainingActions;
		public override bool CanPerformAbility(int actionCost) => turnActionCounterComponent_.CanPerformAction(actionCost);
		public override void PerformAbility(int abilityCost) => turnActionCounterComponent_.PerformAction(abilityCost);
	}
}

