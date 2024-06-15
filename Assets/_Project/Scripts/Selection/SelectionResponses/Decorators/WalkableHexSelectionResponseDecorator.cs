using System;
namespace Game.Selection {
	public class WalkableHexSelectionResponseDecorator : SelectionResponseDecorator {
		private Action<SelectionData> action_ = delegate { };
		public WalkableHexSelectionResponseDecorator(Action<SelectionData> action){
			action_ = action;
		}	
		public override void OnSelect(SelectionData selection) {
			if (selection.SelectedHex?.Walkable() ?? false) {
				action_.Invoke(selection);
			}
			base.OnSelect(selection);
		}
		public override void OnDeselect(SelectionData selection) {
			base.OnDeselect(selection);
		}
	}
}
