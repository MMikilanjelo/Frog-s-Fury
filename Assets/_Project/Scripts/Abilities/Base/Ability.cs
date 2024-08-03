using System;
namespace Game.Abilities {
	public class Ability {
		public AbilityData Data { get; private set; }
		public IAbilityStrategy AbilityStrategy { get; private set; }
		public bool AbilityExecuted => AbilityStrategy.Enabled;

		private Ability(AbilityData data, IAbilityStrategy abilityStrategy) {
			Data = data;
			AbilityStrategy = abilityStrategy;
		}

		public void CastAbility() => AbilityStrategy.CastAbility();
		public void CancelAbility() => AbilityStrategy.CancelAbility();
		public bool CanCastAbility() => AbilityStrategy.CanCastAbility();

		public class Builder {
			private AbilityData data_;
			private IAbilityStrategy abilityStrategy_;

			public Builder WithData(AbilityData data) {
				data_ = data;
				return this;
			}

			public Builder WithStrategy(IAbilityStrategy abilityStrategy) {
				abilityStrategy_ = abilityStrategy;
				return this;
			}

			public Ability Build() {
				if (data_ == null || abilityStrategy_ == null) {
					throw new InvalidOperationException("Ability data and strategy must be set before building the ability.");
				}
				return new Ability(data_, abilityStrategy_);
			}
		}
	}

	public interface IAbilityStrategy {
		bool Enabled { get; }
		bool Executed { get; }
		void CastAbility();
		void CancelAbility();
		bool CanCastAbility();
		void EnableAbility();
		event Action AbilityDisabled;
	}
}
