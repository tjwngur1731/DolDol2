using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LockManager
{
  private static LockManager instance = null;
  private ArrayList connectedLocks;

  private Lock[,] checkBuffer;

  private LockManager()
  {
    connectedLocks = new ArrayList();
    checkBuffer = new Lock[50 + 2, 50 + 2];
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

  public void CheckLocks()
  {
    int minI = 51;
    int minJ = 51;

    int maxI = 0;
    int maxJ = 0;

    foreach (Lock lck in connectedLocks)
    {
      lck.CalculateLockPosition();
      // Debug.Log("i : " + lck.GetLockIndexI() + " j : " + lck.GetLockIndexJ());

      int indexI = lck.GetLockIndexI() - 1;
      int indexJ = lck.GetLockIndexJ() - 1;

      if (indexI < minI)
      {
        minI = indexI;
      }

      if (indexJ < minJ)
      {
        minJ = indexJ;
      }

      if (indexI >= maxI)
      {
        maxI = indexI;
      }

      if (indexJ >= maxJ)
      {
        maxJ = indexJ;
      }

      checkBuffer[indexI, indexJ] = lck;
    }

    for (int i = minI; i <= maxI; i++)
    {
      for (int j = minJ; j <= maxJ; j++)
      {
        if (checkBuffer[i,j] != null)
        {
          Debug.Log("i : " + i + " j : " + j);
        }
      }
    }

    Array.Clear(checkBuffer, 0, 50 * 50);
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
