using UnityEngine;

namespace Game.Entities.Enemies {
	[CreateAssetMenu(fileName = "EnemyData", menuName = "EntityData/EnemyData")]
	public class EnemyData : EntityData {
		[SerializeField] public EnemyTypes Type;
		private void OnValidate() {
			if (Prefab == null) {
				Debug.LogError($"Set Up prefab  {Type}");
			}
		}
	}
}
