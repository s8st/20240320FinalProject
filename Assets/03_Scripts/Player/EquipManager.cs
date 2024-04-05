using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class EquipManager : MonoBehaviour
{
    public Equip curEquip;
    public Transform equipParent;

    private PlayerController controller;
    private PlayerConditions conditions;

    // singleton
    public static EquipManager instance;

    private void Awake()
    {
        instance = this;
        controller = GetComponent<PlayerController>();
        conditions = GetComponent<PlayerConditions>();
    }

    //public void OnAttackInput(InputAction.CallbackContext context)
    //{
    //    if (context.phase == InputActionPhase.Performed/*���������ִ�*/ && curEquip != null && controller.canLook/*�� �� �ִٸ�*/)
    //    {
    //        curEquip.OnAttackInput(conditions);
    //    }
    //}
    // EquipManager�� �̺�Ʈ �޾ƿͼ�  Equip�� �ְ� EquipTool�� ��ӹ޾Ƽ� ó��



    // ItemData ->OnAttackInput ,equipPrefab


    public void EquipNew(ItemData item)
    {
        UnEquip();
        curEquip = Instantiate(item.equipPrefab, equipParent).GetComponent<Equip>();
    }

    public void UnEquip()
    {
        if (curEquip != null) // �������̶��
        {
            Destroy(curEquip.gameObject);
            curEquip = null;
        }
    }
}

