using System;
using System.Collections.Generic;

using Game.Hexagons;
using Game.Entities.Characters;
using Game.Entities.Enemies;


namespace Game.Utils.Helpers {
	public static class HexNodeChecker {
		private static readonly Dictionary<HexNodeFlags, Func<Hex, bool>> flagCheckers_ = new Dictionary<HexNodeFlags, Func<Hex, bool>> {
			{ HexNodeFlags.WALKABLE, IsWalkable },
		 { HexNodeFlags.DESTROYABLE, IsDestroyable },
		};
		private static readonly Dictionary<Fraction, Func<Hex, bool>> fractionCheckers_ = new Dictionary<Fraction, Func<Hex, bool>> {
			{ Fraction.ENEMY, IsOccupiedByEnemy },
			{ Fraction.CHARACTER ,IsOccupiedByCharacter},
		};
		public static bool HasFlags(Hex hexNode, HexNodeFlags flags) {
			foreach (var flag in flagCheckers_.Keys) {
				if (flags.HasFlag(flag) && !flagCheckers_[flag](hexNode)) {
					return false;
				}
			}
			return true;
		}
		public static bool HasFraction(Hex hex, Fraction fraction) {
			if (fractionCheckers_.TryGetValue(fraction, out var checker)) {
				return checker(hex);
			}
			return false;
		}

		private static bool IsWalkable(Hex hexNode) => hexNode.Walkable();
		private static bool IsOccupiedByCharacter(Hex hexNode) => hexNode.OccupiedEntity is Character;
		private static bool IsOccupiedByEnemy(Hex hexNode) => hexNode.OccupiedEntity is Enemy;

		private static bool IsDestroyable(Hex hexNode) => !hexNode.Occupied() && !hexNode.Walkable();
	}
	[Flags]
	public enum HexNodeFlags {
		None = 0,
		WALKABLE = 1 << 0, // 1
		DESTROYABLE = 1 << 1, // 8
	}
	public enum Fraction {
		NONE,
		ENEMY,
		CHARACTER
	}
}
