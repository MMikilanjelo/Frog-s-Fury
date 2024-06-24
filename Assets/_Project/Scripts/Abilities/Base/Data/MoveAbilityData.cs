using UnityEngine;

namespace Game.Abilities {
	[CreateAssetMenu(fileName = "New Move Ability Data", menuName = "AbilityData/NewMoveAbilityData")]
	public class MoveAbilityData : AbilityData {
		private const int MAX_ABILITY_RANGE = 30;
		private const int MIN_ABILITY_RANGE = 1;
		#region  SerializeFields
		[field: SerializeField, Range(MIN_ABILITY_RANGE, MAX_ABILITY_RANGE)] public int Range { get; private set; } = 1;
		#endregion
		private void OnValidate() {
			Range = Mathf.Clamp(Range, MIN_ABILITY_RANGE, MAX_ABILITY_RANGE);
		}
	}
}
