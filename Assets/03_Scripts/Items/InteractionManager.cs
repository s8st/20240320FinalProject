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
   // private Camera camera;


    // Start is called before the first frame update
    void Start()
    {
   //     camera = Camera.main; //main �±׸� ������ ī�޶� �Ѱ�, �̱��� ����ϵ��� �ϳ��� ����Ŵ

        _controller = GetComponent<TopDownCharacterController>();

     //   _controller.OnPickItemEvent += OnPickItem;

    }

    

    // Update is called once per frame
    void Update()
    {
        //if (Time.time - lastCheckTime > checkRate)
        //{
        //    lastCheckTime = Time.time;

        //    // Screen : ȭ��
        //    //ī�޶󿡼� ��ũ������Ʈ��, ������ ���� �ǵ��ƿ��� ������ �̿�
        //    // Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2)/*ȭ���� �߾�*/);

        //    //Ray ray = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y, -Camera.main.transform.position.z));

        //    Ray ray = GetComponent<Camera>().ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));

        //    RaycastHit hit; // �浹 ����

        //    // ref :��ȯ���� ���� ����  out : �ݵ�� ��ȯ���� ���� - null�̰ų� �ٸ� ������ ����
        //    if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
        //    {
        //        if (hit.collider.gameObject != curInteractGameobject)//������ �浹�� ��ü ������ ������ ��ü�� �ٸ��ٸ�
        //        {
        //            curInteractGameobject = hit.collider.gameObject; //�浹�� ��ü
        //            curInteractable = hit.collider.GetComponent<IInteractable>();
        //            Debug.Log(curInteractable);
        //             SetPromptText();
        //        }
        //    }
        //    else
        //    {
        //        //�浹�� ��ü�� ���ٸ� null�� �ʱ�ȭ
        //        curInteractGameobject = null;
        //        curInteractable = null;
        // //      promptText.gameObject.SetActive(false);
        //    }
        //}
    }

    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        // html�±� ó��, <b> == bold
        promptText.text = string.Format("<b>[E]</b> {0}", curInteractable.GetInteractPrompt());
    }

    //Player�� inspector���� Player Input������Ʈ�� events�Ʒ��� player�Ʒ��� interact�� �� ��ü Player�� �����ϰ� InteractionManager.OnInteractInput�����ϱ�
    //public void OnInteractInput(InputAction.CallbackContext callbackContext)
    //{
    //    if (callbackContext.phase == InputActionPhase.Started/*�� ���ȴ�*/ && curInteractable != null)
    //    // e�� �������� ���� ������ �ٶ󺸰� �ִ� �� �ִٸ�
    //    {
    //        //�������� ������ ��ȣ�ۿ��� �����ϰ� �ʱ�ȭ�ϰ� �Ⱥ��̰�
    //        curInteractable.OnInteract();
    //        curInteractGameobject = null;
    //        curInteractable = null;
    //        promptText.gameObject.SetActive(false);
    //    }
    //}



    public void OnInteractInput(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Started/*�� ���ȴ�*/ && curInteractable != null)
        // e�� �������� ���� ������ �ٶ󺸰� �ִ� �� �ִٸ�
        {
            //�������� ������ ��ȣ�ۿ��� �����ϰ� �ʱ�ȭ�ϰ� �Ⱥ��̰�
            curInteractable.OnInteract();
            curInteractGameobject = null;
            curInteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }



    //public void OnInteract(InputValue value)
    //{
    //    if (value.isPressed && curInteractable != null)
    //    // e�� �������� ���� ������ �ٶ󺸰� �ִ� �� �ִٸ�
    //    {
    //        //�������� ������ ��ȣ�ۿ��� �����ϰ� �ʱ�ȭ�ϰ� �Ⱥ��̰�
    //        curInteractable.OnInteract();
    //        curInteractGameobject = null;
    //        curInteractable = null;
    //        promptText.gameObject.SetActive(false);
    //    }
    //}

}