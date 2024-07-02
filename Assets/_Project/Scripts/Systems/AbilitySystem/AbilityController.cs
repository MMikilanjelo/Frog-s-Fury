using Game.Core;
using Game.Entities.Characters;
using Game.Managers;
using Game.Utils.Helpers;
using Game.Abilities;
using System.Collections.Generic;

namespace Game.Systems.AbilitySystem {
	public class AbilityController {

		private readonly AbilityModel<Character> model_;
		private readonly AbilityView view_;
		private Character character_;
		private Ability currentAbility_;
		private AbilityController(AbilityView abilityView, AbilityModel<Character> abilityModel) {
			view_ = abilityView;
			model_ = abilityModel;
			ConnectModel();
			ConnectView();
		}
		private void ConnectModel() => model_.EntityAdded += (Character character, IList<Ability> abilities) => {
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
				if (model_.EntityAbilities.TryGetValue(character, out List<Ability> abilities)) {
					view_.UpdateButtonSprites(abilities);
				}
				if (TurnHelpers.IsPlayerTurn()) {
					UpdateButtons();
				}
			};
		}
		private void UpdateButtons() {
			if (model_.EntityAbilities.TryGetValue(character_, out List<Ability> abilities)) {
				for (int i = 0; i < abilities.Count; i++) {
					view_.buttons[i].SetButtonInteractable(character_.CanPerformAction(abilities[i].AbilityActionCost));
				}
				view_.UpdateButtonSprites(abilities);
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
			private readonly AbilityModel<Character> model_ = new AbilityModel<Character>();
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
