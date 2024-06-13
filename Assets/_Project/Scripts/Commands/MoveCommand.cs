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
			private bool withPrecondition_ = false;
			private Func<bool> predicate_;
			public Builder WithAction(Action action) {
				command_.action_ = action;
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

