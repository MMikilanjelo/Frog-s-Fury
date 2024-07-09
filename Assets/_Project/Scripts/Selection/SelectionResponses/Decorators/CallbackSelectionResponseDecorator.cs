using System;
using Game.Hexagons;

namespace Game.Selection {
	public class CallbackSelectionResponseDecorator : SelectionResponseDecorator {
		public Action<Hex> OnSelectCallback { get; set; } = delegate { };
		public Action<Hex> OnDeselectCallback { get; set; } = delegate { };

		public CallbackSelectionResponseDecorator() { }
		public void SetCallback(Action<Hex> onSelectCallback) {
			OnSelectCallback = onSelectCallback;
		}
		public void SetCallback(Action<Hex> onSelectCallback, Action<Hex> onDeselectCallback) {
			OnSelectCallback = onSelectCallback;
			OnDeselectCallback = onDeselectCallback;
		}
		public override void OnSelect(Hex selectedHex) {
			OnSelectCallback?.Invoke(selectedHex);
		}

		public override void OnDeselect(Hex selectedHex) {
			OnDeselectCallback?.Invoke(selectedHex);
		}
	}
}
