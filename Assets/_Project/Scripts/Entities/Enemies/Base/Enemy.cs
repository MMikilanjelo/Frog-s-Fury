namespace Game.Entities.Enemies {
	/// <summary>
	/// Base class for all enemies in game
	/// </summary>
	public abstract class Enemy : AbilityPerformer {
		public EnemyData Data { get; protected set; }
		public void SetEnemyData(EnemyData data) => Data = data;
	}
}
