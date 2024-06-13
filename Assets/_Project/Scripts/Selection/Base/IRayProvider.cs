using UnityEngine;

namespace Game.Selection {
	public interface IRayProvider {
		public Ray CreateRay();
	}
}