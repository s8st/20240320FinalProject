using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_UI : MonoBehaviour
{
    public Image Ch_Image;
    // public GameObject Ch_Image;
    // public Text ID_Text;


    // Start is called before the first frame update
    void Start()
    {
        //ID_Text = 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlayerUI() 
    {
        //Ch_Image.sprite = this.GetComponent<SpriteRenderer>().sprite;
        Ch_Image.sprite = this.GetComponentInParent<SpriteRenderer>().sprite;
            
    }



}
