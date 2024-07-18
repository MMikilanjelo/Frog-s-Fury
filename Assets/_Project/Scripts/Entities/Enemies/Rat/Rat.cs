using UnityEngine;
using Game.Components;
using Game.Abilities;
using Game.Utils.Helpers;
using Game.Managers;
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
			healthComponent_.Died += () => UnitManager.Instance.DestroyEnemy(this);
			var moveAbEx = new MoveAbilityExecutionStrategy<TargetData<Entity>>(gridMovementComponent_);
			var moveAbSel = new RandomCharacterHexSelectionStrategy<TargetData<Entity>>();
			var moveAbTg = new NearestWalkableHexAroundCharacterFinder<TargetData<Entity>>.Builder()
				.WithFraction(Fraction.CHARACTER)
				.WithSearchRange(3)
				.Build();
			var moveAb = new TargetSelectionAbilityStrategy<TargetData<Entity>>.Builder()
				.WithAbilityTargetSelectionStrategy(moveAbSel)
				.WithAbilityExecutionStrategy(moveAbEx)
				.WithAbilityTargetFinderStrategy(moveAbTg)
				.WithAbilityCost(1)
				.WithAbilityPerformer(this)
				.Build();

			var attackAbilityExecutionStrategy = new DealDamageAbilityExecutionStrategy.Builder()
				.WithDamage(5)
				.Build();
			var attackAbilityTargetFinderStrategy = new EntityInRangeFinder<TargetData<IDamageable>>.Builder()
				.WithFractionChecker(Fraction.CHARACTER)
				.Build();
			var attackAbility = new TargetSelectionAbilityStrategy<TargetData<IDamageable>>.Builder()
				.WithAbilityCost(1)
				.WithAbilityPerformer(this)
				.WithAbilityExecutionStrategy(attackAbilityExecutionStrategy)
				.WithAbilityTargetFinderStrategy(attackAbilityTargetFinderStrategy)
				.WithAbilityTargetSelectionStrategy(new RandomCharacterHexSelectionStrategy<TargetData<IDamageable>>())
				.Build();
			Abilities.Add(AbilityTypes.RAT_MOVE_ABILITY, moveAb);
			Abilities.Add(AbilityTypes.RAT_ATTACK_ABILITY, attackAbility);

			abilityManagerComponent_.SetAbilities(Abilities);
			TurnManager.Instance.StartEnemyTurn += () => {
				turnActionCounterComponent_.ResetActions();
				abilityManagerComponent_.EnableAbilities();
			};


		}
		public override int GetRemainingActions() => turnActionCounterComponent_.RemainingActions;
		public override bool CanPerformAbility(int actionCost) => turnActionCounterComponent_.CanPerformAction(actionCost);
		public override void PerformAbility(int abilityCost) => turnActionCounterComponent_.PerformAction(abilityCost);

		public override void TakeDamage(int damage) => healthComponent_.DealDamage(damage);
	}
}
