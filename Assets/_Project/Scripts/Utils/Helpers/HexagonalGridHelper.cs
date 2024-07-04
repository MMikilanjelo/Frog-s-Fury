using System.Collections.Generic;
using System.Linq;
using Game.Core.Logic;
using Game.Entities.Characters;
using Game.Hexagons;
using Game.Managers;

namespace Game.Utils.Helpers {
	public static class HexagonalGridHelper {
		public static HashSet<Hex> FindHexesWithinAxialDistance(Hex origin, int distance, HexNodeFlags flags = HexNodeFlags.None) {
			var hexes = new HashSet<Hex>();
			foreach (var hexNode in GridManager.Instance.HexesInGrid.Values) {
				if (origin.GetDistance(hexNode) <= distance && HexNodeChecker.HasFlags(hexNode, flags)) {
					hexes.Add(hexNode);
				}
			}
			return hexes;
		}
		public static HashSet<Hex> FindWalkableHexesWithinPathDistance(Hex origin, int distance) {
			var hexes = new HashSet<Hex>();
			foreach (var hex in GridManager.Instance.HexesInGrid.Values) {
				if (HexNodeChecker.HasFlags(hex, HexNodeFlags.WALKABLE)) {
					var path = PathFinding.FindPath(origin, hex);
					if (path.Count <= distance) {
						hexes.Add(hex);
					}
				}
			}
			return hexes;
		}
		public static List<(Character character, Hex nearestWalkableHex)> FindClosestWalkableHexesNearCharactersWithinDistance(Hex origin, int searchDistance) {
			var closestHexes = new List<(Character character, Hex nearestWalkableHex)>();
			var characterHexes = FindHexNodesInGrid(HexNodeFlags.OCCUPIED_BY_CHARACTER);

			foreach (var characterHex in characterHexes) {
				var nearestWalkableHex = FindNearestWalkableHex(characterHex, origin, searchDistance);
				if (nearestWalkableHex != null) {
					closestHexes.Add((characterHex.OccupiedEntity as Character, nearestWalkableHex));
				}
			}

			return closestHexes;
		}
		private static Hex FindNearestWalkableHex(Hex hex, Hex origin, int searchDistance) {
			Hex nearestHex = null;
			int shortestPathLength = int.MaxValue;

			foreach (var neighbor in hex.Neighbors) {
				var path = PathFinding.FindPath(origin, neighbor);
				if (path.Count > 0 && path.Count <= searchDistance && path.Count < shortestPathLength) {
					shortestPathLength = path.Count;
					nearestHex = neighbor;
				}
			}

			return nearestHex;
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
