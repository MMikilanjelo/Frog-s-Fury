using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

using System;

namespace Game.Systems.AbilitySystem {
	public class AbilityButton : MonoBehaviour {
		public Image AbilityIcon { get; private set; }
		public Key Key { get; private set; }
		public int Index { get; private set; }
		public Text Label { get; private set; }
		public Action<int> OnButtonPressed = delegate { };
		private Button button_;
		void Awake() {
			button_ = GetComponent<Button>();
			Label =  GetComponentInChildren<Text>();
			AbilityIcon = GetComponent<Image>();
			button_.onClick.AddListener(() => OnButtonPressed(Index));
		}
		public void Initialize(int Index, Key Key) {
			this.Key = Key;
			this.Index = Index;
		}
		void Update() {
			if (Keyboard.current[Key].wasPressedThisFrame && button_.interactable) {
				OnButtonPressed(Index);
			}
		}
		public void RegisterListener(Action<int> listener) => OnButtonPressed += listener;
		public void UnregisterListeners(Action<int> listener) => OnButtonPressed -= listener;
		public void UpdateButtonSprite(Sprite newIcon) => AbilityIcon.sprite = newIcon;
		public void UpdateButtonText(string text) => Label.text = text;
		public void SetButtonInteractable(bool interactable) => button_.interactable = interactable;
	}
}
