using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockManager
{
  private static LockManager instance = null;
  private ArrayList connectedLocks;

  private LockManager()
  {
    connectedLocks = new ArrayList();
  }

  public static LockManager Instance
  {
    get
    {
      if (null == instance)
      {
        instance = new LockManager();
      }
      return instance;
    }
  }

  private void Update()
  {
    
  }

  public void AddLock(Lock lck)
  {
    connectedLocks.Add(lck);
  }

  public void RemoveLock(Lock lck)
  {
    connectedLocks.Remove(lck);
  }
}
