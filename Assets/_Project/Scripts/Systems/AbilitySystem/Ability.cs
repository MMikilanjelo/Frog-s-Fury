using System.Collections.Generic;
using Game.Commands;
using Game.Systems.AbilitySystem;
namespace Game.Systems.AbilitySystem {
	public class Ability {
		public AbilityData Data {get ; private set;}
		private ICommand command_;
		
		public Ability(AbilityData data , ICommand command){
			Data = data;
			command_ = command;
		}
		
		public ICommand GetAbilityCommand() => command_;
	}
}
