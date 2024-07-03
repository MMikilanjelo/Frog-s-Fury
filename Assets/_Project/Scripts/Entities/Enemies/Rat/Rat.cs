using UnityEngine;
using Game.Components;
using Game.Abilities;
using UnityEngine.Assertions.Must;
using System.Collections.Generic;
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
			var moveAbTg = new PathDistanceHexFinder.Builder().WithFlags(Utils.Helpers.HexNodeFlags.OCCUPIED_BY_CHARACTER).Build(); 
			var moveAb = new TargetedAbilityStrategy.Builder()
				.WithAbilityExecutionStrategy(moveAbEx)
				.WithAbilitySelectionStrategy(moveAbSel)
				.WithAbilityTargetFinderStrategy(moveAbTg)
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
