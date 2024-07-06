using System;
using UnityEngine;

namespace Game.Components {
	public class HealthComponent : MonoBehaviour {

		public event Action<HealthUpdate> HealthChanged;
		public event Action Died;
		public bool Damaged => currentHealth_ < maxHealth_;
		public bool HasHealthRemaining => !Mathf.Approximately(currentHealth_, 0f);
		private float maxHealth_ = 15.0f;
		private float currentHealth_ = 15.0f;
		public void Awake() => InitializeHealth(maxHealth_);
		private void InitializeHealth(float maxHealth_) {
			MaxHealth = maxHealth_;
			CurrentHealth = MaxHealth;
		}
		public float MaxHealth {
			get => maxHealth_;
			private set {
				maxHealth_ = value;
				if (currentHealth_ > maxHealth_) {
					currentHealth_ = maxHealth_;
				}
			}
		}
		public float CurrentHealth {
			get => currentHealth_;
			private set {
				var previousHealth = currentHealth_;
				currentHealth_ = Mathf.Clamp(value, 0, MaxHealth);
				var healthUpdate = new HealthUpdate {
					CurrentHealth = currentHealth_,
					MaxHealth = maxHealth_,
					PreviousHealth = previousHealth,
				};

				HealthChanged?.Invoke(healthUpdate);
				if (!HasHealthRemaining) {
					Died?.Invoke();
				}
			}
		}
		public void DealDamage(float damage) => CurrentHealth -= damage;
	}
	public class HealthUpdate {
		public float MaxHealth;
		public float CurrentHealth;
		public float PreviousHealth;
	}
}
