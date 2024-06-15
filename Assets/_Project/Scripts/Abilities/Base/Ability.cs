
namespace Game.Abilities {
	public class Ability {
		public int AbilityActionCost => Data.AbilityCost;
		public AbilityData Data { get; private set; }
		
		private AbilityStrategy abilityStrategy_;
		private AbilitySelectionStrategy abilitySelectionStrategy_;
		private Ability(AbilityData data, AbilityStrategy abilityStrategy , AbilitySelectionStrategy abilitySelectionStrategy) {
			Data = data;
			abilitySelectionStrategy_ = abilitySelectionStrategy;
			abilityStrategy_ = abilityStrategy;
			abilityStrategy_.SetAbilityData(Data);
		}
		public void BeginSelection() => abilitySelectionStrategy_.StartSelection();
		public void EndSelection() => abilitySelectionStrategy_.EndSelection();
		
		public class Builder{
			private AbilityData data_;
			private AbilityStrategy abilityStrategy_;
			private AbilitySelectionStrategy abilitySelectionStrategy_;
			public Builder WithData(AbilityData data){
				data_ = data;
				return this;
			}
			public Builder WithStrategy(AbilityStrategy abilityStrategy){
				abilityStrategy_ = abilityStrategy;
				return this;
			}
			public Builder WithAbilitySelectionStrategy(AbilitySelectionStrategy abilitySelectionStrategy){
				abilitySelectionStrategy_ = abilitySelectionStrategy;
				return this;
			}
			public Ability Build() => new Ability(data_ , abilityStrategy_ , abilitySelectionStrategy_);
		}
	}
}
