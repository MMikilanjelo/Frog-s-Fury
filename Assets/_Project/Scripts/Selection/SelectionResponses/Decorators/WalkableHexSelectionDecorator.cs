using System;
namespace Game.Selection {
	public class WalkableHexSelectionDecorator : SelectionResponseDecorator {
		private Action<SelectionData> action_ = delegate { };
		public WalkableHexSelectionDecorator(ISelectionResponse wrappedResponse, Action<SelectionData> action) : base(wrappedResponse) {
			action_ = action;
		}
		public override void OnSelect(SelectionData selection) {
			if (selection.SelectedHexNode?.Walkable() ?? false) {
				action_.Invoke(selection);
			}
			base.OnSelect(selection);
		}
		public override void OnDeselect(SelectionData selection) {
			base.OnDeselect(selection);
		}
	}
}
