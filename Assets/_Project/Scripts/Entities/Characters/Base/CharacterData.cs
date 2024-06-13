using System.Collections.Generic;
using Game.Systems.AbilitySystem;
using UnityEngine;

namespace Game.Entities.Characters {
	[CreateAssetMenu(fileName ="CharacterData" , menuName = "EntityData/CharacterData")]
	public class CharacterData : EntityData {
		[SerializeField] public CharacterTypes Type;
	}	
}
