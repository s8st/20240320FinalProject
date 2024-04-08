using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD2 : MonoBehaviour
{

    public Inventory2 Inventory2;

    // Start is called before the first frame update
    void Start()
    {
        Inventory2.ItemAdded += inventoryScript_ItemAdded;
    }

    private void inventoryScript_ItemAdded(object sender, InventoryEventArgs e)
    {
        Transform inventoryPanel = transform.Find("InventoryPanel");
        foreach(Transform slot in inventoryPanel)
        {
            // Border...Image
            Image image = slot.GetChild(0).GetChild(0).GetComponent<Image>();
            
            //we found the empty
            if(!image.enabled)
            {
                image.enabled = true;
                image.sprite = e.Item.Image;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
