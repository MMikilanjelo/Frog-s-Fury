using System;
using Game.Hexagons;
namespace Game.Commands {
	public class MoveCommand : ICommand {
		private Action<Hex> action_ = delegate { };
		private Hex destination_;
		public void Execute() {
			if(destination_ != null){
				action_.Invoke(destination_);
			}
		}
		private MoveCommand() { }
		public class Builder {
			private readonly MoveCommand command_ = new MoveCommand();
			private bool withPrecondition_ = false;
			private Func<bool> predicate_;
			public Builder WithAction(Action<Hex> action) {
				command_.action_ = action;
				return this;
			}
			public Builder WithDestination(Hex destination){
				command_.destination_ = destination;
				return this;
			}
			public Builder WithPrecondition(Func<bool> predicate) {
				withPrecondition_ = true;
				predicate_ = predicate;
				return this;
			}
			public ICommand Build() => withPrecondition_ ? new PreconditionCommandDecorator(command_, predicate_) : command_;
		}
	}
}

