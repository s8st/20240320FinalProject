using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputController : TopDownCharacterController
{
    private Camera _camera;

    private Vector2 joystickValue = Vector2.zero;

    //private void Awake()
    protected override void Awake()
    {
        //부모의 것을 먼저 실행하게
        base.Awake();
        _camera = Camera.main;
    }

    protected override void Update()
    {
#if UNITY_ANDROID
        if (Input.GetMouseButtonDown(0))
        {
            IsAttacking = true;
            Vector2 worldPos = _camera.ScreenToWorldPoint(Input.mousePosition); // 마우스 위치를 월드좌표값으로 변경
            Vector2 newAim = (worldPos - (Vector2)transform.position).normalized;
            Debug.Log(newAim);
            // 마우스의 월드좌표값에서 내 위치값을 vector2값으로 변환한 좌표를 빼고 크기 1인 단위벡터로 만들기
            // 내 위치에서 마우스로 향하는 방향

            if (newAim.magnitude >= .9f) // magnitude : 크기, normalized했으니 1
            {
                CallLookEvent(newAim);
            }
        }
#endif
        base.Update();
    }

    private void FixedUpdate()
    {
#if UNITY_ANDROID
        joystickValue.x = GameManager.instance.joystick.Horizontal;
        joystickValue.y = GameManager.instance.joystick.Vertical;
        CallMoveEvent(joystickValue);
#endif
    }

    public void OnMove(InputValue value)
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        // Debug.Log("Onmove"+value.ToString());
        Vector2 moveInput = value.Get<Vector2>().normalized;
        // normalized : 크기를 1인 단위벡터로 만들어서 방향값만 가지게
        // TopDownCharacterController에서 방향값을 인자로 받음

        CallMoveEvent(moveInput);
#endif
    }
    public void OnLook(InputValue value)
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN

        // Debug.Log("OnLook" + value.ToString());
        Vector2 newAim = value.Get<Vector2>(); // 마우스 포지션
        Vector2 worldPos = _camera.ScreenToWorldPoint(newAim); // 마우스 위치를 월드좌표값으로 변경
        newAim = (worldPos - (Vector2)transform.position).normalized;
        // 마우스의 월드좌표값에서 내 위치값을 vector2값으로 변환한 좌표를 빼고 크기 1인 단위벡터로 만들기
        // 내 위치에서 마우스로 향하는 방향

        if (newAim.magnitude >= .9f) // magnitude : 크기, normalized했으니 1
        {
            CallLookEvent(newAim);
        }
#endif
    }
    //public void OnFire(InputValue value)
    public void OnFire(InputValue value)
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN

        // Debug.Log("OnFire" + value.ToString());

        //  IsAttacking = value.isPressed;
        IsAttacking = value.isPressed;
#endif
    }

    //public void OnInteract(InputValue value)
    //{
    //    //Debug.Log("OnInteract" + value.ToString());

    //    Debug.Log("OnInteract_플레이어인풋컨트롤러.cs" + value.ToString());
    //    Vector2 newAim = value.Get<Vector2>(); // 마우스 포지션
    //    Vector2 worldPos = _camera.ScreenToWorldPoint(newAim); // 마우스 위치를 월드좌표값으로 변경
    //                                                          //  newAim = (worldPos - (Vector2)transform.position).normalized;

    //    CallInteractEvent(newAim);
    //}

    //public void OnPickItem(InputValue value)
    //{
    //    //  IsAttacking = value.isPressed;
    //    CallPickItemEvent(value);
    //}

     public void  OnInventory(InputValue value)
    {
    //    CallOnInventoryEvent(value);
    }

}


// ========================================================


