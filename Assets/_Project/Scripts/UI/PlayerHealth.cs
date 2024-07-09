using Game.Components;
using Game.Core;
using Game.Entities;
using Game.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI {
	public class PlayerHealth : MonoBehaviour {
		private Text healthText_;
		private Entity selectedEntity_;
		private void Awake() {
			healthText_ = GetComponent<Text>();
		}
		private void OnEnable() {
			EventBus<EntitySpawnedEvent>.Register(new EventBinding<EntitySpawnedEvent>((EntitySpawnedEvent entitySpawnedEvent) => {
				if (entitySpawnedEvent.entityInstance.TryGetComponent<HealthComponent>(out HealthComponent healthComponent)) {
					healthComponent.HealthChanged += (HealthUpdate healthUpdate) => {
						DisplayHealth(selectedEntity_);
					};
				}
			}));
			SelectionManager.Instance.EntitySelected += (Entity entity) => {
				selectedEntity_ = entity;
				DisplayHealth(selectedEntity_);
			};
		}
		private void DisplayHealth(Entity entity) {
			if (entity?.TryGetComponent(out HealthComponent healthComponent) ?? false) {
				healthText_.text = healthComponent.CurrentHealth.ToString();
			}
		}
	}
}
