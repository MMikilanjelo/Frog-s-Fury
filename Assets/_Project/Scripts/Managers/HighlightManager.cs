using System.Collections.Generic;
using UnityEngine;
using Game.Highlight;
using Game.Systems;
using Game.Core;
using Game.Abilities;

namespace Game.Managers {
	public class HighlightManager : Singleton<HighlightManager> {
		private HashSet<GameObject> highlightedHexes_ = new HashSet<GameObject>();

		protected override void Awake() {
			base.Awake();
		}

		public void HighlightHexes<T>(List<T> targetData, HighlightType highlightType) where T : TargetData {
			UnHighLightHexes();
			if (targetData == null) {
				Debug.Log("set target data to highlight");
				return;
			}
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