using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Field : MonoBehaviour
{
  MiniField[,] MiniFieldMap;
  public MiniField MField;
  public Player Player1;
  public Player Player2;
  private Player CurrentPlayer;
  public DolObject Wall;
  public DolObject Enemy;
  public DolObject Floor;
  public DolObject Star;
  public DolObject Portal;
  // public MiniFieldSelector SelectorPrefab;
  private MiniFieldSelector Selector;

  public string Stage;



  //private int AnotherPlayerIndexI = 0;
  //private int AnotherPlayerIndexJ = 0;

  //private int CurrentMiniFieldIndexI = -1;
  //private int CurrentMiniFieldIndexJ = -1;

  private MiniField CurrentField;
  private Vector2 PrevPos;

  private FieldDataFromFileCSV Data;

  public bool GenerateField = true;
  private float TileInterval = 1.8f / 2;
  private bool PrevCharChoice = true;
  public int RangeI = 0;
  public int RangeJ = 0;

  void Start()
  {
    this.Init();

    CurrentPlayer = Player1;

    // Selector = Instantiate(SelectorPrefab) as MiniFieldSelector;

    MiniFieldMap = new MiniField[RangeI, RangeJ];

    

    if (Stage != "")
    {
      Data = new FieldDataFromFileCSV(Stage);
    }
    else
    {
      Data = new FieldDataFromFileCSV(GameManager.Instance.GetCurrentStageName());
    }

    PrevPos = new Vector2();

    if (GenerateField == true)
    {
      for (int i = 0; i < RangeI; i++)
      {
        for (int j = 0; j < RangeJ; j++)
        {
          string[,] dataMap = Data.GetPartialMapCSV(RangeI - i - 1, j);

          if (dataMap == null)
          {
            continue;
          }

          MiniFieldMap[i, j] = Instantiate(MField) as MiniField;
          MiniFieldMap[i, j].name = "minifield_" + i + "_" + j;
          MiniFieldMap[i, j].SetMainField(this);
          // Debug.Log("x : " + j + " " + "y : " + i);
          // Debug.Log((int)(10 * TileInterval));
          // Debug.Log(10 * TileInterval);
          // Debug.Log("x : " + j + " " + "y : " + i);
          // Debug.Log("x2 : " + j * 10 * TileInterval + " " + "y2 : " + i * 10 * TileInterval);
          // Debug.Log("x3 : " + j * (int)(10 * TileInterval) + " " + "y3 : " + i * (int)(10 * TileInterval));
          MiniFieldMap[i, j].SetStartPosition(new Vector2(j * 10 * TileInterval, i * 10 * TileInterval));
          // MiniFieldMap[i, j].SetStartPosition(new Vector2(j * (int)(10 * TileInterval), i * (int)(10 * TileInterval) +));
          MiniFieldMap[i, j].SetInterval(TileInterval);
          MiniFieldMap[i, j].SetMap(dataMap);
          MiniFieldMap[i, j].Init();
          MiniFieldMap[i, j].transform.SetParent(transform);
        }
      }
    }

    CalculatePlayerIndex();
    LockManager.Instance.CheckLocks();
  }

  void OnDestroy()
  {
    if (GameManager.Instance)
    {
      if (GameManager.Instance.ArrCalcIndex.Count > 0)
      {
        GameManager.Instance.ArrCalcIndex.Clear();
      }

      if (GameManager.Instance.ArrFixNeeded.Count > 0)
      {
        GameManager.Instance.ArrFixNeeded.Clear();
      }

      if (GameManager.Instance.ArrReRotateNeeded.Count > 0)
      {
        GameManager.Instance.ArrReRotateNeeded.Clear();
      }
    }
  }

  void Init()
  {
    RangeI = 5;
    RangeJ = 5;
  }

  void CalculatePlayerIndex()
  {
    if (GameManager.Instance.charChoice == true)
    {
      CurrentPlayer = Player1;
    }
    else
    {
      CurrentPlayer = Player2;
    }

    foreach (DolObject obj in GameManager.Instance.ArrCalcIndex)
    {
      obj.CalculateMinifieldIndex();
    }

    if (GameManager.Instance.GetCurrentMiniFieldIndexI() != CurrentPlayer.GetMinifieldIndexI() || GameManager.Instance.GetCurrentMiniFieldIndexJ() != CurrentPlayer.GetMinifieldIndexJ())
    {
      GameManager.Instance.SetCurrentMiniFieldIndexI(CurrentPlayer.GetMinifieldIndexI());
      GameManager.Instance.SetCurrentMiniFieldIndexJ(CurrentPlayer.GetMinifieldIndexJ());

      CurrentField = MiniFieldMap[GameManager.Instance.GetCurrentMiniFieldIndexI(), GameManager.Instance.GetCurrentMiniFieldIndexJ()];

      // Debug.Log("y :" + Math.Round(y));
      // Debug.Log("x :" + Math.Round(x));

      // Debug.Log("y :" + y);
      // Debug.Log("x :" + x);

      // Debug.Log("x :" + (x - TileInterval / (10 * TileInterval)));
      // Debug.Log("y :" + (y - TileInterval / (10 * TileInterval)));

      // Debug.Log("I :" + CurrentMiniFieldIndexI);
      // Debug.Log("J :" + CurrentMiniFieldIndexJ);

      // Selector.transform.SetParent(CurrentField.transform);
      // Selector.transform.position = CurrentField.transform.position;

    }
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyUp(KeyCode.Q))
    {
      CalculatePlayerIndex();
      CurrentField.Rotate(1);
    }

    if (Input.GetKeyUp(KeyCode.E))
    {
      CalculatePlayerIndex();
      CurrentField.Rotate(0);
    }

    //PrevPos.x = Player1.transform.position.x;
    //PrevPos.y = Player1.transform.position.y;
    //PrevCharChoice = GameManager.Instance.charChoice;
  }

  public Player GetCurrentPlayer()
  {
    return CurrentPlayer;
  }
}
