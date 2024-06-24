using System;
using Game.Hexagons;
namespace Game.Selection {
	public interface ISelectionResponse {
		public void OnSelect(Hex selection);
		public void OnDeselect(Hex selection);
		
	}
}