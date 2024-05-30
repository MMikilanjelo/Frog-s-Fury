using Game.Core;
using Game.Entities.Enemies;
using Game.SpawnSystem;
using Game.Systems;
namespace Game.Managers {
	public class UnitManager : Singleton<UnitManager>  {
		private IEntityFactory<Enemy , EnemyTypes> enemyFactory_;
		private EntitySpawner<Enemy , EnemyTypes> enemySpawner_;
		protected override void Awake(){
			base.Awake();
			enemyFactory_ = new EnemyFactory();
			enemySpawner_ = new EntitySpawner<Enemy,EnemyTypes>(enemyFactory_);

		}
		private void Start(){
			enemySpawner_.Spawn(transform , EnemyTypes.Rat);
		}
		public void SpawnUnit(){ 
		}
	}
}
