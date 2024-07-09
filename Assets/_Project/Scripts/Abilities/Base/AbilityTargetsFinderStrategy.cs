using System;
using System.Collections.Generic;
using Game.Entities;
using Game.Hexagons;
namespace Game.Abilities {
	public abstract class AbilityTargetsFinderStrategy {
		public event Action<List<TargetData>> TargetsFind = delegate { };
		public abstract bool TryFindTargets(Entity seeker, out List<TargetData> data);
		public void OnTargetsFind(List<TargetData> targets) => TargetsFind?.Invoke(targets);
	}
	public class TargetData {
		public Hex Hex;
	}
}
