using UnityEngine;
using UnityEngine.Tilemaps;
namespace Game.Hexagons {
	public abstract class HexTile : TileBase {
		#region  SerializeField
		[SerializeField] protected bool traversable_;
		[SerializeField] protected Sprite sprite_;
		[SerializeField] protected Color color_;
		#endregion
		public bool Traversable => traversable_;
		public Sprite Sprite => sprite_;
		public Color Color => color_;
		public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData) {
			base.GetTileData(position, tilemap, ref tileData);
			tileData.color = color_;
			tileData.sprite = sprite_;
			tileData.flags = TileFlags.LockColor;
		}
		public void SetColor(Color color){
			color_ = color;
		}
	}
}
