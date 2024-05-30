using System;
using Game.Entities;
using UnityEngine;

namespace Game.SpawnSystem {
	public interface IEntityFactory<T , E> where T : Entity where E : Enum {
		public T Spawn(Transform spawnPosition , E entityTypeEnum);
	}
}
