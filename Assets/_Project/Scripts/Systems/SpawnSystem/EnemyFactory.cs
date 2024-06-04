using Game.Entities.Enemies;
using Game.Hexagons;
using UnityEngine;
namespace Game.Systems.SpawnSystem {
	public class EnemyFactory : IEntityFactory<Enemy, EnemyTypes> {
		public Enemy Spawn(HexNode hexNode, EnemyTypes entityTypeEnum) {
			if(ResourceSystem.Instance.TryGetEnemyData(entityTypeEnum , out EnemyData data)){
				var enemyInstance = GameObject.Instantiate(data.Prefab , hexNode.Position , Quaternion.identity);
				
				enemyInstance.SetOccupiedHexNode(hexNode);
				hexNode.SetOccupiedEntity(enemyInstance);
				
				return enemyInstance.GetComponent<Enemy>();
			}	
			return null;
		}
	}
}
