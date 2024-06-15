using System.Collections.Generic;
using Game.Hexagons;
using Game.Managers;

namespace Game.Utils.Helpers {
	public static class HexagonalGridHelper {
		public static HashSet<Hex> FindHexNodesWithinDistance(Hex origin, int distance, HexNodeFlags flags = HexNodeFlags.None) {
			var hexNodes = new HashSet<Hex>();
			foreach (var hexNode in GridManager.Instance.HexesInGrid.Values) {
				if (origin.GetDistance(hexNode) <= distance && HexNodeChecker.HasFlags(hexNode, flags)) {
					hexNodes.Add(hexNode);
				}
			}
			return hexNodes;
		}
		public static HashSet<Hex> FindHexNodesInGrid(HexNodeFlags flags){
			var hexNodes = new HashSet<Hex>();
			foreach(var hexNode in GridManager.Instance.HexesInGrid.Values){
				if(HexNodeChecker.HasFlags( hexNode, flags)){
					hexNodes.Add(hexNode);
				}
			}
			return hexNodes;
		}
	}
}
