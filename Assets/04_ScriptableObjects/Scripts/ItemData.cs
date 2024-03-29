using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType // 아래 3가지가 선택형으로 나온다
{
    Resource,
    Equipable,
    Consumable
}

public enum ConsumableType // 아래 2가지가 선택형으로 나온다
{
    Hunger,
    Health
}

[System.Serializable]
public class ItemDataConsumable
{
    public ConsumableType type; // Hunger,Health 두가지가 선택형으로 나온다
    public float value;
    //public float value2; 
    // public float value3;
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")] //New Item이라는 메뉴가 생기고 만들면 아래 ItemData의 필드들이 인스펙터에 생김
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public ItemType type; // esource, Equipable, Consumable 총 3가지 선택형 역삼각형
    public Sprite icon; // 스프라이트 끼워넣을 곳
    public GameObject dropPrefab; //

    [Header("Stacking")]
    public bool canStack; // 체크형으로 만들어짐
    public int maxStackAmount;

    [Header("Consumable")]
    public ItemDataConsumable[] consumables;

    [Header("Equip")]
    public GameObject equipPrefab;
}





