using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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


//    NullReferenceException: Object reference not set to an instance of an object
//HUD2.inventoryScript_ItemAdded(System.Object sender, InventoryEventArgs e) (at Assets/03_Scripts/UI/HUD2.cs:21)
//Inventory2.AddItem(IInventoryItem item) (at Assets/03_Scripts/Player/Inventory2.cs:26)
//PlayerInputController.OnTriggerEnter2D(UnityEngine.Collider2D collision) (at Assets/03_Scripts/Controllers/PlayerInputController.cs:30)


    private void inventoryScript_ItemAdded(object sender, InventoryEventArgs e)
    {
        //Transform inventoryPanel = transform.Find("InventoryPanel");
        
        foreach (Transform slot in Inventory2.transform)
        {
            // Border...Image
            Image image = slot.GetChild(0).GetChild(0).GetComponent<Image>();

            //we found the empty

            image.enabled = true;
            image.sprite = e.Item.Image;
            break;

            if (!image.enabled)
            {
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
