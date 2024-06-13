
using System;
using UnityEngine;

namespace Game.Commands {
	public class PreconditionCommandDecorator : ICommand {
		private readonly ICommand command_;
		private readonly Func<bool> predicate_;
		public PreconditionCommandDecorator(ICommand command, Func<bool> predicate){
			command_ = command;
			predicate_ = predicate;
		}
		public void Execute() {
			if(predicate_.Invoke()){
				command_.Execute();
			}
		}
	}
}
