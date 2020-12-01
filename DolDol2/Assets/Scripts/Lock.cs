using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Lock : DolObject
{
  private bool unlockSwitch = false;
  private int lockIndexI = -1;
  private int lockIndexJ = -1;

  // Start is called before the first frame update
  void Start()
  {
    LockManager.Instance.AddLock(gameObject.GetComponent<Lock>());
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    switch(collision.gameObject.tag)
    {
      case "Player":
        unlockSwitch = true;
        break;
    }
  }

  void OnCollisionExit2D(Collision2D collision)
  {
    switch (collision.gameObject.tag)
    {
      case "Player":
        unlockSwitch = false;
        break;
    }
  }

  // Update is called once per frame
  void Update()
  {
    if (unlockSwitch && GameManager.Instance.keyCount > 0 && Input.GetKey(KeyCode.S))
    {
      GameManager.Instance.keyCount--;
      
      DestoryDolObject();
    }
  }

  protected override void DestoryDolObject()
  {
    base.DestoryDolObject();
    LockManager.Instance.RemoveLock(gameObject.GetComponent<Lock>());
  }

  public void CalculateLockPosition()
  {
    lockIndexI = ((int)Math.Round(transform.position.y / TileInterval) + 1);
    lockIndexJ = ((int)Math.Round(transform.position.x / TileInterval) + 1);
  }

  public int GetLockIndexI()
  {
    return lockIndexI;
  }

  public int GetLockIndexJ()
  {
    return lockIndexJ;
  }
}
