using Game.Hexagons;
using Game.Managers;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Game.Selection {
	public class RayCastBasedTileSelector : ISelector {
		private SelectionData selectionData_;
		public void Check(Ray ray) {
			if (!EventSystem.current.IsPointerOverGameObject()) {
				HexTile clickedTile = GridManager.Instance.GetHexTileFromWorldPosition(ray.origin);
				selectionData_ = new SelectionData(GridManager.Instance.GetHexNodeFromWorldPosition(ray.origin), clickedTile);
			}
			else{
				selectionData_ = null;
			}
		}
		public SelectionData GetSelectionData() => selectionData_;
	}
}