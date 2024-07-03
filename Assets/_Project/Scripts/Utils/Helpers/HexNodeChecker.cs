using System;
using System.Collections.Generic;

using Game.Hexagons;
using Game.Entities.Characters;
using Game.Entities.Enemies;

namespace Game.Utils.Helpers {
	public static class HexNodeChecker {
		private static readonly Dictionary<HexNodeFlags, Func<Hex, bool>> flagCheckers_ = new Dictionary<HexNodeFlags, Func<Hex, bool>> {
			{ HexNodeFlags.WALKABLE, IsWalkable },
			{ HexNodeFlags.OCCUPIED_BY_CHARACTER, IsOccupiedByCharacter },
			{ HexNodeFlags.OCCUPIED_BY_ENEMY  ,IsOccupiedByEnemy},
			{ HexNodeFlags.DESTROYABLE, IsDestroyable },
		};
		public static bool HasFlags(Hex hexNode, HexNodeFlags flags) {
			foreach (var flag in flagCheckers_.Keys) {
				if (flags.HasFlag(flag) && !flagCheckers_[flag](hexNode)) {
					return false;
				}
			}
			return true;
		}

		private static bool IsWalkable(Hex hexNode) => hexNode.Walkable();
		private static bool IsOccupiedByCharacter(Hex hexNode) => hexNode.OccupiedEntity is Character;
		private static bool IsOccupiedByEnemy(Hex hexNode) => hexNode.OccupiedEntity is Enemy;
		private static bool IsDestroyable(Hex hexNode) => !hexNode.Occupied() && !hexNode.Walkable();
	}
	[Flags]
	public enum HexNodeFlags {
		None = 0,
		WALKABLE = 1,
		OCCUPIED_BY_CHARACTER = 2,
		OCCUPIED_BY_ENEMY = 3,
		DESTROYABLE = 4,
	}
}
