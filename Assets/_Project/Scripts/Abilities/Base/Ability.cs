

namespace Game.Abilities {
	public class Ability {
		public int AbilityActionCost => Data.Cost;
		public AbilityData Data { get; private set; }
		private IAbilityStrategy abilityStrategy_;
		private Ability(AbilityData data, IAbilityStrategy abilityStrategy) {
			Data = data;
			abilityStrategy_ = abilityStrategy;
			abilityStrategy_.SetAbilityData(Data);
		}
		public void CastAbility() => abilityStrategy_.CastAbility();
		public void CancelAbility() => abilityStrategy_.CancelAbility();
		public bool CanCastAbility() => abilityStrategy_.CanCastAbility();
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
				return new Ability(data_, abilityStrategy_);
			}
		}
	}
}
