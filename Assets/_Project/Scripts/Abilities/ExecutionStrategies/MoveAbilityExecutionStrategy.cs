using System.Collections.Generic;
using System.Linq;
using Game.Components;
using Game.Hexagons;
namespace Game.Abilities {
	public class MoveAbilityExecutionStrategy : AbilityExecutionStrategy {
		private GridMovementComponent gridMovementComponent_;
		public MoveAbilityExecutionStrategy(GridMovementComponent gridMovementComponent) {
			gridMovementComponent_ = gridMovementComponent;
		}
		public override void CastAbility(HashSet<Hex> selectedHexes) {
			var path = gridMovementComponent_.FindPath(selectedHexes.First());
			gridMovementComponent_.Move(path);
			OnAbilityExecuted();
		}
	}
}

