using Game.Core;
using Game.Entities.Characters;
using Game.Managers;
using Game.Utils;
using Game.Utils.Helpers;
using Game.Abilities;
using System.Collections.Generic;

namespace Game.Systems.AbilitySystem {
	public class AbilityController {

		private readonly AbilityModel model_;
		private readonly AbilityView view_;
		private Character character_;
		private Ability currentAbility_;
		private AbilityController(AbilityView abilityView, AbilityModel abilityModel) {
			view_ = abilityView;
			model_ = abilityModel;
			ConnectModel();
			ConnectView();
		}
		private void ConnectModel() => model_.CharacterAdded += (Character character, IList<Ability> abilities) => {
			character_ = character;
		};

		private void ConnectView() {
			for (int i = 0; i < view_.buttons.Length; i++) {
				view_.buttons[i].RegisterListener(OnAbilityButtonPressed);
			}
			TurnManager.Instance.StartPlayerTurn += () => {
				UpdateButtons();
			};
			TurnManager.Instance.EndOfPlayerTurn += () => {
				view_.SetButtonsInteractable(false);
			};
			SelectionManager.Instance.CharacterSelected += (Character character) => {
				character_ = character;
				if (model_.Abilities.TryGetValue(character, out ObservableList<Ability> abilities)) {
					view_.UpdateButtonSprites(abilities);
				}
				if (TurnHelpers.IsPlayerTurn(GameManager.Instance.GameState)) {
					UpdateButtons();
				}
			};
		}
		private void UpdateButtons() {
			if (model_.Abilities.TryGetValue(character_, out ObservableList<Ability> abilities)) {
				for (int i = 0; i < abilities.Count; i++) {
					view_.buttons[i].SetButtonInteractable(character_.CanPerformAction(abilities[i].AbilityActionCost));
				}
				view_.UpdateButtonSprites(abilities);
			}
		}
		private void OnAbilityButtonPressed(int index) {
			if (model_.Abilities.TryGetValue(character_, out ObservableList<Ability> abilities)) {
				if (abilities[index] == null) {
					return;
				}
				currentAbility_?.CancelAbility();
				currentAbility_ = abilities[index];
				currentAbility_.CastAbility();
			}
		}
		public class Builder {
			private readonly AbilityModel model_ = new AbilityModel();
			private AbilityView view_;
			public Builder WithCharacterSpawnedBinding() {
				EventBinding<CharacterSpawnedEvent> characterSpawnedEventBinding = new EventBinding<CharacterSpawnedEvent>
				((CharacterSpawnedEvent characterSpawnedEvent) => {
					model_.Add(characterSpawnedEvent.characterInstance);
				});

				EventBus<CharacterSpawnedEvent>.Register(characterSpawnedEventBinding);

				return this;
			}
			public Builder WithView(AbilityView view) {
				view_ = view;
				return this;
			}
			public AbilityController Build() {
				return new AbilityController(view_, model_);
			}
		}
	}
}
