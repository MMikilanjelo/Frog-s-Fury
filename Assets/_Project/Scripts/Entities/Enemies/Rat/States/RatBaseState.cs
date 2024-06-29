using Game.Core.Logic;

namespace Game.Entities.Enemies {
	public abstract class RatBaseState : IState {
		protected RatBaseState() {
		}
		public virtual void FixedUpdate() {
			//noop
		}

		public virtual void OnEnter() {
			//noop
		}

		public virtual void OnExit() {
			//noop
		}

		public virtual void Update() {
			//noop
		}
	}
}