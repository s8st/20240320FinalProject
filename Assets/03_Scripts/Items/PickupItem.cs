using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 추상클래스 : 생성을 하지 않는다?? 상속받는 곳에서
public abstract class PickupItem : MonoBehaviour
{
    [SerializeField] private bool destroyOnPickup = true; //집으면 삭제여부
    [SerializeField] private LayerMask canBePickupBy; //먹을 수 있는지?
    [SerializeField] private AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 먹을 수 있는 아이템인지 검사
        if (canBePickupBy.value == (canBePickupBy.value | (1 << other.gameObject.layer)))
        {
            OnPickedUp(other.gameObject); // 픽업하고 나서 소리내고 삭제
            if (pickupSound)
                SoundManager.PlayClip(pickupSound);

            if (destroyOnPickup)
            {
                Destroy(gameObject);
            }
        }
    }

    // 상속받는 곳에서 반드시 구현
    //아이템을 먹을때 균일한 동작을 하지 않기에 각각 다른 동작을 받을 수 있게
    protected abstract void OnPickedUp(GameObject receiver);
}