using Game.Commands;
namespace Game.AbilitySystem {
	public class Ability {
		
		public AbilityData Data {get ; private set;}
		private ICommand command_;
		public Ability(AbilityData data , ICommand command){
			Data = data;
			command_ = command;
		}
		public ICommand GetCommand() => command_;
	}
}
