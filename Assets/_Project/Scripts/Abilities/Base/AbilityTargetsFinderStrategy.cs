using System;
using System.Collections.Generic;
using Game.Entities;
using Game.Hexagons;

namespace Game.Abilities {
	public abstract class AbilityTargetsFinderStrategy<T> where T : TargetData {
		public event Action<List<T>> TargetsFind = delegate { };
		public abstract bool TryFindTargets(Entity seeker, out List<T> data);
		public void OnTargetsFind(List<T> targets) => TargetsFind?.Invoke(targets);
	}
	
	public class TargetData {
		public Hex Hex { get; private set; }
		public TargetData(Hex hex) => Hex = hex;
	}
	
	public class TargetDataWithAdditionalTarget<T> : TargetData where T : class {
		public T Target { get; private set; }
		public TargetDataWithAdditionalTarget(Hex hex, T target) : base(hex) {
			Target = target;
		}
	}

}
