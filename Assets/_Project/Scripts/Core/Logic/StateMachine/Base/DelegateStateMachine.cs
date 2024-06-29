using System.Collections.Generic;
namespace Game.Core.Logic {
	public class DelegateStateMachine {
		public delegate void State();

		private State currentState_;
		private readonly Dictionary<State, StateFlows> states_ = new();

		public void AddState(State normalState, State enterState = null, State exitState = null) {
			var StateFlows = new StateFlows(normalState, enterState, exitState);
			states_[normalState] = StateFlows;
		}
		public void ChangeState(State toStateDelegate) {
			states_.TryGetValue(toStateDelegate, out var stateDelegates);
			SetState(stateDelegates);
		}
		public void Update() => currentState_?.Invoke();
		public State GetCurrentState() => currentState_;
		public void SetInitialState(State stateDelegate) {
			states_.TryGetValue(stateDelegate, out var stateFlows);
			SetState(stateFlows);
		}
		private void SetState(StateFlows stateFlows) {
			if (currentState_ != null) {
				states_.TryGetValue(currentState_, out var currentStateDelegates);
				currentStateDelegates?.ExitState?.Invoke();
			}
			currentState_ = stateFlows.Normal;
			stateFlows?.EnterState?.Invoke();
		}

		private class StateFlows {
			public State Normal { get; private set; }
			public State EnterState { get; private set; }

			public State ExitState { get; private set; }

			public StateFlows(State normal, State enterState = null, State exitState = null) {
				Normal = normal;
				EnterState = enterState;
				ExitState = exitState;
			}
		}
	}
}