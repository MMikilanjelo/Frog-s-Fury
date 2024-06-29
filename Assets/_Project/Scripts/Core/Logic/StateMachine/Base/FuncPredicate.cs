using System;

namespace Game.Core.Logic {
	public class FuncPredicate : IPredicate {
		readonly Func<bool> func_;

		public FuncPredicate(Func<bool> func_) {
			this.func_ = func_;
		}

		public bool Evaluate() => func_.Invoke();
	}
}
