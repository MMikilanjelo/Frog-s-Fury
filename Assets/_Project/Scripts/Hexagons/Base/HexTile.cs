using UnityEngine;
using UnityEngine.Tilemaps;
namespace Game.Hexagons {
	public abstract class HexTile : TileBase {
		#region  SerializeField
		[SerializeField] private Sprite sprite_;
		[SerializeField] private Color color_;
		#endregion
		public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData) {
			base.GetTileData(position, tilemap, ref tileData);
			tileData.color = color_;
			tileData.sprite = sprite_;
			tileData.flags = TileFlags.LockColor;
		}
	}
}
