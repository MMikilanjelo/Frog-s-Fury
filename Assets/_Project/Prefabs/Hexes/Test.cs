using UnityEngine;

using Game.Managers;
namespace Game {
	public class Test : MonoBehaviour {
		public void OnMouseDown(){
			Debug.Log(transform.position);	
			var tile = GridManager.Instance.GetHexFromWorldPosition(transform.position);
			Debug.Log(tile.WorldPosition);
		}
	}
}
