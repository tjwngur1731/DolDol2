using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSwap : MonoBehaviour
{
    private bool character;

    public Image characterUI;

    public Sprite[] characterSprite;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Character Swap");
            character = !character;
        }

        if (character)
        {
            characterUI.sprite = characterSprite[0];
        }
        else
        {
            characterUI.sprite = characterSprite[1];
        }
    }
}
