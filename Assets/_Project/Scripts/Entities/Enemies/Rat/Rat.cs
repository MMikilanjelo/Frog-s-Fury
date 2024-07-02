using UnityEngine;
using Game.Components;
using Game.Core.Logic;
using Game.Abilities;
using Game.Systems;
using Game.Managers;
using System.Collections.Generic;
namespace Game.Entities.Enemies {
	public class Rat : Enemy, IActionPerformer {
		#region SerializeFields
		[SerializeField, Range(1, 4)] private int actionsCount_;

		#endregion
		private TurnActionCounterComponent turnActionCounterComponent_;
		private GridMovementComponent gridMovementComponent_;
		private void Awake() {

			// Abilities = new Dictionary<AbilityTypes, IAbilityStrategy>{
			// 	{AbilityTypes.RAT_MOVE_ABILITY, null},
			// };
		}
		public bool CanPerformAction(int actionCost) => turnActionCounterComponent_.CanPerformAction(actionCost);
		public int GetRemainingActions() => turnActionCounterComponent_.RemainingActions;
	}
}
