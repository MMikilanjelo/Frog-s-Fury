using Game.Hexagons;
using UnityEngine;


namespace Game.Entities {
	/// <summary>
	/// Base class for all Entities in game
	/// </summary>
	public abstract class Entity : MonoBehaviour {
		public HexNode OccupiedHexTile{get;protected set;}
		public void SetOccupiedHexTile(HexNode hexNode){
			OccupiedHexTile = hexNode;
		}
	}
}


