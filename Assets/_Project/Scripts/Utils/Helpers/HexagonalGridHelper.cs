using System.Collections.Generic;
using System.Linq;
using Game.Core.Logic;
using Game.Entities;
using Game.Hexagons;
using Game.Managers;
using UnityEngine;

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
		public static Hex FindNearestOccupiedHexes(Hex from, IEnumerable<Entity> entities) {
			Dictionary<int, List<Hex>> distances = new();
			foreach (var entity in entities) {
				List<Hex> path = PathFinding.FindPath(from, GetFirstWalkableHex(entity.OccupiedHex.Neighbors));
				distances.Add(path.Count, path);
			}
			int smallestDistance = distances.Keys.Min();
			var smallestPath = distances[smallestDistance];
			return smallestPath.Last();
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
