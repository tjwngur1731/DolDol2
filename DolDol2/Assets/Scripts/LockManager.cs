using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockManager
{
  private static LockManager instance = null;
  

  public static LockManager Instance
  {
    get
    {
      if (null == instance)
      {
        return null;
      }
      return instance;
    }
  }

  private void Update()
  {
    
  }
}
