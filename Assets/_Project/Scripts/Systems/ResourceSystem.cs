using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Core;
using Game.Entities.Enemies;
using System.Linq;
namespace Game.Systems {
	public class ResourceSystem : Singleton<ResourceSystem> {
		
		public IReadOnlyList<EnemyData> EnemyData;
		private const string ENEMY_RESOURCE_FOLDER = "Enemies";
		private  Dictionary<EnemyTypes , EnemyData> enemyDataCollection_ = new();
		protected override void Awake() {
			base.Awake();
			AssembleResources();
		}
		private void AssembleResources(){
			EnemyData = Resources.LoadAll<EnemyData>(ENEMY_RESOURCE_FOLDER).ToList();

			enemyDataCollection_ = EnemyData.ToDictionary(enemy => enemy.Type , enemy => enemy);
		}
		private bool TryGetData<K, T>(K key, Dictionary<K, T> collection, out T data) where T : class {
			return collection.TryGetValue(key, out data);
		}

		public bool TryGetEnemyData(EnemyTypes type, out EnemyData data) {
			return TryGetData(type, enemyDataCollection_ , out data);
		}
	}
}
