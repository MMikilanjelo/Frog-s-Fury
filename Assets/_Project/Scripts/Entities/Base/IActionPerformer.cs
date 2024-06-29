namespace Game.Entities {
	public interface IActionPerformer {
		public bool CanPerformAction(int actionCost);
		public int GetRemainingActions();
	}
}
