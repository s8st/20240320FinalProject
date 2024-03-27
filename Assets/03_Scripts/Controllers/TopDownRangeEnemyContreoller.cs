using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 오타났는데 바꿔야하나?
public class TopDownRangeEnemyContreoller : TopDownEnemyController
{
    [SerializeField] private float followRange = 15f;
    [SerializeField] private float shootRange = 10f;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        float distance = DistanceToTarget();
        Vector2 direction = DirectionToTarget();

        IsAttacking = false;

        // 공격하는 거리인지 따라가는 거리인지 확인
        if (distance <= followRange)
        {
            if (distance <= shootRange)
            {
                int layerMaskTarget = Stats.CurrentStats.attackSO.target;
                // RaycastHit2D : 물리 충돌 검사
                // 나와 플레이어 사이에 막혀있는 지형이 있는지 검사
                RaycastHit2D hit =
                    Physics2D.Raycast
                    (transform.position, direction, 11f,
                    (1 << LayerMask.NameToLayer("Level")) | layerMaskTarget);

                if (hit.collider != null && layerMaskTarget == (layerMaskTarget | (1 << hit.collider.gameObject.layer)))
                {
                    CallLookEvent(direction); // 쳐다만 보게
                    CallMoveEvent(Vector2.zero); // 이동하지 않게 제자리에 멈추게
                    IsAttacking = true;
                }
                else
                {
                    CallMoveEvent(direction); // 다시 이동
                }
            }
            else
            {
                CallMoveEvent(direction);
            }
        }
        else
        {
            CallMoveEvent(direction);
        }
    }
}