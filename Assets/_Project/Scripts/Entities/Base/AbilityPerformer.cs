using System.Collections.Generic;
using Game.Abilities;
namespace Game.Entities {
	public abstract class AbilityPerformer : Entity, IDamageable {
		public Dictionary<AbilityTypes, IAbilityStrategy> Abilities { get; protected set; } = new();
		public abstract bool CanPerformAbility(int actionCost);
		public abstract void PerformAbility(int abilityCost);
		public abstract int GetRemainingActions();

		public abstract void TakeDamage(int damage);
	}
}
