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
    public BaseObject Wall;
    public BaseObject Enemy;
    public BaseObject Floor;
    public BaseObject Star;
    public BaseObject Portal;
    // public MiniFieldSelector SelectorPrefab;
    private MiniFieldSelector Selector;

    public string Stage;
    public int FieldType = 0;

    private int PlayerIndexI = 0;
    private int PlayerIndexJ = 0;
    private int AnotherPlayerIndexI = 0;
    private int AnotherPlayerIndexJ = 0;

    private int CurrentMiniFieldIndexI = -1;
    private int CurrentMiniFieldIndexJ = -1;

    private MiniField CurrentField;
    private Vector2 PrevPos;

    private FieldDataBase Data;

    public bool GenerateField = true;
    private float TileInterval = 1.8f;
    private bool PrevCharChoice = true;
    public int RangeI = 0;
    public int RangeJ = 0;

    void Start()
    {
        this.Init();

        // Selector = Instantiate(SelectorPrefab) as MiniFieldSelector;

        CurrentMiniFieldIndexI = RangeI - 1;

        MiniFieldMap = new MiniField[RangeI, RangeJ];

        switch(FieldType)
        {
            case 0:
                Data = new FieldData(Stage);
            break;

            case 1:
                Data = new FieldDataOld(Stage);
            break;

            case 2:
                Data = new FieldDataFromFile(Stage);
            break;
        }

        PrevPos = new Vector2();

        if (GenerateField == true)
        {
            for (int i = 0; i < RangeI; i++)
            {
                for (int j = 0; j < RangeJ; j++)
                {
                    int[,] dataMap = Data.GetPartialMap(RangeI - i - 1, j);

                    if (dataMap == null)
                    {
                        continue;
                    }

                    MiniFieldMap[i, j] = Instantiate(MField) as MiniField;
                    MiniFieldMap[i, j].name = "minifield_" + i + "_" + j;
                    MiniFieldMap[i, j].SetMainField(this);
                    // Debug.Log("x : " + j + " " + "y : " + i);
                    // Debug.Log((int)(5 * TileInterval));
                    // Debug.Log(5 * TileInterval);
                    // Debug.Log("x : " + j + " " + "y : " + i);
                    // Debug.Log("x2 : " + j * 5 * TileInterval + " " + "y2 : " + i * 5 * TileInterval);
                    // Debug.Log("x3 : " + j * (int)(5 * TileInterval) + " " + "y3 : " + i * (int)(5 * TileInterval));
                    MiniFieldMap[i, j].SetStartPosition(new Vector2(j * 5 * TileInterval, i * 5 * TileInterval));
                    // MiniFieldMap[i, j].SetStartPosition(new Vector2(j * (int)(5 * TileInterval), i * (int)(5 * TileInterval) +));
                    MiniFieldMap[i, j].SetInterval(TileInterval);
                    MiniFieldMap[i, j].SetMap(dataMap);
                    MiniFieldMap[i, j].Init();
                    MiniFieldMap[i, j].transform.SetParent(transform);
                }
            }
        }

        CalculatePlayerIndex();

        CurrentField = MiniFieldMap[CurrentMiniFieldIndexI, CurrentMiniFieldIndexJ];
        // Selector.transform.SetParent(CurrentField.transform);
        // Selector.transform.position = CurrentField.transform.position;
    }

    void Init()
    {
        RangeI = 5;
        RangeJ = 5;
    }

    public void DrawLines(bool draw)
    {
        for (int i = 0; i < RangeI; i++)
            {
                for (int j = 0; j < RangeJ; j++)
                {
                    // MiniFieldMap[i, j].DrawLines(draw);
                }
            }
    }

    void CalculatePlayerIndex()
    {
        float x = 0.0f;
        float y = 0.0f;

        float anotherX = 0.0f;
        float anotherY = 0.0f;

        if (GameManager.Instance.charChoice == true)
        {
            x = Player1.transform.position.x;
            y = Player1.transform.position.y;

            anotherX = Player2.transform.position.x;
            anotherY = Player2.transform.position.y;
        }
        else
        { 
            x = Player2.transform.position.x;
            y = Player2.transform.position.y;

            anotherX = Player1.transform.position.x;
            anotherY = Player1.transform.position.y;
        }

        // Debug.Log((int)(5 * TileInterval) + 1);
        // Debug.Log((int)(5 * TileInterval));
        // Debug.Log((5 * TileInterval));

        // PlayerIndexJ = (int)x / ((int)(5 * TileInterval) + 1);
        // PlayerIndexI = (int)y / ((int)(5 * TileInterval) + 1);

        // AnotherPlayerIndexJ = (int)anotherX / ((int)(5 * TileInterval) + 1);
        // AnotherPlayerIndexI = (int)anotherY / ((int)(5 * TileInterval) + 1);

        PlayerIndexJ = (int)((Math.Round(x)) / (5 * TileInterval));
        PlayerIndexI = (int)((Math.Round(y)) / (5 * TileInterval));

        AnotherPlayerIndexJ = (int)(Math.Round(anotherX) / (5 * TileInterval));
        AnotherPlayerIndexI = (int)(Math.Round(anotherY) / (5 * TileInterval));

        if (CurrentMiniFieldIndexI != PlayerIndexI || CurrentMiniFieldIndexJ != PlayerIndexJ)
        {
            CurrentMiniFieldIndexI = PlayerIndexI;
            CurrentMiniFieldIndexJ = PlayerIndexJ;

            CurrentField = MiniFieldMap[CurrentMiniFieldIndexI, CurrentMiniFieldIndexJ];

            // Debug.Log("y :" + Math.Round(y));
            // Debug.Log("x :" + Math.Round(x));

            // Debug.Log("y :" + y);
            // Debug.Log("x :" + x);

            // Debug.Log("x :" + (x - TileInterval / (5 * TileInterval)));
            // Debug.Log("y :" + (y - TileInterval / (5 * TileInterval)));

            // Debug.Log("I :" + CurrentMiniFieldIndexI);
            // Debug.Log("J :" + CurrentMiniFieldIndexJ);

            if (CurrentMiniFieldIndexI == AnotherPlayerIndexI && CurrentMiniFieldIndexJ == AnotherPlayerIndexJ)
            {
                GameManager.Instance.SetIsInSameMiniField(true);
            }
            else
            {
                GameManager.Instance.SetIsInSameMiniField(false);
            }

            // Selector.transform.SetParent(CurrentField.transform);
            // Selector.transform.position = CurrentField.transform.position;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PrevPos.x != Player1.transform.position.x || PrevPos.y != Player1.transform.position.y || PrevCharChoice != GameManager.Instance.charChoice)
        {
            CalculatePlayerIndex();
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            CurrentField.Rotate(1);
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            CurrentField.Rotate(0);
        }

        PrevPos.x = Player1.transform.position.x;
        PrevPos.y = Player1.transform.position.y;
        PrevCharChoice = GameManager.Instance.charChoice;
    }
}
