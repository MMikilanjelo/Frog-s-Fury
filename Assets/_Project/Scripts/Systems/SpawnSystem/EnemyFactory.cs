using Game.Entities.Enemies;
using Game.Hexagons;
using UnityEngine;
namespace Game.Systems.SpawnSystem {
	public class EnemyFactory : IEntityFactory<Enemy, EnemyTypes> {
		public Enemy Spawn(Hex hex, EnemyTypes entityTypeEnum) {
			if(ResourceSystem.Instance.TryGetEnemyData(entityTypeEnum , out EnemyData data)){
				var enemyInstance = GameObject.Instantiate(data.Prefab , hex.WorldPosition , Quaternion.identity);
				
				enemyInstance.SetOccupiedHex(hex);
				hex.SetOccupiedEntity(enemyInstance);
				
				return enemyInstance.GetComponent<Enemy>();
			}	
			return null;
		}
	}
}
