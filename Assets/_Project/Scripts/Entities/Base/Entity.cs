using Game.Hexagons;
using UnityEngine;


namespace Game.Entities {
	/// <summary>
	/// Base class for all Entities in game
	/// </summary>
	public abstract class Entity : MonoBehaviour {
		public Hex OccupiedHex { get; protected set; }
		public void SetOccupiedHex(Hex hex) {
			OccupiedHex = hex;
		}
	}
}


