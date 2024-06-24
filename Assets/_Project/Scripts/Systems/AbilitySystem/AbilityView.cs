using UnityEngine;
using UnityEngine.InputSystem;

using System.Collections.Generic;
using Game.Abilities;

namespace Game.Systems.AbilitySystem {
	public class AbilityView : MonoBehaviour {
		[SerializeField] public AbilityButton[] buttons;
		private readonly Key[] keys_ = { Key.Digit1, Key.Digit2 };
		void Awake() {
			for (int i = 0; i < buttons.Length; i++) {
				if (i >= keys_.Length) {
					Debug.LogError("Not enough keycodes for the number of buttons.");
					break;
				}
				buttons[i].Initialize(i, keys_[i]);
			}
		}
		public void UpdateButtonSprites(IList<Ability> abilities) {
			for (int i = 0; i < buttons.Length; i++) {
				if (i < abilities.Count) {
					//buttons[i].UpdateButtonText(abilities[i].Data.Name);
					buttons[i].UpdateButtonSprite(abilities[i].Data.Sprite);
				}
				else {
					buttons[i].gameObject.SetActive(false);
				}
			}
		}
		public void SetButtonsInteractable(bool interactable) {
			for (int i = 0; i < buttons.Length; i++) {
				buttons[i].SetButtonInteractable(interactable);
			}
		}
	}
}
