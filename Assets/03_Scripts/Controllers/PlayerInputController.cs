using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : TopDownCharacterController
{
    private Camera _camera;

    //private void Awake()
    protected override void Awake()
    {
        //부모의 것을 먼저 실행하게
        base.Awake();
        _camera = Camera.main;
    }

    public void OnMove(InputValue value)
    {
   //     Debug.Log("Onmove"+value.ToString());
        Vector2 moveInput = value.Get<Vector2>().normalized;
        // normalized : 크기를 1인 단위벡터로 만들어서 방향값만 가지게
        // TopDownCharacterController에서 방향값을 인자로 받음

        CallMoveEvent(moveInput);

    }
    public void OnLook(InputValue value)
    {
     //  Debug.Log("OnLook" + value.ToString());
        Vector2 newAim = value.Get<Vector2>(); // 마우스 포지션
        Vector2 worldPos = _camera.ScreenToWorldPoint(newAim); // 마우스 위치를 월드좌표값으로 변경
        newAim = (worldPos - (Vector2)transform.position).normalized;
        // 마우스의 월드좌표값에서 내 위치값을 vector2값으로 변환한 좌표를 빼고 크기 1인 단위벡터로 만들기
        // 내 위치에서 마우스로 향하는 방향

        if (newAim.magnitude >= .9f) // magnitude : 크기, normalized했으니 1
        {
            CallLookEvent(newAim);
        }

    }
    //public void OnFire(InputValue value)
    public void OnFire(InputValue value)
    {
       // Debug.Log("OnFire" + value.ToString());

        //  IsAttacking = value.isPressed;
        IsAttacking = value.isPressed;


    }

    public void OnInteract(InputValue value)
    {
         Debug.Log("OnInteract" + value.ToString());
        Vector2 newAim = value.Get<Vector2>(); // 마우스 포지션
        Vector2 worldPos = _camera.ScreenToWorldPoint(newAim); // 마우스 위치를 월드좌표값으로 변경
                                                              //  newAim = (worldPos - (Vector2)transform.position).normalized;

        CallInteractEvent(newAim);
    }


}


// ========================================================


