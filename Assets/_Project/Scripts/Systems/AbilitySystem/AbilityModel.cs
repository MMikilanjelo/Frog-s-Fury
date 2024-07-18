using System.Collections.Generic;

using Game.Abilities;
using Game.Entities;
namespace Game.Systems.AbilitySystem {
	public class AbilityModel {
		public readonly Dictionary<AbilityPerformer, List<Ability>> EntityAbilities = new();
		public void Add(AbilityPerformer abilityPerformer) {
			var characterAbilities = new List<Ability>();
			foreach (var abilityType in abilityPerformer.Abilities.Keys) {
				if (ResourceSystem.Instance.TryGetAbilityData(abilityType, out AbilityData data)) {
					var ability = new Ability.Builder()
						.WithData(data)
						.WithStrategy(abilityPerformer.Abilities[abilityType])
						.Build();
					characterAbilities.Add(ability);
				}
			}
			EntityAbilities.Add(abilityPerformer, characterAbilities);
		}
		public List<Ability> Get(AbilityPerformer abilityPerformer) {
			if (abilityPerformer != null) {
				if (EntityAbilities.TryGetValue(abilityPerformer, out var abilities)) {
					return abilities;
				}
			}
			return new List<Ability>();
		}
	}
}
