using UnityEngine;
using Game.Components;
using Game.Abilities;
using System.Collections.Generic;
using Game.Utils.Helpers;
namespace Game.Entities.Enemies {
	public class Rat : Enemy {
		#region SerializeFields
		[SerializeField, Range(1, 4)] private int actionsCount_;

		#endregion
		private TurnActionCounterComponent turnActionCounterComponent_;
		private GridMovementComponent gridMovementComponent_;
		private void Awake() {
			gridMovementComponent_ = new GridMovementComponent(this);
			turnActionCounterComponent_ = new TurnActionCounterComponent(actionsCount_);
			var moveAbEx = new MoveAbilityExecutionStrategy(gridMovementComponent_);
			var moveAbSel = new RandomCharacterHexSelectionStrategy();
			var moveAbTg = new NearestWalkableHexAroundCharacterFinder.Builder()
				.WithCharacterPriorityEvaluateFunction((EntityTypes type) => {
					return type == EntityTypes.FISH;
				})
				.WithSearchRange(4)
				.Build();
			var moveAb = new SingleTargetAbilityStrategy.Builder()
				.WithAbilityExecutionStrategy(moveAbEx)
				.WithAbilityTargetFinderStrategy(moveAbTg)
				.WithAbilityCost(1)
				.WithEntity(this)
				.Build();

			var attackAbilityExecutionStrategy = new DealDamageAbilityExecutionStrategy.Builder()
				.WithDamage(4)
				.Build();
			var attackAbilityTargetFinderStrategy = new EntityInRangeFinder.Builder()
				.WithAttackRange(5)
				.WithFlags(HexNodeFlags.OCCUPIED_BY_CHARACTER)
				.Build();
			var attackAbility = new TargetSelectionAbilityStrategy.Builder()
				.WithAbilityCost(1)
				.WithEntity(this)
				.WithAbilityExecutionStrategy(attackAbilityExecutionStrategy)
				.WithAbilityTargetFinderStrategy(attackAbilityTargetFinderStrategy)
				.WithAbilityTargetSelectionStrategy(new RandomCharacterHexSelectionStrategy())
				.Build();
			Abilities = new Dictionary<AbilityTypes, IAbilityStrategy>{
				{AbilityTypes.RAT_ATTACK_ABILITY, attackAbility},
				{AbilityTypes.RAT_MOVE_ABILITY, moveAb},
			};
		}
		public override int GetRemainingActions() => turnActionCounterComponent_.RemainingActions;
		public override bool CanPerformAbility(int actionCost) => turnActionCounterComponent_.CanPerformAction(actionCost);
		public override void PerformAbility(int abilityCost) => turnActionCounterComponent_.PerformAction(abilityCost);
	}
}
