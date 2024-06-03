using UnityEngine;

namespace Game.AbilitySystem {
	[CreateAssetMenu(fileName = "New Ability Data" , menuName = "AbilityData/NewAbilityData")]
	public class AbilityData : ScriptableObject {
		public Sprite Sprite;
		public string Name;
	}
}
