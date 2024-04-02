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

    private CharacterType characterType;
    

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


    }

    public void OnClickJoin()
    {
        if(!(2 <inputField.text.Length && inputField.text.Length < 10))
        {
            return;

        }

        //GameManager.instance.playerName.text = inputField.text;
        GameManager.instance.SetCharacter(characterType, inputField.text);

        Destroy(gameObject);


    }
}
