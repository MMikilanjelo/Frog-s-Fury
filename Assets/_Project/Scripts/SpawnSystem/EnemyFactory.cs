using Game.Entities.Enemies;
using UnityEngine;
using Game.Systems;
namespace Game.SpawnSystem {
	public class EnemyFactory : IEntityFactory<Enemy, EnemyTypes> {
		public Enemy Spawn(Transform spawnPosition, EnemyTypes entityTypeEnum) {
			if(ResourceSystem.Instance.TryGetEnemyData(entityTypeEnum , out EnemyData data)){
				var enemyInstance = GameObject.Instantiate(data.Prefab ,spawnPosition);
				return enemyInstance.GetComponent<Enemy>();
			}	
			return null;
		}
	}
}
