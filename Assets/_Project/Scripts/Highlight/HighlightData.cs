using Game.Managers;
using UnityEngine;

namespace Game.Highlight {
	[CreateAssetMenu(fileName = "New Highlight Data" , menuName = "Highlight/HighlightData")]
	public class HighlightData : ScriptableObject{
		public HighlightType Type;
		public GameObject GameObject;	
	}
}
