using UnityEngine;

namespace Game.Components {
	public class TurnActionCounterComponent {
		private int maxActions_;
		private int remainingActions_;

		public bool HasActionsRemain => remainingActions_ > 0;
		public int MaxActions => maxActions_;
		public int RemainingActions => remainingActions_;

		public TurnActionCounterComponent(int maxActions) {
			maxActions_ = maxActions;
			remainingActions_ = maxActions_;
		}
		public void ResetActions() => remainingActions_ = maxActions_;
		public bool CanPerformAction(int actionCost) => remainingActions_ >= actionCost;

		public void PerformAction(int actionCost) {
			remainingActions_ -= actionCost;
			remainingActions_ = Mathf.Clamp(remainingActions_, 0, maxActions_);
		}
	}
}
