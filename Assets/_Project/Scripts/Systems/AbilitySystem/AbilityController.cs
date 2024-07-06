using Game.Core;
using Game.Entities.Characters;
using Game.Managers;
using Game.Utils.Helpers;
using Game.Abilities;
using System.Collections.Generic;
using Game.Entities;
using Game.Entities.Enemies;

namespace Game.Systems.AbilitySystem {
	public class AbilityController {

		private AbilityModel model_ => UnitManager.Instance.AbilityModel;
		private readonly AbilityView view_;
		private Character character_;
		private Ability currentAbility_;
		private AbilityController(AbilityView abilityView) {
			view_ = abilityView;
			ConnectView();
		}
		private void ConnectView() {
			for (int i = 0; i < view_.buttons.Length; i++) {
				view_.buttons[i].RegisterListener(OnAbilityButtonPressed);
			}
			TurnManager.Instance.StartPlayerTurn += () => {
				UpdateButtonsInteractable(character_);
			};
			TurnManager.Instance.EndOfPlayerTurn += () => {
				view_.SetButtonsInteractable(false);
			};
			SelectionManager.Instance.CharacterSelected += (Character character) => {
				character_ = character;
				UpdateButtonsSprites(character_);
				if (TurnHelpers.IsPlayerTurn()) {
					UpdateButtonsInteractable(character_);
				}
			};
			SelectionManager.Instance.EnemySelected += (Enemy enemy) => {
				UpdateButtonsSprites(enemy);
				view_.SetButtonsInteractable(false);
			};
		}
		private void UpdateButtonsSprites(Entity entity) {
			if (model_.EntityAbilities.TryGetValue(entity, out List<Ability> abilities)) {
				view_.UpdateButtonSprites(abilities);
			}
		}
		private void UpdateButtonsInteractable(Entity entity) {
			if (model_.EntityAbilities.TryGetValue(entity, out List<Ability> abilities)) {
				for (int i = 0; i < abilities.Count; i++) {
					view_.buttons[i].SetButtonInteractable(abilities[i].CanCastAbility());
				}
			}
		}
		private void OnAbilityButtonPressed(int index) {
			if (model_.EntityAbilities.TryGetValue(character_, out List<Ability> abilities)) {
				if (abilities[index] == null) {
					return;
				}
				currentAbility_?.CancelAbility();
				currentAbility_ = abilities[index];
				currentAbility_.CastAbility();
			}
		}
		public class Builder {
			private AbilityView view_;
			public Builder WithView(AbilityView view) {
				view_ = view;
				return this;
			}
			public AbilityController Build() {
				return new AbilityController(view_);
			}
		}
	}
}
