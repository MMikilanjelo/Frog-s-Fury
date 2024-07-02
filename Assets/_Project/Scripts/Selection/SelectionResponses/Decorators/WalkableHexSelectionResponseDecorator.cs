using System;
using Game.Hexagons;
namespace Game.Selection {
	public class WalkableHexSelectionResponseDecorator : SelectionResponseDecorator {
		private Action<Hex> action_ = delegate { };
		private Func<Hex, bool> func_ = hex => true;
		private WalkableHexSelectionResponseDecorator() { }
		public override void OnSelect(Hex selectedHex) {
			if (func_(selectedHex) && (selectedHex?.Walkable() ?? false)) {
				action_.Invoke(selectedHex);
			}
			base.OnSelect(selectedHex);
		}
		public override void OnDeselect(Hex selectedHex) {
			base.OnDeselect(selectedHex);
		}
		public class Builder {
			private WalkableHexSelectionResponseDecorator walkableHexSelectionResponseDecorator_ = new();
			public Builder WithCallback(Action<Hex> callback) {
				walkableHexSelectionResponseDecorator_.action_ = callback;
				return this;
			}
			public Builder WithValidator(Func<Hex, bool> func) {
				walkableHexSelectionResponseDecorator_.func_ = func;
				return this;
			}
			public WalkableHexSelectionResponseDecorator Build() {
				return walkableHexSelectionResponseDecorator_;
			}
		}
	}
}
