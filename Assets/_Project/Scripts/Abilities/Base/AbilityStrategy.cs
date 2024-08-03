
using System;
using Game.Entities;

namespace Game.Abilities {
	public abstract class AbilityStrategy<T> : IAbilityStrategy where T : TargetData {
		public bool Enabled { get; protected set; } = true;
		public bool Executed { get; protected set; } = false;
		public int ExecutionCost { get; protected set; }
		
		public AbilityPerformer AbilityPerformer { get; protected set; }
		
		public event Action AbilityExecuted = delegate { };
		public event Action AbilityEnabled = delegate { };
		public event Action AbilityDisabled = delegate { };

		public abstract bool CanCastAbility();
		public abstract void CastAbility();
		public virtual void CancelAbility() { }

		public void EnableAbility() {
			Enabled = true;
			Executed = false;
			AbilityEnabled?.Invoke();
		}

		protected void OnAbilityExecuted() {
			Executed = true;
			AbilityExecuted?.Invoke();
		}

		protected void OnAbilityCasted() {
			Enabled = false;
			AbilityDisabled?.Invoke();
		}
	}
}
