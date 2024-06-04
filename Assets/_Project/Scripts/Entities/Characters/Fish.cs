using UnityEngine;
using System.Collections.Generic;
using Game.Commands;
using Game.Systems.AbilitySystem;
using Game.Components;
namespace Game.Entities.Characters {
	public class Fish : Character{
		private GridMovementComponent gridMovementComponent_;
		private void Awake(){
			gridMovementComponent_ = new GridMovementComponent (this);
			
			var command_ = new MoveCommand.Builder()
			.WithAction(() => Debug.Log("cool"))
			.Build();

			CharacterAbilities = new Dictionary<AbilityTypes, ICommand>{
				{AbilityTypes.FISH_MOVE_ABILITY , command_},
			};
		}
	}
}
