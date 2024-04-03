using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem; // ��ǲ�ý���

public class PlayerController : MonoBehaviour
{
    //[Header("Movement")]
    //public float moveSpeed;
    //private Vector2 curMovementInput;
    //public float jumpForce;
    //public LayerMask groundLayerMask;

    //[Header("Look")]
    //public Transform cameraContainer;
    //public float minXLook;
    //public float maxXLook;
    //private float camCurXRot;
    //public float lookSensitivity;

    //private Vector2 mouseDelta;

    //[HideInInspector]
    //public bool canLook = true;

    //private Rigidbody _rigidbody; // ������Ʈ�� rigidbody�� �ִ�.

    //public static PlayerController instance; //�̱���

    //private void Awake()
    //{
    //    instance = this;
    //    _rigidbody = GetComponent<Rigidbody>();
    //}

    //void Start()
    //{
    //    // Cursor.lockState = CursorLockMode.Locked; // Ŀ�� �Ⱥ��̰� ��= �Ⱥ��̰� ��װڴ�
    //}

    //private void FixedUpdate()
    //{
    //    Move();
    //}

    //private void LateUpdate()
    //{
    //    if (canLook)
    //    {
    //        CameraLook();
    //    }
    //}

    //private void Move()
    //{
    //    // �ɸ��Ͱ� �� �ִ� ������ ������� ����Ʈ???
    //    Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;

    //    dir *= moveSpeed;
    //    dir.y = _rigidbody.velocity.y; // y���� ���ֱ�, ���ν�Ƽ�� y�� ��������, �� ��ġ������ ����ϱ� ����?

    //    _rigidbody.velocity = dir;
    //}

    ////ī�޶� ���콺�� �����̰�
    //void CameraLook()
    //{
    //    // ���콺 ���Ʒ��� ������ --> x�� ������ 
    //    camCurXRot += mouseDelta.y * lookSensitivity;
    //    camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook); // �� �ȿ��� ���α�?
    //    cameraContainer.localEulerAngles/*����*/ = new Vector3(-camCurXRot, 0, 0); // ���콺 ������ �����ӿ� ���� �����̰�


    //    transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    //}

    ////�Է� �޾�
    //public void OnLookInput(InputAction.CallbackContext context)
    //{
    //    mouseDelta = context.ReadValue<Vector2>(); //context�� ���� Vector2�� �о����
    //}

    //public void OnMoveInput(InputAction.CallbackContext context)
    //{
    //    // Started : �� ó�� �ѹ� �������� �� �������ʹ� ��ȯ���� �ʴ´�
    //    // Performed : ���� ������
    //    // Canceled :���� ������
    //    if (context.phase/*����*/ == InputActionPhase.Performed)
    //    {
    //        curMovementInput = context.ReadValue<Vector2>();
    //    }
    //    else if (context.phase == InputActionPhase.Canceled)
    //    {
    //        curMovementInput = Vector2.zero; // �������� ���ϰ�
    //    }
    //}

    //public void OnJumpInput(InputAction.CallbackContext context)
    //{
    //    if (context.phase == InputActionPhase.Started)
    //    {
    //        if (IsGrounded()) // ���� ��� �־�߸� �����ǰ�, �ȱ׷��� ���� ���
    //            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);

    //    }
    //}

    //private bool IsGrounded() //���� ��� �ִ��� üũ
    //{
    //    Ray[] rays = new Ray[4]
    //    {
    //        // �ɸ����� ������������ �浹���� �Ǵ� 
    //        //+ (Vector3.up * 0.01f) : �� �Ʒ��� ���� �ȵǱ� ������ �� ���� �÷��ֱ� ����
    //        new Ray(transform.position + (transform.forward * 0.2f) + (Vector3.up * 0.01f) , Vector3.down),
    //        new Ray(transform.position + (-transform.forward * 0.2f)+ (Vector3.up * 0.01f), Vector3.down),
    //        new Ray(transform.position + (transform.right * 0.2f) + (Vector3.up * 0.01f), Vector3.down),
    //        new Ray(transform.position + (-transform.right * 0.2f) + (Vector3.up * 0.01f), Vector3.down),
    //    };

    //    //�迭�� ������ ������ �ݺ��� ���
    //    // Length :�޸��ʿ��� �������� �̾��� ���  count : Listó�� �幮 �幮 �� ���
    //    for (int i = 0; i < rays.Length; i++)
    //    {
    //        // ��𿡼� ��� ��������
    //        if (Physics.Raycast(rays[i], 0.1f/*�ִ�Ÿ�*/, groundLayerMask))
    //        {
    //            return true; // 4�� �߿� �ϳ��� ���� �΋H���� true��ȯ, �ƴ϶�� false��ȯ
    //        }
    //    }

    //    return false;
    //}

    ////OnDrawGizmos : �׻� ����� ���̰�   OnDrawGizmoSelected : ������������ ����� ���̰�
    //private void OnDrawGizmos()
    //{
    //    // alt + drag = ���콺�� ����
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawRay(transform.position + (transform.forward * 0.2f), Vector3.down);
    //    Gizmos.DrawRay(transform.position + (-transform.forward * 0.2f), Vector3.down);
    //    Gizmos.DrawRay(transform.position + (transform.right * 0.2f), Vector3.down);
    //    Gizmos.DrawRay(transform.position + (-transform.right * 0.2f), Vector3.down);
    //}

    //public void ToggleCursor(bool toggle)
    //{
    //    Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
    //    canLook = !toggle; // �þ߸� ������ ���� ����
    //}
}