using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

public class ItemSlot
{
    public ItemData item;
    public int quantity;
}

public class Inventory : MonoBehaviour
{
    public ItemSlotUI[] uiSlots;
    public ItemSlot[] slots;

    public GameObject inventoryWindow;
    public Transform dropPosition;

    [Header("Selected Item")]
    private ItemSlot selectedItem;
    private int selectedItemIndex;
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;
    public TextMeshProUGUI selectedItemStatNames;
    public TextMeshProUGUI selectedItemStatValues;
    public GameObject useButton;
    public GameObject equipButton;
    public GameObject unEquipButton;
    public GameObject dropButton;

    private int curEquipIndex;

    private PlayerController controller;
    private PlayerConditions condition;

    [Header("Events")]
    public UnityEvent onOpenInventory;
    public UnityEvent onCloseInventory;


    private TopDownCharacterController _controllerTopDown;

    public static Inventory instance;
    void Awake()
    {
        instance = this;
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerConditions>();
        _controllerTopDown = GetComponent <TopDownCharacterController>();
    }
    private void Start()
    {
        inventoryWindow.SetActive(false);
        slots = new ItemSlot[uiSlots.Length];

        // uiSlots 초기화
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = new ItemSlot();
            uiSlots[i].index = i;
            uiSlots[i].Clear();
        }

        ClearSeletecItemWindow();
    }

    public void OnInventoryButton(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Started)/*막 눌려졌다면*/
        {
            Debug.Log("toggle");
            _controllerTopDown.CallOnInventoryButtonEvent(callbackContext);
            Toggle();

            
        }
    }

    //public void OnInventoryButton(InputAction value)
    //{
    //    if (value.phase == InputActionPhase.Started)/*막 눌려졌다면*/
    //    {
    //      //  Debug.Log("toggle");
    //        Toggle();
    //    }
    //}



    public void Toggle()
    {
        if (inventoryWindow.activeInHierarchy) //하이어라키에서 켜져있는지?
        {
            inventoryWindow.SetActive(false);
            onCloseInventory?.Invoke();
            controller.ToggleCursor(false); //커서 없애기, 인벤토리 꺼지면 커서도 꺼지게
        }
        else
        {
            inventoryWindow.SetActive(true);
            onOpenInventory?.Invoke();
            controller.ToggleCursor(true); // 인벤토리를 켰을때만 커서 사용
        }
    }

    public bool IsOpen()
    {
        return inventoryWindow.activeInHierarchy;
    }

    public void AddItem(ItemData item)
    {
        if (item.canStack)// 아이템이 쌓일 수 있나?
        {
            ItemSlot slotToStackTo = GetItemStack(item);
            if (slotToStackTo != null)
            {
                slotToStackTo.quantity++;
                UpdateUI();
                return;
            }
        }
        // 쌓을 수 있으면 쌓고 없으면 빈칸 찾아서
        ItemSlot emptySlot = GetEmptySlot();

        if (emptySlot != null)
        {
            emptySlot.item = item;
            emptySlot.quantity = 1;
            UpdateUI();
            return;
        }

        ThrowItem(item); // 꽉 찼을 경우
    }

    void ThrowItem(ItemData item)
    {
        Instantiate(item.dropPrefab, dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360f));
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
                uiSlots[i].Set(slots[i]); // 슬롯에 있는 데이터로 유아이슬롯 최신화
            else
                uiSlots[i].Clear();
        }
    }

    ItemSlot GetItemStack(ItemData item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            // 찾고 싶은 얘의 슬롯을 찾는 중??
            if (slots[i].item == item && slots[i].quantity < item.maxStackAmount)
                return slots[i];
        }

        return null;
    }

    // 빈 슬롯 찾기
    ItemSlot GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
                return slots[i];
        }

        return null;
    }

    public void SelectItem(int index)
    {
        if (slots[index].item == null)
            return;

        selectedItem = slots[index];
        selectedItemIndex = index;

        selectedItemName.text = selectedItem.item.displayName;
        selectedItemDescription.text = selectedItem.item.description;

        selectedItemStatNames.text = string.Empty;
        selectedItemStatValues.text = string.Empty;

        for (int i = 0; i < selectedItem.item.consumables.Length; i++)
        {
            selectedItemStatNames.text += selectedItem.item.consumables[i].type.ToString() + "\n";
            selectedItemStatValues.text += selectedItem.item.consumables[i].value.ToString() + "\n";
            // 능력치 숫자
            //health 20
            //hunger 50
        }

        useButton.SetActive(selectedItem.item.type == ItemType.Consumable);
        equipButton.SetActive(selectedItem.item.type == ItemType.Equipable && !uiSlots[index].equipped); // 아이템을 장착중이지 않는가?
        unEquipButton.SetActive(selectedItem.item.type == ItemType.Equipable && uiSlots[index].equipped); // 아이템을 장착중인가?
        dropButton.SetActive(true); // 항상 보이게
    }


    private void ClearSeletecItemWindow()
    {
        selectedItem = null;
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;

        selectedItemStatNames.text = string.Empty;
        selectedItemStatValues.text = string.Empty;

        useButton.SetActive(false);
        equipButton.SetActive(false);
        unEquipButton.SetActive(false);
        dropButton.SetActive(false);
    }

    //public void OnUseButton() //버튼 눌렀을때
    //{
    //    if (selectedItem.item.type == ItemType.Consumable) // 선택된 아이템의 타입이 소모할 수 있는 타입이라면
    //    {
    //        for (int i = 0; i < selectedItem.item.consumables.Length; i++)
    //        {
    //            switch (selectedItem.item.consumables[i].type)
    //            {
    //                case ConsumableType.Health:
    //                    condition.Heal(selectedItem.item.consumables[i].value); break; //헬쓰이라면 체력 값
    //                case ConsumableType.Hunger:
    //                    condition.Eat(selectedItem.item.consumables[i].value); break; // 헝거라면 헝거 값
    //            }
    //        }
    //    }
    //    RemoveSelectedItem(); // 사용한 아이템은 삭제
    //}

    public void OnEquipButton()
    {
        if (uiSlots[curEquipIndex].equipped) // 슬롯에 장착중인게 equipped이라면
        {
            UnEquip(curEquipIndex);
        }

        uiSlots[selectedItemIndex].equipped = true; // 새로 장착할 아이템을 true
        curEquipIndex = selectedItemIndex; //선택한 아이템으로 만들어주기
        EquipManager.instance.EquipNew(selectedItem.item); // EquipManager의 EquipNew
        UpdateUI();

        SelectItem(selectedItemIndex);
    }

    void UnEquip(int index)
    {
        uiSlots[index].equipped = false;
   //     EquipManager.instance.UnEquip();
        UpdateUI();

        if (selectedItemIndex == index) //선택한 아이템이 찾고 있는 거랑 같다면??
        {
            SelectItem(index);
        }

    }

    public void OnUnEquipButton()
    {
        UnEquip(selectedItemIndex);    //차고 있는 아이템을 해제하라

    }

    public void OnDropButton()
    {
        ThrowItem(selectedItem.item);
        RemoveSelectedItem();
    }

    private void RemoveSelectedItem()
    {
        selectedItem.quantity--; //선택한 아이템 수량 하나 깍기

        if (selectedItem.quantity <= 0)
        {
            if (uiSlots[selectedItemIndex].equipped)
            {
                //아이템을 제거 했으면 아이템을 해제
                UnEquip(selectedItemIndex);
            }

            selectedItem.item = null;
            ClearSeletecItemWindow();
        }

        UpdateUI();
    }

    public void RemoveItem(ItemData item)
    {

    }

    public bool HasItems(ItemData item, int quantity)
    {
        return false;
    }
}