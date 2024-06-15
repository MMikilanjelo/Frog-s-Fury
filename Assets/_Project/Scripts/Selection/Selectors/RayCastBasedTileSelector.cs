using Game.Hexagons;
using Game.Managers;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Game.Selection {
	public class RayCastBasedTileSelector : ISelector {
		private SelectionData selectionData_;
		public void Check(Ray ray) {
			
			if (!EventSystem.current.IsPointerOverGameObject()) {

				selectionData_ = new SelectionData(GridManager.Instance.GetHexFromWorldPosition(ray.origin));
			}
			else{
				selectionData_ = null;
			}
		}
		public SelectionData GetSelectionData() => selectionData_;
	}
}