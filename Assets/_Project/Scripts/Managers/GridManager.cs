using Game.Core;
using UnityEngine;
using UnityEngine.Tilemaps;
using Game.Hexagons;
using System.Collections.Generic;
namespace Game.Managers {
	public class GridManager : Singleton<GridManager> {

		[SerializeField] private Tilemap worldTileMap_;
		public IReadOnlyDictionary<Vector3Int, HexNode> HexesInGrid { get; private set; }
		protected override void Awake() {
			base.Awake();
			RetrieveAllTilesInfo();
		}

		public HexTile GetHexTile(Vector3Int position) => worldTileMap_.GetTile<HexTile>(position);
		public HexNode GetHexNode(Vector3Int position) => HexesInGrid.TryGetValue(position, out HexNode hexNode) ? hexNode : null;
		public HexNode GetHexNodeFromWorldPosition(Vector3 worldPosition) {
			Vector3Int cellPosition = worldTileMap_.WorldToCell(worldPosition);
			return GetHexNode(cellPosition);
		}
		public HexTile GetHexTileFromWorldPosition(Vector3 worldPosition) {
			Vector3Int cellPosition = worldTileMap_.WorldToCell(worldPosition);
			return GetHexTile(cellPosition);
		}

		private void RetrieveAllTilesInfo() {
			BoundsInt bounds = worldTileMap_.cellBounds;
			Dictionary<Vector3Int, HexNode> hexes = new();
			for (int x = bounds.xMin; x < bounds.xMax; x++) {
				for (int y = bounds.yMin; y < bounds.yMax; y++) {
					Vector3Int cellPosition = new Vector3Int(x, y, 0);
					Vector3 worldPosition = worldTileMap_.CellToWorld(cellPosition);
					HexTile tile = GetHexTile(cellPosition);
					if (tile != null) {
						int q = x - (y - (y & 1)) / 2;
						int r = y;
						var hexNode = new HexNode(cellPosition, worldPosition, new HexCoords(q, r), tile.Traversable);
						hexes.Add(cellPosition, hexNode);
					}
				}
			}
			HexesInGrid = hexes;
			foreach (var hex in hexes.Values){
				hex.CacheNeighbors();
			}
		}
	}
}
