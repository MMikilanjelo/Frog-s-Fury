
using System.Collections.Generic;
using System;
namespace Game.Core.Logic {
	public class StateMachine {
		StateNode current_;
		Dictionary<Type, StateNode> nodes_ = new();
		HashSet<ITransition> anyTransitions_ = new();

		public void Update() {
			var transition = GetTransition();
			if (transition != null) {
				ChangeState(transition.To);
			}

			current_.State?.Update();
		}

		public void FixedUpdate() {
			current_.State?.FixedUpdate();
		}

		public void SetState(IState state) {
			current_ = nodes_[state.GetType()];
			current_.State?.OnEnter();
		}

		void ChangeState(IState state) {
			if (state == current_.State) return;

			var previousState = current_.State;
			var nextState = nodes_[state.GetType()].State;

			previousState?.OnExit();
			nextState?.OnEnter();
			current_ = nodes_[state.GetType()];
		}

		ITransition GetTransition() {
			foreach (var transition in anyTransitions_) {
				if (transition.Condition.Evaluate()) {
					return transition;
				}
			}
			foreach (var transition in current_.Transitions) {
				if (transition.Condition.Evaluate()) {
					return transition;
				}
			}


			return null;
		}

		public void AddTransition(IState from, IState to, IPredicate condition) {
			GetOrAddNode(from).AddTransition(GetOrAddNode(to).State, condition);
		}

		public void AddAnyTransition(IState to, IPredicate condition) {
			anyTransitions_.Add(new Transition(GetOrAddNode(to).State, condition));
		}

		StateNode GetOrAddNode(IState state) {
			var node = nodes_.GetValueOrDefault(state.GetType());

			if (node == null) {
				node = new StateNode(state);
				nodes_.Add(state.GetType(), node);
			}

			return node;
		}

		class StateNode {
			public IState State { get; }
			public HashSet<ITransition> Transitions { get; }

			public StateNode(IState state) {
				State = state;
				Transitions = new HashSet<ITransition>();
			}

			public void AddTransition(IState to, IPredicate condition) {
				Transitions.Add(new Transition(to, condition));
			}
		}
	}
}