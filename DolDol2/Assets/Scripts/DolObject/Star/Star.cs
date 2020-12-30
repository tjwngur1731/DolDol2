﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Star : DolObject
{

  public Image[] starUI;

  private void Start()
  {
    GameManager.Instance.starCount = 0;
  }

  void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.tag == "Player")
    {
      DestoryDolObject();
    }
  }
  private void Update()
  {
    // if (Input.GetButtonDown("Jump")) GameManager.Instance.starCount++;
    // StarUp(GameManager.Instance.starCount);
  }

  void StarUp(int cnt)
  {
    for (int i = 0; i < cnt; i++)
    {
      starUI[i].gameObject.SetActive(true);
    }
  }
}
