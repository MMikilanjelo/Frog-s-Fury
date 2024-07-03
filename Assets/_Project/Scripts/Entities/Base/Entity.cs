using System.Collections.Generic;
using Game.Abilities;
using Game.Hexagons;
using UnityEngine;


namespace Game.Entities {
	/// <summary>
	/// Base class for all Entities in game
	/// </summary>
	public abstract class Entity : MonoBehaviour {
		public Hex OccupiedHex { get; protected set; }
		public EntityData Data { get; protected set; }
		public void SetOccupiedHex(Hex hex) => OccupiedHex = hex;
		public abstract bool CanPerformAbility(int actionCost);
		public abstract void PerformAbility(int abilityCost);
		public abstract int GetRemainingActions();
		public IReadOnlyDictionary<AbilityTypes, IAbilityStrategy> Abilities { get; protected set; }
	}
}


