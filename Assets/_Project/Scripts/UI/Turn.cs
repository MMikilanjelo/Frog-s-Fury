using Game.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI {
	public class Turn : MonoBehaviour {
		private Text text_;
		private void Awake() {

			if (TryGetComponent(out Text text)) {
				text_ = text;
			}
			TurnManager.Instance.AfterTurnPhaseChanged += (TurnPhases phase) => {
				DisplayTurnPhase(phase);
			};
		}
		private void DisplayTurnPhase(TurnPhases turnPhases) {
			text_.text = turnPhases.ToString();
		}
	}
}
