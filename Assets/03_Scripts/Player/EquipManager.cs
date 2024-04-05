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
    //    if (context.phase == InputActionPhase.Performed/*눌려지고있다*/ && curEquip != null && controller.canLook/*볼 수 있다면*/)
    //    {
    //        curEquip.OnAttackInput(conditions);
    //    }
    //}
    // EquipManager가 이벤트 받아와서  Equip을 주고 EquipTool이 상속받아서 처리



    // ItemData ->OnAttackInput ,equipPrefab


    public void EquipNew(ItemData item)
    {
        UnEquip();
        curEquip = Instantiate(item.equipPrefab, equipParent).GetComponent<Equip>();
    }

    public void UnEquip()
    {
        if (curEquip != null) // 장착중이라면
        {
            Destroy(curEquip.gameObject);
            curEquip = null;
        }
    }
}

