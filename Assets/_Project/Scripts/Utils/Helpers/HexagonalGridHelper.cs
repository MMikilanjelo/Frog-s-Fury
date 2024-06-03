using System.Collections.Generic;
using UnityEngine;

namespace Game.Utils.Helpers {
	public static class HexagonalGridHelper {
		private static List<Vector3Int> directionsOffsetOdd_ = new List<Vector3Int>(){
			new Vector3Int(-1 , 1, 0),
			new Vector3Int(0 , 1 ,0),
			new Vector3Int(1 ,0 , 0),
			new Vector3Int(0 , -1 , 0),
			new Vector3Int(-1 ,-1 , 0),
			new Vector3Int(-1 , 0 , 0)
		};
		private static List<Vector3Int> directionsOffsetEvent_ = new List<Vector3Int>(){
			new Vector3Int(0, 1, 0),
			new Vector3Int(1, 1, 0),
			new Vector3Int(1, 0 , 0),
			new Vector3Int(1, -1, 0),
			new Vector3Int(0, -1, 0),
			new Vector3Int(-1 ,0, 0)
		};
		public static List<Vector3Int> GetDirectionsList(int y) => y % 2 == 0 ? directionsOffsetOdd_ : directionsOffsetEvent_;
	}
}
