

namespace Game.Abilities {
	public class Ability {
		public AbilityData Data { get; private set; }
		public AbilityStrategy AbilityStrategy { get; private set; }
		public bool AbilityExecuted => AbilityStrategy.Enabled;
		private Ability(AbilityData data, AbilityStrategy abilityStrategy) {
			Data = data;
			AbilityStrategy = abilityStrategy;
		}
		public void CastAbility() => AbilityStrategy.CastAbility();
		public void CancelAbility() => AbilityStrategy.CancelAbility();
		public bool CanCastAbility() => AbilityStrategy.CanCastAbility();
		public class Builder {
			private AbilityData data_;
			private AbilityStrategy abilityStrategy_;
			public Builder WithData(AbilityData data) {
				data_ = data;
				return this;
			}
			public Builder WithStrategy(AbilityStrategy abilityStrategy) {
				abilityStrategy_ = abilityStrategy;
				return this;
			}
			public Ability Build() {
				return new Ability(data_, abilityStrategy_);
			}
		}
	}
}
