using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LockManager
{
  private static LockManager instance = null;
  private ArrayList connectedLocks;

  private Lock[,] checkBuffer;
  ArrayList bfsQueue;
  ArrayList destoryedLocks;

  private LockManager()
  {
    connectedLocks = new ArrayList();
    checkBuffer = new Lock[50 + 2, 50 + 2];
    bfsQueue = new ArrayList();
    destoryedLocks = new ArrayList();
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

  public void ResetLockManager()
  {
    connectedLocks.Clear();
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

    int lockID = 0;

    foreach (Lock lck in connectedLocks)
    {
      if (lck == null)
      {
        continue;
      }

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
          bfsQueue.Add(checkBuffer[i, j]);

          while (bfsQueue.Count > 0)
          {
            Lock current = bfsQueue[0] as Lock;

            int x = current.GetLockIndexJ() - 1;
            int y = current.GetLockIndexI() - 1;

            checkBuffer[y, x] = null;

            Lock up = checkBuffer[y - 1, x];
            Lock down = checkBuffer[y + 1, x];
            Lock left = checkBuffer[y, x - 1];
            Lock right = checkBuffer[y, x + 1];

            if (up)
            {
              bfsQueue.Add(up);
            }

            if (down)
            {
              bfsQueue.Add(down);
            }

            if (left)
            {
              bfsQueue.Add(left);
            }

            if (right)
            {
              bfsQueue.Add(right);
            }

            (bfsQueue[0] as Lock).SetLockID(lockID);
            bfsQueue.RemoveAt(0);
          }

          lockID++;
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

  public void DestroyLocks(int id)
  {
    foreach (Lock lck in connectedLocks)
    {
      if (lck == null)
      {
        continue;
      }

      if (lck.GetLockID() == id)
      {
        destoryedLocks.Add(lck);
      }
    }

    foreach (Lock destoryedLock in destoryedLocks)
    {
      RemoveLock(destoryedLock);
      destoryedLock.DestoryDolObject();
    }

    destoryedLocks.Clear();
  }
}
