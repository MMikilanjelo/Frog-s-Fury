using System;
using Game.Entities;
using UnityEngine;

namespace Game.SpawnSystem {
	public class EntitySpawner<T, E> where T : Entity where E : Enum {
		private IEntityFactory<T, E> entityFactory_;

		public EntitySpawner(IEntityFactory<T, E> entityFactory) {
			entityFactory_ = entityFactory;
		}

		public T Spawn(Transform transform, E entityType) {
			return entityFactory_.Spawn(transform, entityType);
		}
	}
}
