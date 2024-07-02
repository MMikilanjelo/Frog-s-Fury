using System.Collections.Generic;
using Game.Components;
using Game.Hexagons;
using UnityEngine;
namespace Game.Abilities {
	public class MoveAbilityExecutionStrategy : AbilityExecutionStrategy {
		private GridMovementComponent gridMovementComponent_;
		public MoveAbilityExecutionStrategy(GridMovementComponent gridMovementComponent) {
			gridMovementComponent_ = gridMovementComponent;
		}
		public override void CastAbility(Hex selectedHex) {
			Debug.Log("cool execution ability");
			var path = gridMovementComponent_.FindPath(selectedHex);
			gridMovementComponent_.Move(path);
			OnAbilityExecuted();
		}
	}
}

