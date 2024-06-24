using System;
using Game.Hexagons;
namespace Game.Selection {
	public class WalkableHexSelectionResponseDecorator : SelectionResponseDecorator {
		private Action<Hex> action_ = delegate { };
		public WalkableHexSelectionResponseDecorator(Action<Hex> action){
			action_ = action;
		}	
		public override void OnSelect(Hex selectedHex) {
			if (selectedHex?.Walkable() ?? false) {
				action_.Invoke(selectedHex);
			}
			base.OnSelect(selectedHex);
		}
		public override void OnDeselect(Hex selectedHex) {
			base.OnDeselect(selectedHex);
		}
	}
}
