using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSwap : MonoBehaviour
{
    public Image characterUI;

    public Sprite[] characterSprite;


    void Update()
    {
        if(GameManager.Instance.charChoice)
        {
            characterUI.sprite = characterSprite[0];
        }

        else
        {
            characterUI.sprite = characterSprite[1];
        }
    }
}
