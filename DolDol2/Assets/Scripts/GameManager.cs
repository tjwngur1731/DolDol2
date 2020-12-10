using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  private static GameManager instance = null;
  public int starCount = 0;
  public int keyCount = 0;
  public bool charChoice = true;
  bool isRotating = false;
  bool isInSameMiniField = false;
  public ArrayList ArrCalcIndex;
  public ArrayList ArrFixNeeded;
  public ArrayList ArrReRotateNeeded;
  string currentStageName = "";
  private int currentMiniFieldIndexI = -1;
  private int currentMiniFieldIndexJ = -1;

  void Awake()
  {
    charChoice = true;
    if (null == instance)
    {
      instance = this;
      DontDestroyOnLoad(this.gameObject);
    }
    else
    {
      Destroy(this.gameObject);
    }

    ArrCalcIndex = new ArrayList();
    ArrFixNeeded = new ArrayList();
    ArrReRotateNeeded = new ArrayList();
  }

  public static GameManager Instance
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
    if (Input.GetKeyDown(KeyCode.Space))
    {
      charChoice = !charChoice;
    }
  }

  public void SetIsRotating(bool isRot)
  {
    isRotating = isRot;
  }

  public bool GetIsRotating()
  {
    return isRotating;
  }

  public void SetIsInSameMiniField(bool isIn)
  {
    isInSameMiniField = isIn;
  }

  public bool GetIsInSameMiniField()
  {
    return isInSameMiniField;
  }

  public string GetCurrentStageName()
  {
    return currentStageName;
  }

  public void SetCurrentStageName(string currentStageNameParameter)
  {
    currentStageName = currentStageNameParameter;
  }

  public void SetCurrentMiniFieldIndexI(int indexI)
  {
    currentMiniFieldIndexI = indexI;
  }

  public void SetCurrentMiniFieldIndexJ(int indexJ)
  {
    currentMiniFieldIndexJ = indexJ;
  }

  public int GetCurrentMiniFieldIndexI()
  {
    return currentMiniFieldIndexI;
  }

  public int GetCurrentMiniFieldIndexJ()
  {
    return currentMiniFieldIndexJ;
  }
}
