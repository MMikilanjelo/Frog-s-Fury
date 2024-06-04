using UnityEngine;

namespace Game.Entities {
	public abstract class EntityData : ScriptableObject {
		[SerializeField] public Entity Prefab;
	}
}
