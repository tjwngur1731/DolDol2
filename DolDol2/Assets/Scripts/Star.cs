using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Star : MonoBehaviour
{
    public Sprite starOn;
    public Sprite starOff;

    public Image[] star;

    public bool[] starCount;

    void Start()
    {
        starCount = new bool[3];   
    }

    void Update()
    {
        for (int i = 0; i < starCount.Length;i++)
        {
            if(starCount[i])
            {
                star[i].sprite = starOn;
            }
            else
            {
                star[i].sprite = starOff;
            }
        }
    }

    void StarUp(int cnt)
    {
        starCount[cnt] = true;
    }
}
