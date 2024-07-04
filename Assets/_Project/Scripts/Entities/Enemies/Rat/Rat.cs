using UnityEngine;
using Game.Components;
using Game.Abilities;
using System.Collections.Generic;
using Game.Entities.Characters;
namespace Game.Entities.Enemies {
	public class Rat : Enemy {
		#region SerializeFields
		[SerializeField, Range(1, 4)] private int actionsCount_;

		#endregion
		private TurnActionCounterComponent turnActionCounterComponent_;
		private GridMovementComponent gridMovementComponent_;
		private void Awake() {
			gridMovementComponent_ = new GridMovementComponent(this);
			turnActionCounterComponent_ = new TurnActionCounterComponent(actionsCount_);
			var moveAbEx = new MoveAbilityExecutionStrategy(gridMovementComponent_);
			var moveAbSel = new RandomCharacterHexSelectionStrategy();
			var moveAbTg = new NearestWalkableHexAroundCharacterFinder.Builder()
				.WithCharacterPriorityEvaluateFunction((CharacterTypes type) => {
					return type == CharacterTypes.FISH;
				})
				.WithSearchRange(4)
				.Build();
			var moveAb = new SingleTargetAbilityStrategy.Builder()
				.WithAbilityExecutionStrategy(moveAbEx)
				.WithAbilityTargetFinderStrategy(moveAbTg)
				.WithAbilityCost(1)
				.WithEntity(this)
				.Build();
			Abilities = new Dictionary<AbilityTypes, IAbilityStrategy>{
				{AbilityTypes.RAT_MOVE_ABILITY, moveAb},
			};
		}
		public override int GetRemainingActions() => turnActionCounterComponent_.RemainingActions;
		public override bool CanPerformAbility(int actionCost) => turnActionCounterComponent_.CanPerformAction(actionCost);
		public override void PerformAbility(int abilityCost) => turnActionCounterComponent_.PerformAction(abilityCost);
	}
}
