using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
  public ArrayList ArrStaticRotated;
  string currentStageName = "";
  private int currentMiniFieldIndexI = -1;
  private int currentMiniFieldIndexJ = -1;
  string prevStageName = "";

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
    ArrStaticRotated = new ArrayList();
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
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (Player.twoPlayerEnter)
        {
            if (Input.GetKeyDown(KeyCode.Space)) { }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                 charChoice = !charChoice;

                if (UIManger.Instance)
                {
                         UIManger.Instance.SetChoiceUI(charChoice);
                }
            }
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

  public void SetPrevStageName(string stage)
  {
    prevStageName = stage;
  }

  public string GetPrevStageName()
  {
    return prevStageName;
  }

  public void SetCurrentStageName(string currentStageNameParameter)
  {
    prevStageName = currentStageName;
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

  public bool StageCheck()
  {
    return currentStageName == prevStageName;
  }
}
