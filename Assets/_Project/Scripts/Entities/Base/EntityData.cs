using UnityEngine;

namespace Game.Entities {
	public abstract class EntityData : ScriptableObject {
		[SerializeField] public Entity Prefab;
		[SerializeField] public EntityTypes Type;
	}
}
