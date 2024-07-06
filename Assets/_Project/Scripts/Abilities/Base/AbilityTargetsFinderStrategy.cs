using System;
using System.Collections.Generic;
using Game.Components;
using Game.Entities;
using Game.Hexagons;
using Game.Utils.Helpers;
namespace Game.Abilities {
	public abstract class AbilityTargetsFinderStrategy {
		public List<TargetData> TargetsData { get; protected set; } = new();
		public event Action<List<TargetData>> TargetsFind = delegate { };
		public virtual void FindTargets(Entity seeker) {
			if (TryFindTargets(seeker)) {
				OnTargetsFind(TargetsData);
			}
		}
		public abstract bool TryFindTargets(Entity seeker);
		protected void OnTargetsFind(List<TargetData> targets) => TargetsFind?.Invoke(targets);
	}
	public class AbilityTargetsData {

	}
	public class TargetData {
		public Hex Hex;
		public Entity Entity;
	}
}
