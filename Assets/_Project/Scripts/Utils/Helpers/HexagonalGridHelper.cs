using System.Collections.Generic;

using Game.Core.Logic;
using Game.Hexagons;
using Game.Managers;

namespace Game.Utils.Helpers {
	public static class HexagonalGridHelper {
		public static Hex GetFirstWalkableHex(IEnumerable<Hex> hexes) {
			foreach (var hex in hexes) {
				if (hex.Walkable()) {
					return hex;
				}
			}
			return null;
		}
		public static HashSet<Hex> FindHexesWithinDistance(Hex origin, int distance, HexNodeFlags flags = HexNodeFlags.None) {
			var hexes = new HashSet<Hex>();
			foreach (var hexNode in GridManager.Instance.HexesInGrid.Values) {
				if (origin.GetDistance(hexNode) <= distance && HexNodeChecker.HasFlags(hexNode, flags)) {
					hexes.Add(hexNode);
				}
			}
			return hexes;
		}
		public static HashSet<Hex> FindHexesWithinPathDistance(Hex origin, int distance, HexNodeFlags flags = HexNodeFlags.None) {
			var hexes = new HashSet<Hex>();
			foreach (var hex in GridManager.Instance.HexesInGrid.Values) {
				if (origin.GetDistance(hex) <= distance && HexNodeChecker.HasFlags(hex, flags)) {
					if (PathFinding.FindPath(origin, hex)?.Count <= distance) {
						hexes.Add(hex);
					}
				}
			}
			return hexes;
		}
		public static HashSet<Hex> FindHexNodesInGrid(HexNodeFlags flags) {
			var hexes = new HashSet<Hex>();
			foreach (var hexNode in GridManager.Instance.HexesInGrid.Values) {
				if (HexNodeChecker.HasFlags(hexNode, flags)) {
					hexes.Add(hexNode);
				}
			}
			return hexes;
		}
	}
}
