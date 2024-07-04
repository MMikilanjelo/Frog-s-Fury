
namespace Game.Entities.Enemies {
	/// <summary>
	/// Base class for all enemies in game
	/// </summary>
	public abstract class Enemy : Entity {
		public EnemyData EnemyData { get; protected set; }
		public void SetEnemyData(EnemyData enemyData) => EnemyData = enemyData;
	}
}
