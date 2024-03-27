using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��Ÿ���µ� �ٲ���ϳ�?
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

        // �����ϴ� �Ÿ����� ���󰡴� �Ÿ����� Ȯ��
        if (distance <= followRange)
        {
            if (distance <= shootRange)
            {
                int layerMaskTarget = Stats.CurrentStats.attackSO.target;
                // RaycastHit2D : ���� �浹 �˻�
                // ���� �÷��̾� ���̿� �����ִ� ������ �ִ��� �˻�
                RaycastHit2D hit =
                    Physics2D.Raycast
                    (transform.position, direction, 11f,
                    (1 << LayerMask.NameToLayer("Level")) | layerMaskTarget);

                if (hit.collider != null && layerMaskTarget == (layerMaskTarget | (1 << hit.collider.gameObject.layer)))
                {
                    CallLookEvent(direction); // �Ĵٸ� ����
                    CallMoveEvent(Vector2.zero); // �̵����� �ʰ� ���ڸ��� ���߰�
                    IsAttacking = true;
                }
                else
                {
                    CallMoveEvent(direction); // �ٽ� �̵�
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