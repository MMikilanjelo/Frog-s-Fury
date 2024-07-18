using Game.Components;
using Game.Entities;
using Game.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI {
	public class PlayerHealth : MonoBehaviour {
		private Text healthText_;
		private void Awake() {
			healthText_ = GetComponent<Text>();
		}
		private void OnEnable() {
			SelectionManager.Instance.EntitySelected += (Entity entity) => {
				DisplayHealth(entity);
			};
		}
		private void DisplayHealth(Entity entity) {
			if (entity?.TryGetComponent(out HealthComponent healthComponent) ?? false) {
				healthText_.text = healthComponent.CurrentHealth.ToString();
			}
		}
	}
}
