using Game.Hexagons;
namespace Game.Selection {
	public abstract class SelectionResponseDecorator : ISelectionResponse {
		public ISelectionResponse WrappedResponse { get; protected set; }
		public SelectionResponseDecorator() { }
		public void Decorate(ISelectionResponse selectionResponse) => WrappedResponse = selectionResponse;

		public virtual void OnSelect(Hex selectedHex) {
			WrappedResponse?.OnSelect(selectedHex);
		}
		public virtual void OnDeselect(Hex selectedHex) {
			WrappedResponse?.OnDeselect(selectedHex);
		}
	}

}

