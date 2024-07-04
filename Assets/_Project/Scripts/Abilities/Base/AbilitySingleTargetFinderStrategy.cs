using System;
using System.Collections.Generic;
using Game.Entities;
using Game.Hexagons;
namespace Game.Abilities {
	public abstract class AbilitySingleTargetFinderStrategy {
		public Hex Target { get; protected set; }
		public event Action<Hex> TargetsFind = delegate { };
		public abstract void FindTarget(Entity seeker);
		public abstract bool TryFindTarget(Entity seeker);
		protected void OnTargetsFind(Hex target) => TargetsFind?.Invoke(target);
	}
	
}
