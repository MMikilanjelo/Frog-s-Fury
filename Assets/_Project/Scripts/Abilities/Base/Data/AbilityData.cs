using Game.Managers;
using UnityEngine;
namespace Game.Abilities {
	[CreateAssetMenu(fileName = "New Ability Data", menuName = "AbilityData/NewAbilityData")]
	public class AbilityData : ScriptableObject {
		[field: SerializeField] public Sprite Sprite { get; private set; }
		[field: SerializeField] public string Name { get; private set; }
		[field: SerializeField] public AbilityTypes Type { get; private set; }
	}
}