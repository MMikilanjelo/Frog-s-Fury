
using Game.Hexagons;

namespace Game.Selection {
	public class SelectionData {
		public HexNode SelectedHexNode { get; private set; }
		public HexTile SelectedHexTile { get; private set; }
		public SelectionData(HexNode selectedHexNode , HexTile selectedHexTile){
			SelectedHexNode = selectedHexNode;
			SelectedHexTile = selectedHexTile;
		}
	}
}
