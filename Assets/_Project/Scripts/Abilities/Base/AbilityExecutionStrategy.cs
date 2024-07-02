using System;
using Game.Hexagons;

namespace Game.Abilities {
	public abstract class AbilityExecutionStrategy {
		public abstract void CastAbility(Hex selectedHex);
		protected AbilityData AbilityData { get; private set; }
		public void SetAbilityData(AbilityData abilityData) {
			AbilityData = abilityData;
		}
		public event Action AbilityExecuted;
		protected void OnAbilityExecuted() {
			AbilityExecuted?.Invoke();
		}
	}
}
