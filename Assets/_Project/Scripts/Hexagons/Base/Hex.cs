using System;
using System.Collections.Generic;
using System.Linq;
using Game.Core;
using Game.Entities;
using Game.Managers;

using UnityEngine;

namespace Game.Hexagons {
	public abstract class Hex : MonoBehaviour {
		#region  SerializeFields
		[field: SerializeField] public bool Traversable { get; private set; }
		#endregion
		public Vector3Int TileMapPosition { get; private set; }
		public List<Hex> Neighbors { get; private set; }
		public ICoords HexCoord { get; private set; }
		public Entity OccupiedEntity { get; private set; }
		public Vector3 WorldPosition => transform.position;
		public bool Occupied() => OccupiedEntity != null;
		public void SetOccupiedEntity(Entity entity) => OccupiedEntity = entity;
		public void Initialize(Vector3Int tileMapPosition, HexCoords hexCoords) {
			TileMapPosition = tileMapPosition;
			HexCoord = hexCoords;
		}
		#region PathFinding
		public Hex Connection { get; private set; }
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
		public float GetDistance(Hex other) => HexCoord.GetDistance(other.HexCoord);
		public void SetConnection(Hex hex) => Connection = hex;
		public bool Walkable() => !Occupied() && Traversable;
		#endregion
	}
}
