namespace Game.Core.Logic {
	public interface ITransition {
		IState To { get; }
		IPredicate Condition { get; }
	}
}