using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : DolObject
{
  private bool unlockSwitch = false;
  private ArrayList connectedLocks;
  // Start is called before the first frame update
  void Start()
  {
    connectedLocks = new ArrayList();
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    switch(collision.gameObject.tag)
    {
      case "Player":
        unlockSwitch = true;
        break;

      case "Lock":
        connectedLocks.Add(collision.gameObject.GetComponent<Lock>());
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

      case "Lock":
        connectedLocks.Remove(collision.gameObject.GetComponent<Lock>());
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

      foreach (Lock lck in connectedLocks)
      {
        lck.DestoryDolObject();
      }
    }
  }
}
