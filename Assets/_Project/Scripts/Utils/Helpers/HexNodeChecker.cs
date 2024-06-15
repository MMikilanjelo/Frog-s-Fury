using System;
using Game.Hexagons;
using System.Collections.Generic;
namespace Game.Utils.Helpers {
	public static class HexNodeChecker {
		private static readonly Dictionary<HexNodeFlags, Func<Hex, bool>> flagCheckers_ = new Dictionary<HexNodeFlags, Func<Hex, bool>> {
			{ HexNodeFlags.Walkable, IsWalkable },
			{ HexNodeFlags.Occupied, IsOccupied },
			{ HexNodeFlags.Destroyable, IsDestroyable },
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
		private static bool IsOccupied(Hex hexNode) => hexNode.Occupied();
		private static bool IsDestroyable(Hex hexNode) => !hexNode.Occupied() && !hexNode.Walkable();
	}
	[Flags]
	public enum HexNodeFlags {
		None = 0,
		Walkable = 1,
		Occupied = 2,
		Destroyable = 4,
	}
}
