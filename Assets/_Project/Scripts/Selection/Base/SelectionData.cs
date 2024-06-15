
using Game.Hexagons;

namespace Game.Selection {
	public class SelectionData {
		public Hex SelectedHex { get; private set; }
		public SelectionData(Hex selectedHex){
			SelectedHex = selectedHex;
		}
	}
}
