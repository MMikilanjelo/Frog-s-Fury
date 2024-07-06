using UnityEngine;

namespace Game.Entities.Enemies {
	[CreateAssetMenu(fileName = "EnemyData", menuName = "EntityData/EnemyData")]
	public class EnemyData : EntityData {
		private void OnValidate() {
			if (Prefab == null) {
				Debug.LogError($"Set Up prefab  {Type}");
			}
		}
	}
}
