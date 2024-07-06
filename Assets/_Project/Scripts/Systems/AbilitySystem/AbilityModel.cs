using System.Collections.Generic;
using System;

using Game.Abilities;
using Game.Entities;
namespace Game.Systems.AbilitySystem {
	public class AbilityModel {
		public readonly Dictionary<Entity, List<Ability>> EntityAbilities = new();
		public void Add(Entity entity) {
			var characterAbilities = new List<Ability>();
			foreach (var abilityType in entity.Abilities.Keys) {
				if (ResourceSystem.Instance.TryGetAbilityData(abilityType, out AbilityData data)) {
					var ability = new Ability.Builder()
						.WithData(data)
						.WithStrategy(entity.Abilities[abilityType])
						.Build();
					characterAbilities.Add(ability);
				}
			}
			EntityAbilities.Add(entity, characterAbilities);
		}
	}
}
