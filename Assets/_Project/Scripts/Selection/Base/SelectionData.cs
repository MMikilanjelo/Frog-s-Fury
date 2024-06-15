
using Game.Hexagons;

namespace Game.Selection {
	public class SelectionData {
		public Hex SelectedHex { get; private set; }
		public HexTile SelectedHexTile { get; private set; }
		public SelectionData(Hex selectedHex , HexTile selectedHexTile){
			SelectedHex = selectedHex;
			SelectedHexTile = selectedHexTile;
		}
	}
}
