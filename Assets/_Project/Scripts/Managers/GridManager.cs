using Game.Core;
using UnityEngine;
using UnityEngine.Tilemaps;
using Game.Hexagons;
using System.Collections.Generic;
namespace Game.Managers {
	public class GridManager : Singleton<GridManager> {

		[SerializeField] private Tilemap worldTileMap_;
		public IReadOnlyDictionary<Vector3Int, HexNode> HexesInGrid { get; private set; }
		
		protected override void Awake() => RetrieveAllTilesInfo();
		
		public HexTile GetHexTile(Vector3Int position) => worldTileMap_.GetTile<HexTile>(position);
		
		public HexNode GetHexNode(Vector3Int position) {
			if (HexesInGrid.TryGetValue(position, out HexNode hexNode)) {
				return hexNode;
			}
			return null;
		}
		
		void Update() {
			if (Input.GetMouseButtonDown(0)) {
				Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				worldPosition.z = 0;
				Vector3Int cellPosition = worldTileMap_.WorldToCell(worldPosition);
				HexTile clickedTile = GetHexTile(cellPosition);
				if (clickedTile != null) {
					var hexNode = GetHexNode(cellPosition);
					Debug.Log(hexNode.OccupiedEntity);
				}
				else {
					Debug.Log("No tile at clicked position.");
				}
			}
		}

		private void RetrieveAllTilesInfo() {
			BoundsInt bounds = worldTileMap_.cellBounds;
			Dictionary<Vector3Int, HexNode> hexes = new();
			for (int x = bounds.xMin; x < bounds.xMax; x++) {
				for (int y = bounds.yMin; y < bounds.yMax; y++) {
					Vector3Int cellPosition = new Vector3Int(x, y, 0);
					HexTile tile = GetHexTile(cellPosition);
					if (tile != null) {
						int q = x - (y - (y & 1)) / 2;
						int r = y;
						var hexNode = new HexNode(cellPosition, new HexCoords(q, r) , tile.Traversable);
						hexes.Add(cellPosition, hexNode);
					}
				}
			}
			HexesInGrid = hexes;
		}
	}
}
