using System;
using Game.Entities;

namespace Game.Abilities {
	public abstract class AbilityStrategy {
		public bool Enabled { get; protected set; } = true;
		public bool Executed { get; protected set; } = false;
		public Entity Entity { get; protected set; }
		public int ExecutionCost { get; protected set; }
		public event Action AbilityExecuted = delegate { };
		public event Action AbilityEnabled = delegate { };
		public event Action AbilityDisabled = delegate { };
		public abstract bool CanCastAbility();
		public abstract void CastAbility();
		public virtual void CancelAbility() { }
		protected void OnAbilityExecuted() {
			Executed = true;
			AbilityExecuted?.Invoke();
		}
		public void EnableAbility() {
			Enabled = true;
			Executed = false;
			AbilityEnabled?.Invoke();
		}
		protected void DisableAbility() {
			Enabled = false;
			AbilityDisabled?.Invoke();
		}
	}
}

