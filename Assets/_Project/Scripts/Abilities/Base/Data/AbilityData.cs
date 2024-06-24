using Game.Managers;
using UnityEngine;
namespace Game.Abilities {
	[CreateAssetMenu(fileName = "New Ability Data", menuName = "AbilityData/NewAbilityData")]
	public class AbilityData : ScriptableObject {
		private const int MAX_ABILITY_COST = 4;
		private const int MIN_ABILITY_COST = 1;
		[field: SerializeField] public Sprite Sprite { get; private set; }
		[field: SerializeField] public string Name { get; private set; }
		[field: SerializeField] public AbilityTypes Type { get; private set; }
		[field: SerializeField] public HighlightType HighlightType { get; private set; }
		[field: SerializeField, Range(MIN_ABILITY_COST, MAX_ABILITY_COST)] public int Cost { get; private set; } = 1;
		private void OnValidate(){
			Cost = Mathf.Clamp(Cost ,MIN_ABILITY_COST, MAX_ABILITY_COST);
		}
	}
}