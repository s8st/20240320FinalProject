using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupStartMenu : MonoBehaviour
{
    [SerializeField] private Image characterSprite;

    //[SerializeField] private TextMeshProUGUI inputField;
    //[SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private InputField inputField;
    //[SerializeField] private Text playerName;
    [SerializeField] private GameObject information;
    [SerializeField] private GameObject selectCharacter;


    //[SerializeField] private GameObject player0;
    //[SerializeField] private GameObject player1;
    //[SerializeField] private GameObject player2;
    //[SerializeField] private GameObject player3;

    //public  List<GameObject> playerPrefabs = new List<GameObject>();


    private CharacterType characterType;
    //private PlayerType playerType;
    

    public void OnClickCharacter()
    {
        information.SetActive(false);
        selectCharacter.SetActive(true);
    }

    public void OnClickSelectCharacter(int index)
    {
        characterType = (CharacterType)index;
        var character = GameManager.instance.CharacterList.Find(item => item.characterType == characterType);

        characterSprite.sprite = character.CharacterSprite;
        characterSprite.SetNativeSize();

        selectCharacter.SetActive(false);
        information.SetActive(true);

        //GameObject player = Instantiate(playerPrefabs[index], transform.position, Quaternion.identity);
        //player.SetActive(true);


        //if (index == 0)
        //{
        //    player0.SetActive(true);
        //}
        //if (index == 1)
        //{
        //    player1.SetActive(true);
        //}
        //if (index == 2)
        //{
        //    player2.SetActive(true);
        //}
        //if (index == 3)
        //{
        //    player3.SetActive(true);
        //}

    }


    //public void OnClickSelectCharacter(int index)
    //{
    //    playerType = (PlayerType)index;
    //    var character = GameManager.instance.CharacterList.Find(item => item.playerType == playerType);

    //    characterSprite.sprite = character.CharacterSprite;
    //    characterSprite.SetNativeSize();

    //    selectCharacter.SetActive(false);
    //    information.SetActive(true);



    //}











    public void OnClickJoin()
    {
        if(!(2 <inputField.text.Length && inputField.text.Length < 10))
        {
            return;

        }

        //GameManager.instance.playerName.text = inputField.text;
        
        GameManager.instance.SetCharacter(characterType, inputField.text); // 1.인자를 입력받으면 게임메니저에서 실행 ---> gamemanager


        //GameManager.instance.SetCharacter(playerType, inputField.text);


        //GameObject player = Instantiate(playerPrefabs[index], transform.position, Quaternion.identity);
        //player.SetActive(true);


        Destroy(gameObject);


    }
}
