using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour, IInventoryItem
{
    public string Name 
    {
        get
        {
            return "Coin";
        }
    }


    public Sprite _Image;

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
