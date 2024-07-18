using Game.Entities;
namespace Game.Commands {
	public class DealDamageCommand : ICommand {
		private IDamageable damageable_;
		private int damage_ = 1;
		public void Execute() {
			if (damageable_ != null) {
				damageable_.TakeDamage(damage_);
			}
		}
		private DealDamageCommand() { }
		public class Builder {

			private readonly DealDamageCommand command_ = new DealDamageCommand();
			public Builder WithTargetedEntity(IDamageable damageable) {
				command_.damageable_ = damageable;
				return this;
			}
			public Builder WithDamage(int damage) {
				if (damage > 0) {
					command_.damage_ = damage;
				}
				return this;
			}
			public ICommand Build() => command_;
		}
	}
}

