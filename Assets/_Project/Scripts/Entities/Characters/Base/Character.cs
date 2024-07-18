
namespace Game.Entities.Characters {
	public abstract class Character : AbilityPerformer {
		public CharacterData Data { get; protected set; }
		public void SetCharacterData(CharacterData data) => Data = data;
	}
}
