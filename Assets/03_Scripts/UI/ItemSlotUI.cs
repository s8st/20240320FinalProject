using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    //public Button button;
    //public Image icon;
    //public TextMeshProUGUI quatityText;
    //private ItemSlot curSlot;
    //private Outline outline;

    //public int index;
    //public bool equipped;

    //private void Awake()
    //{
    //    outline = GetComponent<Outline>();
    //}

    //private void OnEnable()
    //{
    //    // 아웃라인에 장착할 게 있다면 넣어준다, 아웃라인을 켜준다
    //    outline.enabled = equipped;
    //}

    //public void Set(ItemSlot slot)
    //{
    //    curSlot = slot;
    //    icon.gameObject.SetActive(true);
    //    icon.sprite = slot.item.icon;
    //    quatityText.text = slot.quantity > 1 ? slot.quantity.ToString() : string.Empty;

    //    if (outline != null)
    //    {
    //        outline.enabled = equipped;
    //    }
    //}

    //public void Clear()
    //{
    //    curSlot = null;
    //    icon.gameObject.SetActive(false);
    //    quatityText.text = string.Empty;
    //}

    //public void OnButtonClick()
    //{
    //    Inventory.instance.SelectItem(index);
    //}
}