using System;

namespace Game.Commands {
	public class MoveCommand : ICommand {
		private Action action_ = delegate { };
		public void Execute() {
			action_.Invoke();
		}
		private MoveCommand() { }
		public class Builder {
			private readonly MoveCommand command_ = new MoveCommand();
			public Builder WithActions(Action action) {
				command_.action_ = action;
				return this;
			}
			public MoveCommand Build () => command_;

			//TODO implement wrapper that will enable selection for this command;
		}
	}
}

