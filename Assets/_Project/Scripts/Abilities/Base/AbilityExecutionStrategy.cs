using System;
using Game.Hexagons;
using Game.Selection;

namespace Game.Abilities {
	public abstract class AbilityExecutionStrategy {
		public abstract void CastAbility(Hex selectedHex , AbilityData abilityData);
		public event Action AbilityExecuted;
		public void OnAbilityExecuted(){
			AbilityExecuted?.Invoke();
		}
	}
}
