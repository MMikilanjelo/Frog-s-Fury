
using System;
using System.Collections.Generic;
using Game.Abilities;
using Game.Core;
using Game.Entities;
using Game.Entities.Characters;
using Game.Entities.Enemies;
using Game.Systems;

namespace Game.Managers {
	public class AbilityResourcesManager : Singleton<AbilityResourcesManager> {
		public readonly Dictionary<Entity, List<Ability>> EntityAbilities = new();
		public event Action<Character, List<Ability>> CharacterAdded = delegate { };
		public event Action<Enemy, List<Ability>> EnemyAdded = delegate { };

		public void Add(Character character) {
			var characterAbilities = AddAbilities(character);
			EntityAbilities.Add(character, characterAbilities);
			CharacterAdded?.Invoke(character, characterAbilities);
		}

		public void Add(Enemy enemy) {
			var enemyAbilities = AddAbilities(enemy);
			EntityAbilities.Add(enemy, enemyAbilities);
			EnemyAdded?.Invoke(enemy, enemyAbilities);
		}

		public List<Ability> Get(Entity entity) {
			if (entity != null && EntityAbilities.TryGetValue(entity, out var abilities)) {
				return abilities;
			}
			return new List<Ability>();
		}

		private List<Ability> AddAbilities(Entity entity) {
			var abilities = new List<Ability>();
			foreach (var abilityType in entity.Abilities.Keys) {
				if (ResourceSystem.Instance.TryGetAbilityData(abilityType, out AbilityData data)) {
					var ability = new Ability.Builder()
							.WithData(data)
							.WithStrategy(entity.Abilities[abilityType])
							.Build();
					abilities.Add(ability);
				}
			}
			return abilities;
		}
	}
}