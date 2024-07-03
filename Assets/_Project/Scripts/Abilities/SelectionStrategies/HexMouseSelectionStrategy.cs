using System.Collections.Generic;
using Game.Hexagons;
using Game.Managers;
using Game.Selection;
namespace Game.Abilities {
	public class HexMouseSelectionStrategy : AbilityTargetSelectionStrategy {
		private HighlightType highlightType_ = HighlightType.NONE;
		private CallbackSelectionResponseDecorator callbackSelectionResponseDecorator_;
		private HexMouseSelectionStrategy() { }
		public override void SelectTarget(HashSet<Hex> targets) {
			callbackSelectionResponseDecorator_?.SetCallback((Hex target) => {
				if (targets.Contains(target)) {
					OnTargetSelected(new HashSet<Hex> { target });
				}
			});
			HighlightManager.Instance.HighlightHexes(targets, highlightType_);
			SelectionManager.Instance.DecorateSelectionResponse(callbackSelectionResponseDecorator_);
		}
		public override void EndSelection() {
			HighlightManager.Instance.UnHighLightHexes();
			SelectionManager.Instance.ResetSelectionResponseToDefault();
		}
		public class Builder {
			private readonly HexMouseSelectionStrategy hexMouseSelectionStrategy_ = new();

			public Builder WithHighLightType(HighlightType highlightType) {
				hexMouseSelectionStrategy_.highlightType_ = highlightType;
				return this;
			}
			public Builder WithCallbackSelectionResponseDecorator() {
				hexMouseSelectionStrategy_.callbackSelectionResponseDecorator_ = new CallbackSelectionResponseDecorator();
				return this;
			}
			public HexMouseSelectionStrategy Build() => hexMouseSelectionStrategy_;
		}
	}
}
