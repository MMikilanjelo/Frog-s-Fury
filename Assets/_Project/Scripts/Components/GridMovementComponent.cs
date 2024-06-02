using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Entities;
using System;
using Game.Hexagons;
using System.Linq;
using Game.Core.Logic;
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
			entity_.OccupiedHexTile.SetOccupiedEntity(null);
			entity_.SetOccupiedHexTile(tile);
			entity_.OccupiedHexTile.SetOccupiedEntity(entity_);
		}
		public void Move(HexNode targetTile) {
			if (targetTile == null || !targetTile.Walkable()) {
				return;
			}
			List<HexNode> path = FindPath(entity_.OccupiedHexTile, targetTile);

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
