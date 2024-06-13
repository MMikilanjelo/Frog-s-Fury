using System.Collections.Generic;
using System.Linq;
using Game.Entities;
using Game.Managers;
using UnityEngine;

namespace Game.Hexagons {
	public class HexNode {
		public Vector3Int TileMapPosition { get; private set; }
		public Vector3 WorldPosition {get;private set;}
		public List<HexNode> Neighbors { get; private set; }
		public ICoords HexCoord { get; private set; }
		public Entity OccupiedEntity { get; private set; }
		public bool Traversable { get; private set; }
		public bool Occupied() => OccupiedEntity != null;

		public void SetOccupiedEntity(Entity entity) => OccupiedEntity = entity;
		public HexNode(Vector3Int tileMapPosition,Vector3 worldPosition , HexCoords hexCoords, bool traversable) {
			TileMapPosition = tileMapPosition;
			HexCoord = hexCoords;
			Traversable = traversable;
			WorldPosition = worldPosition;
		}
		#region PathFinding
		public HexNode Connection { get; private set; }
		public float G { get; private set; }
		public float H { get; private set; }
		public float F => G + H;
		public float SetG(float g) => G = g;
		public float SetH(float h) => H = h;
		public void CacheNeighbors() {
			Neighbors = GridManager.Instance.HexesInGrid
			.Where(h => HexCoord.GetDistance(h.Value.HexCoord) == 1)
			.Select(h => h.Value).ToList();
		}
		public float GetDistance(HexNode other) => HexCoord.GetDistance(other.HexCoord);
		public void SetConnection(HexNode hexNode) => Connection = hexNode;
		public bool Walkable() => !Occupied() && Traversable;
		#endregion
	}
}

