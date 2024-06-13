using System.Collections.Generic;

using Game.Abilities;
using Game.Components;

using UnityEngine;

namespace Game.Entities.Characters {

	public class Fish : Character {

		#region SerializeFields
		[SerializeField, Range(1, 4)] private int actionsCount_;
		[SerializeField, Range(1, 10)] private int moveDistance_;

		#endregion

		#region Components
		private TurnActionCounterComponent turnActionCounterComponent_;
		private GridMovementComponent gridMovementComponent_;
		#endregion

		private void Awake() {
			gridMovementComponent_ = new GridMovementComponent(this , moveDistance_);
			turnActionCounterComponent_ = new TurnActionCounterComponent(actionsCount_);

			var moveAbilityStrategy = new MoveAbilityStrategy.Builder()
				.WithGridMovementComponent(gridMovementComponent_)
				.WithTurnActionCounterComponent(turnActionCounterComponent_)
				.Build();
			CharacterAbilities = new Dictionary<AbilityTypes, AbilityStrategy>{
				{AbilityTypes.FISH_MOVE_ABILITY , moveAbilityStrategy},
			};
		}
		public override int GetRemainingActions() => turnActionCounterComponent_.RemainingActions;
		public override bool CanPerformAction(int actionCost) => turnActionCounterComponent_.CanPerformAction(actionCost);
	}
}

