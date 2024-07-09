
using System;
using Game.Core;
using Game.Entities.Characters;
using Game.Selection;
using Game.Hexagons;
using UnityEngine;
using Game.Entities.Enemies;
using Game.Entities;

namespace Game.Managers {
	public class SelectionManager : Singleton<SelectionManager> {
		public event Action<Entity> EntitySelected = delegate { };
		public event Action<Enemy> EnemySelected = delegate { };
		public event Action<Character> CharacterSelected = delegate { };
		public Entity SelectedEntity { get; private set; }
		public ISelectionResponse SelectionResponse => selectionResponse_;

		private ISelectionResponse selectionResponse_;
		private ISelectionResponse defaultSelectionResponse_;
		private IRayProvider rayProvider_;
		private ISelector selector_;
		private Hex selectedHex_;
		private bool enableSelection_ = false;

		protected override void Awake() {
			base.Awake();
			rayProvider_ = new MouseScreenRayProvider();
			defaultSelectionResponse_ = new HightLightSelectionResponse();
			selectionResponse_ = defaultSelectionResponse_;
			selector_ = new RayCastBasedTileSelector();
		}

		private void Update() {
			if (Input.GetMouseButtonDown(0) && enableSelection_) {
				if (selectedHex_ != null) {
					selectionResponse_?.OnDeselect(selectedHex_);
				}
				selector_.Check(rayProvider_.CreateRay());
				selectedHex_ = selector_.GetSelectedHex();

				if (selectedHex_ != null) {
					selectionResponse_?.OnSelect(selectedHex_);
				}
			}
		}
		public void EnableSelection() => enableSelection_ = true;
		public void DecorateSelectionResponse(SelectionResponseDecorator selectionResponseDecorator) {
			selectionResponseDecorator.Decorate(selectionResponse_);
			selectionResponse_ = selectionResponseDecorator;
		}
		public void ResetSelectionResponseToDefault() => selectionResponse_ = defaultSelectionResponse_;
		public void SetSelectedEntity(Entity entity) {
			if (entity is Character character) {
				CharacterSelected?.Invoke(character);
			}
			if (entity is Enemy enemy) {
				EnemySelected?.Invoke(enemy);
			}
			EntitySelected?.Invoke(entity);
		}
		public void OnSelected(Hex selectedHex) {
			if (selectedHex?.OccupiedEntity != null) {
				SetSelectedEntity(selectedHex.OccupiedEntity);
			}
		}
	}
}