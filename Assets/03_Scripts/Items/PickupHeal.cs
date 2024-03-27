using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 추상클래스를 상속받았기에 구현부를 만들어야 한다
public class PickupHeal : PickupItem
{
    [SerializeField] int healValue = 10;
    private HealthSystem _healthSystem;

    //힐을 더 회복하게 만들어 준다
    protected override void OnPickedUp(GameObject receiver)
    {
        _healthSystem = receiver.GetComponent<HealthSystem>();
        _healthSystem.ChangeHealth(healValue);
    }

}