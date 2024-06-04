using System.Collections.Generic;
using Game.Commands;
using Game.Core;
using Game.Entities.Characters;
using Game.Managers;
using Game.Utils;
using UnityEngine;

namespace Game.Systems.AbilitySystem {
	public class AbilityController {

		private readonly AbilityModel model_;
		private readonly AbilityView view_;
		private Character character_;
		private ICommand currentCommand_;
		private AbilityController(AbilityView abilityView, AbilityModel abilityModel) {
			view_ = abilityView;
			model_ = abilityModel;
			ConnectModel();
			ConnectView();
		}
		private void ConnectModel() {
			model_.CharacterAdded += (IList<Ability> abilities) => view_.UpdateButtonSprites(abilities);  
		}
		private void ConnectView() {
			for (int i = 0; i < view_.buttons.Length; i++) {
				view_.buttons[i].RegisterListener(OnAbilityButtonPressed);
			}
		}
		private void UpdateButtons() {
			if (model_.Abilities.TryGetValue(character_, out ObservableList<Ability> abilities)) {
				view_.UpdateButtonSprites(abilities);
			}
		}
		private void OnAbilityButtonPressed(int index) {
			if (model_.Abilities.TryGetValue(character_, out ObservableList<Ability> abilities)) {
				if (abilities[index] == null) {
					return;
				}
				currentCommand_ = abilities[index].GetAbilityCommand();
				currentCommand_.Execute();
				UpdateButtons();
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
			public Builder WithView(AbilityView view){
				view_ = view;
				return this;
			}
			public AbilityController Build() {
				return new AbilityController(view_, model_);
			}
		}
	}
}
