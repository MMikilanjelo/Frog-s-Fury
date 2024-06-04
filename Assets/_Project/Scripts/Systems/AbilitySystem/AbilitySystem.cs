using UnityEngine;
namespace Game.Systems.AbilitySystem {
	public class AbilitySystem : MonoBehaviour {

		[SerializeField] AbilityView view_;
		private AbilityController controller_;

		void Awake() {
			controller_ = new AbilityController.Builder()
				.WithCharacterSpawnedBinding()
				.WithView(view_)
				.Build();
		}
	}
}
