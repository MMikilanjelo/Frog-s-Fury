using System;
using System.Collections.Generic;
using Game.Entities;
using Game.Hexagons;
namespace Game.Abilities {
	public abstract class AbilityTargetsFinderStrategy<T> where T : class, ITargetData {
		public event Action<List<T>> TargetsFind = delegate { };
		public abstract bool TryFindTargets(Entity seeker, out List<T> data);
		public void OnTargetsFind(List<T> targets) => TargetsFind?.Invoke(targets);
	}
	public interface ITargetData {
		Hex Hex { get; }
	}
	public class TargetData<T> : ITargetData where T : class {
		public Hex Hex { get; set; }
		public T Target { get; set; }

		public TargetData(Hex hex, T additionalTarget) {
			Hex = hex;
			Target = additionalTarget;
		}
	}
}
