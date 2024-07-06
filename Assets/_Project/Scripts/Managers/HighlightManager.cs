using System.Collections.Generic;
using Game.Abilities;
using Game.Core;
using Game.Hexagons;
using Game.Highlight;
using Game.Systems;
using UnityEngine;
namespace Game.Managers {
	public class HighlightManager : Singleton<HighlightManager> {
		private HashSet<GameObject> highlightedHexes_ = new();
		protected override void Awake() {
			base.Awake();
		}
		public void HighlightHexes(HashSet<Hex> hexes, HighlightType highlightType) {
			UnHighLightHexes();
			if (ResourceSystem.Instance.TryGetHighlightData(highlightType, out HighlightData data)) {
				foreach (var hex in hexes) {
					var highlight = Instantiate(data.GameObject, hex.WorldPosition, Quaternion.identity);
					highlightedHexes_.Add(highlight);
				}
			}
		}
		public void HighlightHexes(List<TargetData> targetData, HighlightType highlightType) {
			UnHighLightHexes();
			if (ResourceSystem.Instance.TryGetHighlightData(highlightType, out HighlightData highlightData)) {
				foreach (var data in targetData) {
					var highlight = Instantiate(highlightData.GameObject, data.Hex.WorldPosition, Quaternion.identity);
					highlightedHexes_.Add(highlight);
				}
			}

		}
		public void UnHighLightHexes() {
			foreach (var hex in highlightedHexes_) {
				Destroy(hex.gameObject);
			}
			highlightedHexes_.Clear();
		}
	}
	public enum HighlightType {
		NONE,
		OUTLINE,
		ATTACK,
	}
}
