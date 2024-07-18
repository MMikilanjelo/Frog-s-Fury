using Game.Entities.Characters;
using Game.Managers;
using Game.Utils.Helpers;
using Game.Abilities;
using Game.Entities;
using Game.Entities.Enemies;
using UnityEngine;
using System.Collections.Generic;

namespace Game.Systems.AbilitySystem {
	public class AbilityController {
		private readonly AbilityView view_;
		private Character character_;
		private Ability currentAbility_;
		private AbilityController(AbilityView abilityView) {
			view_ = abilityView;
			ConnectView();
			ConnectModel();
		}
		public void Update() {
			if (Input.GetKeyDown(KeyCode.Escape)) {
				currentAbility_?.CancelAbility();
				currentAbility_ = null;
			}
		}
		private void ConnectModel() {
			AbilityResourcesManager.Instance.CharacterAdded += (Character character, List<Ability> abilities) => {
				foreach (var ability in abilities) {
					ability.AbilityStrategy.AbilityDisabled += () => {
						UpdateButtonsInteractable(character_);
					};
				}
			};
		}
		private void ConnectView() {
			for (int i = 0; i < view_.buttons.Length; i++) {
				view_.buttons[i].RegisterListener(OnAbilityButtonPressed);
			}
			TurnManager.Instance.PlayerTurn += () => UpdateButtonsInteractable(character_);
			TurnManager.Instance.EndOfPlayerTurn += () => view_.SetButtonsInteractable(false);

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
		private void UpdateButtonsSprites(AbilityPerformer abilityPerformer) {
			var abilities = AbilityResourcesManager.Instance.Get(abilityPerformer);
			if (abilities != null) {
				view_.UpdateButtonSprites(abilities);
			}
		}
		private void UpdateButtonsInteractable(AbilityPerformer abilityPerformer) {
			var abilities = AbilityResourcesManager.Instance.Get(abilityPerformer);
			for (int i = 0; i < abilities?.Count; i++) {
				view_.buttons[i].SetButtonInteractable(abilities[i].AbilityStrategy.Enabled);
			}

		}
		private void OnAbilityButtonPressed(int index) {
			var abilities = AbilityResourcesManager.Instance.Get(character_);
			if (abilities?[index] == null) {
				return;
			}
			currentAbility_?.CancelAbility();
			currentAbility_ = abilities[index];
			currentAbility_.CastAbility();
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
