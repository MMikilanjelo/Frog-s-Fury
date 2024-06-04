using System;
using Game.Entities;
using Game.Hexagons;

namespace Game.Systems.SpawnSystem {
	public interface IEntityFactory<T , E> where T : Entity where E : Enum {
		public T Spawn(HexNode hexNode , E entityTypeEnum);
	}
}
