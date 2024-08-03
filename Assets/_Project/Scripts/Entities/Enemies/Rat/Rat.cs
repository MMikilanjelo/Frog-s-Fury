using UnityEngine;
using Game.Components;
using Game.Abilities;
using Game.Utils.Helpers;
using Game.Managers;
using Game.Entities.Characters;
namespace Game.Entities.Enemies {
	public class Rat : Enemy {
		#region SerializeFields
		[SerializeField, Range(1, 4)] private int actionsCount_;

		#endregion
		private TurnActionCounterComponent turnActionCounterComponent_;
		private GridMovementComponent gridMovementComponent_;
		private AbilityManagerComponent abilityManagerComponent_;
		private HealthComponent healthComponent_;
		private void Awake() {
			gridMovementComponent_ = new GridMovementComponent(this);
			turnActionCounterComponent_ = new TurnActionCounterComponent(actionsCount_);
			abilityManagerComponent_ = GetComponent<AbilityManagerComponent>();
			healthComponent_ = GetComponent<HealthComponent>() ?? new HealthComponent();
			var moveAbEx = new MoveAbilityExecutionStrategy<TargetDataWithAdditionalTarget<Character>>(gridMovementComponent_);
			var moveAbSel = new RandomCharacterHexSelectionStrategy<TargetDataWithAdditionalTarget<Character>>(moveAbEx);
			var moveAbTg = new NearestWalkableHexAroundEntityFinder<Character>.Builder()
				.WithFraction(Fraction.CHARACTER)
				.WithSearchRange(3)
				.Build();
			var moveAb = new TargetSelectionAbilityStrategy<TargetDataWithAdditionalTarget<Character>>.Builder()
				.WithAbilityTargetSelectionStrategy(moveAbSel)
				.WithAbilityExecutionStrategy(moveAbEx)
				.WithAbilityTargetFinderStrategy(moveAbTg)
				.WithAbilityCost(1)
				.WithAbilityPerformer(this)
				.Build();

			var attackAbilityExecutionStrategy = new DealDamageAbilityExecutionStrategy.Builder()
				.WithDamage(5)
				.Build();
			var attackAbilityTargetFinderStrategy = new EntityInRangeFinder<IDamageable>.Builder()
				.WithFractionChecker(Fraction.CHARACTER)
				.Build();
			var attackAbility = new TargetSelectionAbilityStrategy<TargetDataWithAdditionalTarget<IDamageable>>.Builder()
				.WithAbilityCost(1)
				.WithAbilityPerformer(this)
				.WithAbilityExecutionStrategy(attackAbilityExecutionStrategy)
				.WithAbilityTargetFinderStrategy(attackAbilityTargetFinderStrategy)
				.WithAbilityTargetSelectionStrategy(new RandomCharacterHexSelectionStrategy<TargetDataWithAdditionalTarget<IDamageable>>(attackAbilityExecutionStrategy))
				.Build();
			Abilities.Add(AbilityTypes.RAT_MOVE_ABILITY, moveAb);
			Abilities.Add(AbilityTypes.RAT_ATTACK_ABILITY, attackAbility);

			abilityManagerComponent_.SetAbilities(Abilities);
			TurnManager.Instance.StartEnemyTurn += () => {
				turnActionCounterComponent_.ResetActions();
				abilityManagerComponent_.EnableAbilities();
			};


		}
		#region Ability Performer Implementation  

		public override int GetRemainingActions() => turnActionCounterComponent_.RemainingActions;
		public override bool CanPerformAbility(int actionCost) => turnActionCounterComponent_.CanPerformAction(actionCost);
		public override void PerformAbility(int abilityCost) => turnActionCounterComponent_.PerformAction(abilityCost);
		#endregion
		#region  IDamageable
		public override void TakeDamage(int damage) => healthComponent_.DealDamage(damage);
		#endregion
	}
}
