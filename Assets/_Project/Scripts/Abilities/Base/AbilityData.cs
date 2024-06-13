using UnityEngine;

namespace Game.Abilities {
	[CreateAssetMenu(fileName = "New Ability Data" , menuName = "AbilityData/NewAbilityData")]
	public class AbilityData : ScriptableObject {
		public Sprite Sprite;
		public string Name;
		public AbilityTypes Type;
		public int AbilityCost;
	}
}
