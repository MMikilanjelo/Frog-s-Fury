using Game.Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using Game.Hexagons;
namespace Game.Selection {
	public class RayCastBasedTileSelector : ISelector {
		private Hex selectedHex_;
		public void Check(Ray ray) {
			if (!EventSystem.current.IsPointerOverGameObject()) {
				selectedHex_ = GridManager.Instance.GetHexFromWorldPosition(ray.origin);
			}
			else {
				selectedHex_ = null;
			}
		}
		public Hex GetSelectedHex() => selectedHex_;
	}
}

