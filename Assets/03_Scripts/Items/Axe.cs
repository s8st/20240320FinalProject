using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour,IInventoryItem
{

    public string Name
    {
        get
        {
            return "Item_Axe";
        }

    }

    public Sprite _Image = null;
    public Sprite Image
    {
        get
        {
            return _Image;
        }
    }
    public void OnPickup()
    {
        gameObject.SetActive(false);
    }



}
