using Game.Components;
using Game.Hexagons;
namespace Game.Commands {
	public class MoveCommand : ICommand {
		private GridMovementComponent gridMovementComponent_;
		private Hex destination_;
		public void Execute() {
			if (destination_ != null) {
				var path = gridMovementComponent_?.FindPath(destination_);
				gridMovementComponent_?.Move(path);
			}
		}
		private MoveCommand() { }
		public class Builder {

			private readonly MoveCommand command_ = new MoveCommand();
			public Builder WithGridMovementComponent(GridMovementComponent gridMovementComponent) {
				command_.gridMovementComponent_ = gridMovementComponent;
				return this;
			}
			public Builder WithDestination(Hex destination) {
				command_.destination_ = destination;
				return this;
			}
			public ICommand Build() => command_;
		}
	}
}

