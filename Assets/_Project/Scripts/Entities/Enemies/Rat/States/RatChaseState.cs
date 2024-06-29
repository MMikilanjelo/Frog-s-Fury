using System.Collections.Generic;
using Game.Components;
using Game.Managers;
using Game.Hexagons;
using System;
namespace Game.Entities.Enemies {

	public class RatChaseState : RatBaseState {
		private GridMovementComponent gridMovementComponent_;
		private RatChaseState(GridMovementComponent gridMovementComponent) : base() {
			gridMovementComponent_ = gridMovementComponent;
		}
		public override void OnEnter() {
			if(gridMovementComponent_.TryFindPathToDestination(
				UnitManager.Instance.Characters[0].OccupiedHex.Neighbors[2] , out List<Hex> path , 30)){
				gridMovementComponent_.Move(path);
			}
		}
		public class Builder {
			private GridMovementComponent gridMovementComponent_;
			public Builder WithGridMovementComponent(GridMovementComponent gridMovementComponent) {
				gridMovementComponent_ = gridMovementComponent;
				return this;
			}
			public Builder WithActionToPerform(Action action){
				return this;
			}
			public RatChaseState Build() => new RatChaseState(gridMovementComponent_);
		}
	}
}
