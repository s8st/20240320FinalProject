using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem; // 인풋시스템

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

    //private Rigidbody _rigidbody; // 컴포넌트에 rigidbody가 있다.

    //public static PlayerController instance; //싱글턴

    //private void Awake()
    //{
    //    instance = this;
    //    _rigidbody = GetComponent<Rigidbody>();
    //}

    //void Start()
    //{
    //    // Cursor.lockState = CursorLockMode.Locked; // 커서 안보이게 락= 안보이게 잠그겠다
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
    //    // 케릭터가 서 있는 상태의 포워드와 라이트???
    //    Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;

    //    dir *= moveSpeed;
    //    dir.y = _rigidbody.velocity.y; // y값을 없애기, 벨로시티의 y값 가져오기, 그 위치정보를 사용하기 위해?

    //    _rigidbody.velocity = dir;
    //}

    ////카메라 마우스로 움직이게
    //void CameraLook()
    //{
    //    // 마우스 위아래로 움직임 --> x축 움직임 
    //    camCurXRot += mouseDelta.y * lookSensitivity;
    //    camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook); // 값 안에서 가두기?
    //    cameraContainer.localEulerAngles/*각도*/ = new Vector3(-camCurXRot, 0, 0); // 마우스 상하의 움직임에 따라 움직이게


    //    transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    //}

    ////입력 받아
    //public void OnLookInput(InputAction.CallbackContext context)
    //{
    //    mouseDelta = context.ReadValue<Vector2>(); //context의 값을 Vector2로 읽어오기
    //}

    //public void OnMoveInput(InputAction.CallbackContext context)
    //{
    //    // Started : 맨 처음 한번 가져오고 그 다음부터는 반환되지 않는다
    //    // Performed : 눌려 졌을때
    //    // Canceled :떼어 졌을때
    //    if (context.phase/*상태*/ == InputActionPhase.Performed)
    //    {
    //        curMovementInput = context.ReadValue<Vector2>();
    //    }
    //    else if (context.phase == InputActionPhase.Canceled)
    //    {
    //        curMovementInput = Vector2.zero; // 움직이지 못하게
    //    }
    //}

    //public void OnJumpInput(InputAction.CallbackContext context)
    //{
    //    if (context.phase == InputActionPhase.Started)
    //    {
    //        if (IsGrounded()) // 땅을 밟고 있어야만 점프되게, 안그러면 무한 상승
    //            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);

    //    }
    //}

    //private bool IsGrounded() //땅을 밝고 있는지 체크
    //{
    //    Ray[] rays = new Ray[4]
    //    {
    //        // 케릭터의 동서남북으로 충돌여부 판단 
    //        //+ (Vector3.up * 0.01f) : 땅 아래로 가면 안되기 때문에 땅 위로 올려주기 위해
    //        new Ray(transform.position + (transform.forward * 0.2f) + (Vector3.up * 0.01f) , Vector3.down),
    //        new Ray(transform.position + (-transform.forward * 0.2f)+ (Vector3.up * 0.01f), Vector3.down),
    //        new Ray(transform.position + (transform.right * 0.2f) + (Vector3.up * 0.01f), Vector3.down),
    //        new Ray(transform.position + (-transform.right * 0.2f) + (Vector3.up * 0.01f), Vector3.down),
    //    };

    //    //배열이 나오면 무조건 반복문 사용
    //    // Length :메모리쪽에서 선형으로 이어진 경우  count : List처럼 드문 드문 들어간 경우
    //    for (int i = 0; i < rays.Length; i++)
    //    {
    //        // 어디에서 어디 방향으로
    //        if (Physics.Raycast(rays[i], 0.1f/*최대거리*/, groundLayerMask))
    //        {
    //            return true; // 4개 중에 하나라도 땅에 부딫히면 true반환, 아니라면 false반환
    //        }
    //    }

    //    return false;
    //}

    ////OnDrawGizmos : 항상 기즈모 보이게   OnDrawGizmoSelected : 선택했을때만 기즈모 보이게
    //private void OnDrawGizmos()
    //{
    //    // alt + drag = 마우스로 선택
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawRay(transform.position + (transform.forward * 0.2f), Vector3.down);
    //    Gizmos.DrawRay(transform.position + (-transform.forward * 0.2f), Vector3.down);
    //    Gizmos.DrawRay(transform.position + (transform.right * 0.2f), Vector3.down);
    //    Gizmos.DrawRay(transform.position + (-transform.right * 0.2f), Vector3.down);
    //}

    //public void ToggleCursor(bool toggle)
    //{
    //    Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
    //    canLook = !toggle; // 시야를 돌릴지 말지 결정
    //}
}