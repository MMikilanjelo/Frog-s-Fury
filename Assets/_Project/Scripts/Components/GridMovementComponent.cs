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
		private float moveSpeed_ = 10.0f;
		private Coroutine moveCoroutine_;
		public event Action MovementFinished;
		public event Action MovementStarted;
		public GridMovementComponent(Entity entity) {
			entity_ = entity;
		}
		private void UpdateEntityAndTileInfo(HexNode tile) {
			entity_.OccupiedHexNode.SetOccupiedEntity(null);
			entity_.SetOccupiedHexNode(tile);
			entity_.OccupiedHexNode.SetOccupiedEntity(entity_);
		}
		public void Move(HexNode targetTile) {
			if (targetTile == null || !targetTile.Walkable()) {
				return;
			}
			List<HexNode> path = FindPath(entity_.OccupiedHexNode, targetTile);

			if (path != null) {
				UpdateEntityAndTileInfo(path.Last());
				MovementStarted?.Invoke();
				if (moveCoroutine_ != null) {
					entity_.StopCoroutine(moveCoroutine_);
				}
				moveCoroutine_ = entity_.StartCoroutine(MoveAlongPath(path));
			}
			else {
				MovementFinished?.Invoke();
			}
		}
		private IEnumerator MoveAlongPath(List<HexNode> path) {
			int startIndex = 0;
			while (startIndex < path.Count) {
				Vector3 targetPosition = path[startIndex].Position;
				while (entity_.transform.position != targetPosition) {
					entity_.transform.position = Vector3.MoveTowards(entity_.transform.position, targetPosition, moveSpeed_ * Time.deltaTime);
					yield return null;
				}
				startIndex++;
			}
			MovementFinished?.Invoke();
		}
		private List<HexNode> FindPath(HexNode current, HexNode target) {
			return PathFinding.FindPath(current, target);
		}
	}
}
