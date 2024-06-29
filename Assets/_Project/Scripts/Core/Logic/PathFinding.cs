using Game.Hexagons;
using System.Collections.Generic;
using System.Linq;
using System;
namespace Game.Core.Logic {
	public static class PathFinding {
		public static List<Hex> FindPath(Hex start, Hex target) {
			var toSearch = new List<Hex>() { start };
			var processed = new List<Hex>();
			while (toSearch.Any()) {
				var current = toSearch[0];
				foreach (var h in toSearch)
					if (h.F < current.F || h.F == current.F && h.H < current.H) current = h;

				processed.Add(current);
				toSearch.Remove(current);


				if (current == target) {
					var currentPathTile = target;
					var path = new List<Hex>();
					var count = 100;
					while (currentPathTile != start) {
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
							neighbor.SetH(neighbor.GetDistance(target));
							toSearch.Add(neighbor);
						}
					}
				}
			}
			return new List<Hex> { };
		}
	}
}
