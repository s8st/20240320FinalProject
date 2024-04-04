using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

public interface IInteractable
{
    string GetInteractPrompt();
    void OnInteract();
}

public class InteractionManager : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask layerMask;

    private TopDownCharacterController _controller;

    private GameObject curInteractGameobject;
    private IInteractable curInteractable;

    public TextMeshProUGUI promptText;
    private Camera camera;


    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main; //main 태그를 가지는 카메라 한개, 싱글턴 사용하듯이 하나만 가르킴

        _controller = GetComponent<TopDownCharacterController>();

     //   _controller.OnPickItemEvent += OnPickItem;

    }

    

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;

            // Screen : 화면
            //카메라에서 스크린포인트로, 광선을 쏴서 되돌아오는 정보를 이용
            // Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2)/*화면의 중앙*/);

            //Ray ray = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y, -Camera.main.transform.position.z));

            Ray ray = camera.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));

            RaycastHit hit; // 충돌 정보

            // ref :반환값이 없을 수도  out : 반드시 반환값이 있음 - null이거나 다른 정보가 있음
            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                if (hit.collider.gameObject != curInteractGameobject)//광선에 충돌한 객체 정보와 저장한 객체와 다르다면
                {
                    curInteractGameobject = hit.collider.gameObject; //충돌한 객체
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                    Debug.Log(curInteractable);
                     SetPromptText();
                }
            }
            else
            {
                //충돌한 객체가 없다면 null로 초기화
                curInteractGameobject = null;
                curInteractable = null;
                promptText.gameObject.SetActive(false);
            }
        }
    }

    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        // html태그 처리, <b> == bold
        promptText.text = string.Format("<b>[E]</b> {0}", curInteractable.GetInteractPrompt());
    }

    //Player의 inspector에서 Player Input컴포넌트의 events아래에 player아래에 interact에 본 객체 Player를 연결하고 InteractionManager.OnInteractInput연결하기
    //public void OnInteractInput(InputAction.CallbackContext callbackContext)
    //{
    //    if (callbackContext.phase == InputActionPhase.Started/*막 눌렸다*/ && curInteractable != null)
    //    // e가 눌려졌고 눌린 시점에 바라보고 있는 게 있다면
    //    {
    //        //아이템을 먹으로 상호작용을 진행하고 초기화하고 안보이게
    //        curInteractable.OnInteract();
    //        curInteractGameobject = null;
    //        curInteractable = null;
    //        promptText.gameObject.SetActive(false);
    //    }
    //}

    // public void OnInteract(InputValue value)
    //{
    //    if (value.phase == InputActionPhase.Started/*막 눌렸다*/ && curInteractable != null)
    //    // e가 눌려졌고 눌린 시점에 바라보고 있는 게 있다면
    //    {
    //        //아이템을 먹으로 상호작용을 진행하고 초기화하고 안보이게
    //        curInteractable.OnInteract();
    //        curInteractGameobject = null;
    //        curInteractable = null;
    //        promptText.gameObject.SetActive(false);
    //    }


}