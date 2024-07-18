using System.Collections.Generic;
using UnityEngine;

namespace Game.Abilities {
	public class AbilityManagerComponent : MonoBehaviour {
		public Dictionary<AbilityTypes, IAbilityStrategy> Abilities { get; private set; } = new();
		public void SetAbilities(Dictionary<AbilityTypes, IAbilityStrategy> abilities) {
			Abilities = abilities;
		}
		public bool HasAbilitiesRemain() {
			foreach (var ability in Abilities.Values) {
				if (!ability.Executed) {
					return true;
				}
			}
			return false;
		}
		public void EnableAbilities() {
			foreach (var ability in Abilities.Values) {
				ability.EnableAbility();
			}
		}
	}
}
