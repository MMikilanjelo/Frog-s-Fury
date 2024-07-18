using System.Collections.Generic;
using Game.Hexagons;
using Game.Managers;
using Game.Selection;
using UnityEngine;

namespace Game.Abilities {
	public class HexMouseSelectionStrategy<T> : AbilityTargetSelectionStrategy<T> where T : class, ITargetData {
		private HighlightType highlightType_ = HighlightType.NONE;
		private CallbackSelectionResponseDecorator callbackSelectionResponseDecorator_ = new CallbackSelectionResponseDecorator();

		private HexMouseSelectionStrategy() { }

		public override void SelectTarget(List<T> targetsData) {
			callbackSelectionResponseDecorator_.SetCallback((Hex selectedHex) => {
				foreach (var target in targetsData) {
					if (target.Hex == selectedHex) {
						OnTargetSelected(target);
					}
				}
			});
			HighlightManager.Instance.HighlightHexes(targetsData, highlightType_);
			SelectionManager.Instance.DecorateSelectionResponse(callbackSelectionResponseDecorator_);
		}

		public override void EndSelection() {
			HighlightManager.Instance.UnHighLightHexes();
			SelectionManager.Instance.ResetSelectionResponseToDefault();
		}

		public class Builder {
			private readonly HexMouseSelectionStrategy<T> hexMouseSelectionStrategy_ = new HexMouseSelectionStrategy<T>();

			public Builder WithHighLightType(HighlightType highlightType) {
				hexMouseSelectionStrategy_.highlightType_ = highlightType;
				return this;
			}

			public Builder WithCallbackSelectionResponseDecorator() {
				hexMouseSelectionStrategy_.callbackSelectionResponseDecorator_ = new CallbackSelectionResponseDecorator();
				return this;
			}

			public HexMouseSelectionStrategy<T> Build() => hexMouseSelectionStrategy_;
		}
	}
}
