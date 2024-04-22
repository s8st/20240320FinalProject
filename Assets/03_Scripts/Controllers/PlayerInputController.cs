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
        //�θ��� ���� ���� �����ϰ�
        base.Awake();
        _camera = Camera.main;
    }

    protected override void Update()
    {
#if UNITY_ANDROID
        if (Input.GetMouseButtonDown(0))
        {
            IsAttacking = true;
            Vector2 worldPos = _camera.ScreenToWorldPoint(Input.mousePosition); // ���콺 ��ġ�� ������ǥ������ ����
            Vector2 newAim = (worldPos - (Vector2)transform.position).normalized;
            Debug.Log(newAim);
            // ���콺�� ������ǥ������ �� ��ġ���� vector2������ ��ȯ�� ��ǥ�� ���� ũ�� 1�� �������ͷ� �����
            // �� ��ġ���� ���콺�� ���ϴ� ����

            if (newAim.magnitude >= .9f) // magnitude : ũ��, normalized������ 1
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
        // normalized : ũ�⸦ 1�� �������ͷ� ���� ���Ⱚ�� ������
        // TopDownCharacterController���� ���Ⱚ�� ���ڷ� ����

        CallMoveEvent(moveInput);
#endif
    }
    public void OnLook(InputValue value)
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN

        // Debug.Log("OnLook" + value.ToString());
        Vector2 newAim = value.Get<Vector2>(); // ���콺 ������
        Vector2 worldPos = _camera.ScreenToWorldPoint(newAim); // ���콺 ��ġ�� ������ǥ������ ����
        newAim = (worldPos - (Vector2)transform.position).normalized;
        // ���콺�� ������ǥ������ �� ��ġ���� vector2������ ��ȯ�� ��ǥ�� ���� ũ�� 1�� �������ͷ� �����
        // �� ��ġ���� ���콺�� ���ϴ� ����

        if (newAim.magnitude >= .9f) // magnitude : ũ��, normalized������ 1
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

    //    Debug.Log("OnInteract_�÷��̾���ǲ��Ʈ�ѷ�.cs" + value.ToString());
    //    Vector2 newAim = value.Get<Vector2>(); // ���콺 ������
    //    Vector2 worldPos = _camera.ScreenToWorldPoint(newAim); // ���콺 ��ġ�� ������ǥ������ ����
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


