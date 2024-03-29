using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType // �Ʒ� 3������ ���������� ���´�
{
    Resource,
    Equipable,
    Consumable
}

public enum ConsumableType // �Ʒ� 2������ ���������� ���´�
{
    Hunger,
    Health
}

[System.Serializable]
public class ItemDataConsumable
{
    public ConsumableType type; // Hunger,Health �ΰ����� ���������� ���´�
    public float value;
    //public float value2; 
    // public float value3;
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")] //New Item�̶�� �޴��� ����� ����� �Ʒ� ItemData�� �ʵ���� �ν����Ϳ� ����
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public ItemType type; // esource, Equipable, Consumable �� 3���� ������ ���ﰢ��
    public Sprite icon; // ��������Ʈ �������� ��
    public GameObject dropPrefab; //

    [Header("Stacking")]
    public bool canStack; // üũ������ �������
    public int maxStackAmount;

    [Header("Consumable")]
    public ItemDataConsumable[] consumables;

    [Header("Equip")]
    public GameObject equipPrefab;
}





