using Game.Hexagons;
using UnityEngine;

namespace Game.Selection {
	public interface ISelector {
		public void Check(Ray ray);
		public SelectionData GetSelectionData();

	}
}