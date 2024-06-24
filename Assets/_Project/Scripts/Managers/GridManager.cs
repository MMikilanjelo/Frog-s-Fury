using Game.Core;
using UnityEngine;
using UnityEngine.Tilemaps;
using Game.Hexagons;
using System.Collections.Generic;
namespace Game.Managers {
	public class GridManager : Singleton<GridManager> {

		[SerializeField] private Tilemap worldTileMap_;
		public Dictionary<Vector3Int, Hex> HexesInGrid { get; private set; } = new();
		protected override void Awake() {
			base.Awake();
		}
		public void AddHex(Hex hex) {
			var tileMapPosition = worldTileMap_.WorldToCell(hex.transform.position);
			int q = tileMapPosition.x - (tileMapPosition.y - (tileMapPosition.y & 1)) / 2;
			int r = tileMapPosition.y;
			var hexCoords = new HexCoords(q, r);
			hex.Initialize(tileMapPosition, hexCoords);
			HexesInGrid.Add(tileMapPosition, hex);
		}
		public Hex GetHex(Vector3Int position) => HexesInGrid.TryGetValue(position, out Hex hex) ? hex : null;
		public Hex GetHexFromWorldPosition(Vector3 worldPosition) {
			Vector3Int cellPosition = worldTileMap_.WorldToCell(worldPosition);
			return GetHex(cellPosition);
		}
	}
}
