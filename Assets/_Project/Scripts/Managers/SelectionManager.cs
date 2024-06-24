using System;

using Game.Core;
using Game.Entities.Characters;
using Game.Selection;
using Game.Hexagons;

using UnityEngine;

namespace Game.Managers {
	public class SelectionManager : Singleton<SelectionManager> {
		public event Action<Character> CharacterSelected = delegate { };
		public Character SelectedCharacter { get; private set; }
		public ISelectionResponse SelectionResponse => selectionResponse_;

		private ISelectionResponse selectionResponse_;
		private IRayProvider rayProvider_;
		private ISelector selector_;
		private Hex selectionData_;
		private bool enableSelection_ = false;
		protected override void Awake() {
			base.Awake();
			rayProvider_ = new MouseScreenRayProvider();
			selectionResponse_ = new HightLightSelectionResponse();
			selector_ = new RayCastBasedTileSelector();
		}

		private void Update() {
			if (Input.GetMouseButtonDown(0) && enableSelection_) {
				if (selectionData_ != null) {
					selectionResponse_?.OnDeselect(selectionData_);
				}
				selector_.Check(rayProvider_.CreateRay());
				selectionData_ = selector_.GetSelectedHex();

				if (selectionData_ != null) {
					selectionResponse_?.OnSelect(selectionData_);
					OnSelected(selectionData_);
				}
			}
		}
		public void EnableSelection(){
			enableSelection_ = true;
		}
		public void DecorateSelectionResponse(SelectionResponseDecorator selectionResponseDecorator) {
			selectionResponseDecorator.Decorate(selectionResponse_);
			selectionResponse_ = selectionResponseDecorator;
		}
		public void UnDecorateSelectionResponse(SelectionResponseDecorator selectionResponseDecorator) {
			selectionResponse_ = selectionResponseDecorator.WrappedResponse;
		}
		public void SetSelectedCharacter(Character character) {
			SelectedCharacter = character;
			CharacterSelected?.Invoke(character);
		}
		private void OnSelected(Hex selectedHex) {
			if (selectedHex?.OccupiedEntity != null && selectedHex?.OccupiedEntity is Character) {
				var character = selectedHex?.OccupiedEntity as Character;
				SetSelectedCharacter(character);
			}
		}
	}
}