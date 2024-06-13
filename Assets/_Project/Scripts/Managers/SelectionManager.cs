using System;

using Game.Core;
using Game.Entities.Characters;
using Game.Selection;

using UnityEngine;

namespace Game.Managers {
	public class SelectionManager : Singleton<SelectionManager> {
		public event Action<Character> CharacterSelected = delegate { };
		public Character SelectedCharacter { get; private set; }
		public ISelectionResponse SelectionResponse => selectionResponse_;

		private ISelectionResponse selectionResponse_;
		private IRayProvider rayProvider_;
		private ISelector selector_;
		private SelectionData selectionData_;

		protected override void Awake() {
			base.Awake();
			rayProvider_ = new MouseScreenRayProvider();
			selectionResponse_ = new HightLightSelectionResponse();
			selector_ = new RayCastBasedTileSelector();
		}
		private void Update() {
			if (Input.GetMouseButtonDown(0)) {
				if (selectionData_ != null) {
					selectionResponse_?.OnDeselect(selectionData_);
				}
				selector_.Check(rayProvider_.CreateRay());
				selectionData_ = selector_.GetSelectionData();

				if (selectionData_ != null) {
					selectionResponse_?.OnSelect(selectionData_);
					OnSelected(selectionData_);
				}
			}
		}
		public void SetSelectionResponse(ISelectionResponse selectionResponse) => selectionResponse_ = selectionResponse;
		public void SetSelectedCharacter(Character character) {
			SelectedCharacter = character;
			CharacterSelected?.Invoke(character);
		}
		private void OnSelected(SelectionData selectionData) {
			if (selectionData?.SelectedHexNode?.OccupiedEntity != null && selectionData?.SelectedHexNode?.OccupiedEntity is Character) {
				var character = selectionData?.SelectedHexNode?.OccupiedEntity as Character;
				SetSelectedCharacter(character);
			}
		}
	}
}