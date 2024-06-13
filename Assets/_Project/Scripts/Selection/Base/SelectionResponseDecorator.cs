using System;

namespace Game.Selection {
	public abstract class SelectionResponseDecorator : ISelectionResponse {
		public ISelectionResponse WrappedResponse { get; protected set; }

		public SelectionResponseDecorator(ISelectionResponse wrappedResponse) {
			WrappedResponse = wrappedResponse;
		}
		public virtual void OnSelect(SelectionData selection) => WrappedResponse.OnSelect(selection);
		public virtual void OnDeselect(SelectionData selection) => WrappedResponse.OnDeselect(selection);
	}
}
