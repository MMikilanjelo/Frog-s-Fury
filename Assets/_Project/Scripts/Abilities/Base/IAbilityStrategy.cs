namespace Game.Abilities {
	public interface IAbilityStrategy {
		public bool CanCastAbility();
		public void SetAbilityData(AbilityData abilityData);
		public void CastAbility();
		public void CancelAbility();
	}
}

