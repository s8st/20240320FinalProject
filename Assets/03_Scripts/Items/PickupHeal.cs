using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �߻�Ŭ������ ��ӹ޾ұ⿡ �����θ� ������ �Ѵ�
public class PickupHeal : PickupItem
{
    [SerializeField] int healValue = 10;
    private HealthSystem _healthSystem;

    //���� �� ȸ���ϰ� ����� �ش�
    protected override void OnPickedUp(GameObject receiver)
    {
        _healthSystem = receiver.GetComponent<HealthSystem>();
        _healthSystem.ChangeHealth(healValue);
    }

}