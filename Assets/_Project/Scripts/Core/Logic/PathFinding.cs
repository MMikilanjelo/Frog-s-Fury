using Game.Hexagons;
using System.Collections.Generic;
using System.Linq;
using System;
namespace Game.Core.Logic {
	public static class PathFinding {
		public static List<HexNode> FindPath(HexNode startNode, HexNode targetNode) {
			var toSearch = new List<HexNode>() { startNode };
			var processed = new List<HexNode>();
			while (toSearch.Any()) {
				var current = toSearch[0];
				foreach (var h in toSearch)
					if (h.F < current.F || h.F == current.F && h.H < current.H) current = h;

				processed.Add(current);
				toSearch.Remove(current);


				if (current == targetNode) {
					var currentPathTile = targetNode;
					var path = new List<HexNode>();
					var count = 100;
					while (currentPathTile != startNode) {
						path.Add(currentPathTile);
						currentPathTile = currentPathTile.Connection;
						count--;
						if (count < 0) throw new Exception();
					}

					path.Reverse();
					return path;
				}

				foreach (var neighbor in current.Neighbors.Where(h => h.Walkable() && !processed.Contains(h))) {
					var inSearch = toSearch.Contains(neighbor);

					var costToNeighbor = current.G + current.GetDistance(neighbor);

					if (!inSearch || costToNeighbor < neighbor.G) {
						neighbor.SetG(costToNeighbor);
						neighbor.SetConnection(current);

						if (!inSearch) {
							neighbor.SetH(neighbor.GetDistance(targetNode));
							toSearch.Add(neighbor);
						}
					}
				}
			}
			return null;
		}
	}
}
