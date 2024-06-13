
using UnityEngine;

namespace Game.Selection {
	public class MouseScreenRayProvider : IRayProvider {
		public Ray CreateRay() {
			Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			return new Ray(clickPosition, clickPosition);
		}
	}
}