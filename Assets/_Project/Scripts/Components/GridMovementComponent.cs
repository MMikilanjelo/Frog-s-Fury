using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;

using Game.Entities;
using Game.Hexagons;
using Game.Core.Logic;

using UnityEngine;

namespace Game.Components {
	public class GridMovementComponent {
		private Entity entity_;
		private float moveSpeed_ = 7.0f;
		private Coroutine moveCoroutine_;
		public event Action MovementFinished;
		public event Action MovementStarted;
		public bool TryFindPathToDestinationWithinDistance(Hex targetHex, out List<Hex> path, int movementRange) {
			if (targetHex == null || !targetHex.Walkable()) {
				path = null;
				return false;
			}
			path = FindPath(targetHex);
			return path != null && path.Count <= movementRange;
		}
		public GridMovementComponent(Entity entity) {
			entity_ = entity;
		}
		private void UpdateEntityAndTileInfo(Hex tile) {
			entity_.OccupiedHex.SetOccupiedEntity(null);
			entity_.SetOccupiedHex(tile);
			entity_.OccupiedHex.SetOccupiedEntity(entity_);
		}
		public void Move(List<Hex> path) {
			if (path != null && path.Count > 0) {
				UpdateEntityAndTileInfo(path.Last());
				MovementStarted?.Invoke();
				if (moveCoroutine_ != null) {
					entity_.StopCoroutine(moveCoroutine_);
				}
				moveCoroutine_ = entity_.StartCoroutine(MoveAlongPath(path));
			}
		}
		private IEnumerator MoveAlongPath(List<Hex> path) {
			int startIndex = 0;
			while (startIndex < path.Count) {
				Vector3 targetPosition = path[startIndex].WorldPosition;
				while (entity_.transform.position != targetPosition) {
					entity_.transform.position = Vector3.MoveTowards(entity_.transform.position, targetPosition, moveSpeed_ * Time.deltaTime);
					yield return null;
				}
				startIndex++;
			}
			MovementFinished?.Invoke();
		}
		public List<Hex> FindPath(Hex target) => PathFinding.FindPath(entity_.OccupiedHex, target);

	}
}
