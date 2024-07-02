using System.Collections.Generic;
using System;

using Game.Abilities;
using Game.Entities;
namespace Game.Systems.AbilitySystem {
	public class AbilityModel<T> where T : Entity {
		public readonly Dictionary<T, List<Ability>> EntityAbilities = new();
		public event Action<T, IList<Ability>> EntityAdded = delegate { };
		public void Add(T entity) {
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
			EntityAdded?.Invoke(entity, characterAbilities);
		}
	}
}
