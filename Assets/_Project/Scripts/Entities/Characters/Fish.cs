using UnityEngine;
using Game.Commands;
using Game.Components;
namespace Game.Entities.Characters {
	public class Fish : Character{
		private ICommand command_;
		private GridMovementComponent gridMovementComponent_;
		private void Awake(){
			gridMovementComponent_ = new GridMovementComponent(this);

			command_ = new MoveCommand.Builder()
			.WithActions(() => Debug.Log("Cool cool cool cool cool"))
			.Build();
		}
		private void Update(){
		}	
	}
}
