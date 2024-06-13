
namespace Game.Abilities {
	public class Ability {
		public AbilityData Data { get; private set; }
		private AbilityStrategy abilityStrategy_;
		private Ability(AbilityData data, AbilityStrategy abilityStrategy) {
			Data = data;
			abilityStrategy_ = abilityStrategy;
			abilityStrategy_.SetAbilityData(Data);
		}
		public int GetAbilityActionCost() => Data.AbilityCost;
		public AbilityStrategy GetAbilityStrategy() => abilityStrategy_;
		public class Builder{
			private AbilityData data_;
			private AbilityStrategy abilityStrategy_;
			public Builder WithData(AbilityData data){
				data_ = data;
				return this;
			}
			public Builder WithStrategy(AbilityStrategy abilityStrategy){
				abilityStrategy_ = abilityStrategy;
				return this;
			}
			public Ability Build() => new Ability(data_ , abilityStrategy_);
		}
	}
}
