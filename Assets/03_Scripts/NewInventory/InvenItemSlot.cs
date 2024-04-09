using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvenItemSlot : MonoBehaviour
{
    [SerializeField] private Image thumbnail;
    [SerializeField] private GameObject EquipObject;

    private InvenItem item;

    public void Init(InvenItem item)
    {
        this.item = item;

        thumbnail.sprite = item.Data.ItemSprite;
        EquipObject.SetActive(item.IsEquipped);
    }

    public void OnClickItem()
    {
        GameObject obj = InvenUIManager.Instance.Show("InvenPopupAlert");
        InvenPopupAlert popupAlert = obj.GetComponent<InvenPopupAlert>();

        popupAlert.Setting(item.IsEquipped ? "장착 해제 하시겠습니까??" : "장착 하시겠습니까??",
            () =>
            {
                item.IsEquipped = !item.IsEquipped;
                EquipObject.SetActive(item.IsEquipped);

                InvenManager.Instance.SaveUserData();

                InvenUIManager.Instance.Hide("InvenPopupAlert");
            },
            () =>
            {
                InvenUIManager.Instance.Hide("InvenPopupAlert");
            });





        //void OnClickConfirm()
        //{
        //    item.IsEquipped = !item.IsEquipped;
        //    EquipObject.SetActive(item.IsEquipped);

        //    GameManager.Instance.SaveUserData();

        //    UIManager.Instance.Hide("PopupAlert");
        //}

        //void OnClickCancel()
        //{
        //    UIManager.Instance.Hide("PopupAlert");
        //}
        //}
    }
}