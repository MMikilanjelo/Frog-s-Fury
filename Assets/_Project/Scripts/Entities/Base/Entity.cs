using Game.Hexagons;
using UnityEngine;


namespace Game.Entities {
	/// <summary>
	/// Base class for all Entities in game
	/// </summary>
	public abstract class Entity : MonoBehaviour {
		public HexNode OccupiedHexNode{get;protected set;}
		public void SetOccupiedHexNode(HexNode hexNode){
			OccupiedHexNode = hexNode;
		}
	}
}


