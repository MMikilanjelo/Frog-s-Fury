using System;
using System.Collections.Generic;
using Game.Entities;
using Game.Hexagons;
namespace Game.Abilities {
	public abstract class AbilityTargetFinderStrategy {
		public HashSet<Hex> Targets { get; protected set; } = new();
		public event Action<HashSet<Hex>> TargetsFind = delegate { };
		protected void OnTargetsFind(HashSet<Hex> targets) => TargetsFind?.Invoke(targets);
		public abstract void FindTargets(Entity seeker);	
		public abstract bool TryFindTargets(Entity seeker);
	}
}
