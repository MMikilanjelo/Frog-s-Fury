using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Entities.Enemies {
	[CreateAssetMenu(fileName = "EnemyData" , menuName = "EntityData/EnemyData")]
	public class EnemyData : EntityData {
		[SerializeField] public EnemyTypes Type;
	}
}
