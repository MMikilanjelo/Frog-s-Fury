namespace Game.Abilities {
	public interface IAbilityStrategy {
		public bool CanCastAbility();
		public void CastAbility();
		public void CancelAbility();
	}
}

