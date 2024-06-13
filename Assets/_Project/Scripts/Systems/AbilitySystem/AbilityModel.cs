using System.Collections.Generic;
using System;

using Game.Utils;
using Game.Entities.Characters;
using Game.Abilities;
namespace Game.Systems.AbilitySystem {
	public class AbilityModel {
		public readonly Dictionary<Character, ObservableList<Ability>> Abilities = new();
		public event Action<Character, IList<Ability>> CharacterAdded = delegate { };
		public void Add(Character character) {
			var characterAbilities = new ObservableList<Ability>();
			foreach (var abilityType in character.CharacterAbilities.Keys) {
				if (ResourceSystem.Instance.TryGetAbilityData(abilityType, out AbilityData data)) {
					var ability = new Ability.Builder()
						.WithData(data)
						.WithStrategy(character.CharacterAbilities[abilityType])
						.Build();
					characterAbilities.Add(ability);
				}
			}
			Abilities.Add(character, characterAbilities);
			CharacterAdded?.Invoke( character, characterAbilities);
		}
	}
}
