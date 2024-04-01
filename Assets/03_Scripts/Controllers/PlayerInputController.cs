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
        //�θ��� ���� ���� �����ϰ�
        base.Awake();
        _camera = Camera.main;
    }

    public void OnMove(InputValue value)
    {
   //     Debug.Log("Onmove"+value.ToString());
        Vector2 moveInput = value.Get<Vector2>().normalized;
        // normalized : ũ�⸦ 1�� �������ͷ� ���� ���Ⱚ�� ������
        // TopDownCharacterController���� ���Ⱚ�� ���ڷ� ����

        CallMoveEvent(moveInput);

    }
    public void OnLook(InputValue value)
    {
     //  Debug.Log("OnLook" + value.ToString());
        Vector2 newAim = value.Get<Vector2>(); // ���콺 ������
        Vector2 worldPos = _camera.ScreenToWorldPoint(newAim); // ���콺 ��ġ�� ������ǥ������ ����
        newAim = (worldPos - (Vector2)transform.position).normalized;
        // ���콺�� ������ǥ������ �� ��ġ���� vector2������ ��ȯ�� ��ǥ�� ���� ũ�� 1�� �������ͷ� �����
        // �� ��ġ���� ���콺�� ���ϴ� ����

        if (newAim.magnitude >= .9f) // magnitude : ũ��, normalized������ 1
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
        Vector2 newAim = value.Get<Vector2>(); // ���콺 ������
        Vector2 worldPos = _camera.ScreenToWorldPoint(newAim); // ���콺 ��ġ�� ������ǥ������ ����
                                                              //  newAim = (worldPos - (Vector2)transform.position).normalized;

        CallInteractEvent(newAim);
    }


}


// ========================================================


