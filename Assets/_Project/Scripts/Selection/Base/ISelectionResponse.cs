using System;

namespace Game.Selection {
	public interface ISelectionResponse {
		public void OnSelect(SelectionData selection);
		public void OnDeselect(SelectionData selection);
		
	}
}