
using Game.Abilities;
using Game.Components;
using Game.Managers;
using Game.Utils.Helpers;
using UnityEngine;

namespace Game.Entities.Characters {
	public class Fish : Character {
		#region SerializeFields
		[SerializeField, Range(1, 4)] private int actionsCount_;

		#endregion
		#region Components
		private TurnActionCounterComponent turnActionCounterComponent_;
		private GridMovementComponent gridMovementComponent_;
		private AbilityManagerComponent abilityManagerComponent_;
		private HealthComponent healthComponent_;
		#endregion
		private void Awake() {
			gridMovementComponent_ = new GridMovementComponent(this);
			turnActionCounterComponent_ = new TurnActionCounterComponent(actionsCount_);
			abilityManagerComponent_ = GetComponent<AbilityManagerComponent>();
			healthComponent_ = GetComponent<HealthComponent>();
			healthComponent_.Died += () => UnitManager.Instance.DestroyCharacter(this);
			var moveAbilityStrategy = new MoveAbilityExecutionStrategy<HexTargetData>(gridMovementComponent_);

			var moveAbilitySelectionStrategy = new HexMouseSelectionStrategy<HexTargetData>.Builder()
				.WithCallbackSelectionResponseDecorator()
				.WithHighLightType(HighlightType.OUTLINE)
				.Build();

			var moveAbilityTargetFinderStrategy = new PathDistanceWalkableHexFinder.Builder()
				.WithSearchRange(5)
				.Build();

			var moveAbility = new TargetSelectionAbilityStrategy<HexTargetData>.Builder()
				.WithAbilityExecutionStrategy(moveAbilityStrategy)
				.WithAbilityTargetFinderStrategy(moveAbilityTargetFinderStrategy)
				.WithAbilityTargetSelectionStrategy(moveAbilitySelectionStrategy)
				.WithAbilityPerformer(this)
				.WithAbilityCost(1)
				.Build();
			
			var attackAbilitySelectionStrategy = new HexMouseSelectionStrategy<TargetData<IDamageable>>.Builder()
				.WithCallbackSelectionResponseDecorator()
				.WithHighLightType(HighlightType.ATTACK)
				.Build();
			var attackAbilityTargetFinderStrategy = new NearestWalkableHexAroundCharacterFinder<TargetData<IDamageable>>.Builder()
				.WithFraction(Fraction.ENEMY)
				.WithSearchRange(5)
				.Build();
			var moveAndAttack = new MoveAndAttackAbilityExecutionStrategy(gridMovementComponent_);
			var attackAbility = new TargetSelectionAbilityStrategy<TargetData<IDamageable>>.Builder()
				.WithAbilityExecutionStrategy(moveAndAttack)
				.WithAbilityTargetFinderStrategy(attackAbilityTargetFinderStrategy)
				.WithAbilityTargetSelectionStrategy(attackAbilitySelectionStrategy)
				.WithAbilityPerformer(this)
				.WithAbilityCost(1)
				.Build();
			Abilities.Add(AbilityTypes.FISH_MOVE_ABILITY, moveAbility);
			Abilities.Add(AbilityTypes.FISH_ATTACK_ABILITY, attackAbility);

			abilityManagerComponent_.SetAbilities(Abilities);
			TurnManager.Instance.StartPlayerTurn += () => {
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

