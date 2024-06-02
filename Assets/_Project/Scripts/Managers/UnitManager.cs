using System.Collections.Generic;
using Game.Core;
using Game.Entities.Enemies;
using Game.SpawnSystem;
using UnityEngine;
namespace Game.Managers {
	public class UnitManager : Singleton<UnitManager>  {
		private IEntityFactory<Enemy , EnemyTypes> enemyFactory_;
		protected override void Awake(){
			enemyFactory_ = new EnemyFactory();
			base.Awake();
		}
		public void SpawnEnemy(Transform transform , EnemyTypes enemyType){
			enemyFactory_.Spawn(transform , enemyType);
		}
	}
}
