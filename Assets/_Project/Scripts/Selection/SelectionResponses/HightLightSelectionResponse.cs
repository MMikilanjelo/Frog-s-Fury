using Game.Hexagons;
using Game.Managers;
namespace Game.Selection {
	public class HightLightSelectionResponse : ISelectionResponse {
		public void OnDeselect(Hex selectedHex) {
		}
		public void OnSelect(Hex selectedHex) {
			SelectionManager.Instance.OnSelected(selectedHex);
		}
	}
}