using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InvenPopupMain : InvenUIBase
{
    [Header("User Info")]
    [SerializeField] private TMP_Text userName;
    [SerializeField] private TMP_Text userLevel;
    [SerializeField] private TMP_Text userExp;
    [SerializeField] private Slider userExpSlider;

    [Header("User Stat")]
    [SerializeField] private TMP_Text userAtk;
    [SerializeField] private TMP_Text userDef;
    [SerializeField] private TMP_Text userStr;
    [SerializeField] private TMP_Text userCrt;

    [Header("User Inventory")]
    [SerializeField] private Transform content;
    [SerializeField] private GameObject itemSlot;

    private void Start()
    {
        Init();
        Refresh();
    }

    void Init()
    {
        foreach (InvenItem item in InvenManager.Instance.User.Inventory)
        {
            InvenItemSlot itemSlot = Instantiate(this.itemSlot, content).GetComponent<InvenItemSlot>();
            itemSlot.Init(item);
        }
    }

    public void Refresh()
    {
        UserData userData = InvenManager.Instance.User;

        userName.text = userData.Name;
        userLevel.text = $"LV <size=150%><b>{userData.Level}</b></size>";
        userExp.text = $"{userData.NowExp} / {userData.FullExp}";
        userExpSlider.value = (float)userData.NowExp / userData.FullExp;

        Status stat = userData.GetAllStat();

        userAtk.text = $"공격력\n<b>{stat.atk}</b>";
        userDef.text = $"방어력\n<b>{stat.def}</b>";
        userStr.text = $"체력\n<b>{stat.str}</b>";
        userCrt.text = $"치명타\n<b>{stat.crt}</b>";
    }
}
