using Game.Entities;
using Game.Entities.Enemies;
using Game.Hexagons;
using UnityEngine;
namespace Game.Systems.SpawnSystem {
	public class EnemyFactory : IEntityFactory<Enemy, EntityTypes> {
		public Enemy Spawn(Hex hex, EntityTypes entityTypeEnum) {
			if (ResourceSystem.Instance.TryGetEntityData(entityTypeEnum, out EntityData data)) {
				var enemyInstance = GameObject.Instantiate(data.Prefab, hex.WorldPosition, Quaternion.identity);

				enemyInstance.SetOccupiedHex(hex);
				hex.SetOccupiedEntity(enemyInstance);
				var enemy = enemyInstance.GetComponent<Enemy>();
				enemy.SetEnemyData(data as EnemyData);
				return enemy;
			}
			return null;
		}
	}
}
