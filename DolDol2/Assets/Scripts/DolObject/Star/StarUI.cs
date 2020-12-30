﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarUI : MonoBehaviour
{
    public Image[] starUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0;i<GameManager.Instance.starCount;i++)
        {
            starUI[i].gameObject.SetActive(true);
        }
    }
}
