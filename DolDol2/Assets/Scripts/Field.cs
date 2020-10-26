using System.Collections;
using System.Collections.Generic;
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

    public int Stage;

    private int PlayerIndexI = 0;
    private int PlayerIndexJ = 0;

    private int CurrentMiniFieldIndexI = 0;
    private int CurrentMiniFieldIndexJ = 0;

    private MiniField CurrentField;
    private Vector2 PrevPos;

    private FieldData Data;

    public bool GenerateField = true;
    private float TileInterval = 1.8f;
    private bool PrevCharChoice = true;
    public int RangeI = 0;
    public int RangeJ = 0;

    void Start()
    {
        RangeI = 5;
        RangeJ = 5;

        CurrentMiniFieldIndexI = RangeI - 1;

        MiniFieldMap = new MiniField[RangeI, RangeJ];

        Data = new FieldData(Stage);
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
                    MiniFieldMap[i, j].SetStartPosition(new Vector2(j * 5 * TileInterval, i * 5 * TileInterval));
                    MiniFieldMap[i, j].SetInterval(TileInterval);
                    MiniFieldMap[i, j].SetMap(dataMap);
                    MiniFieldMap[i, j].Init();
                    MiniFieldMap[i, j].transform.SetParent(transform);
                }
            }
        }
        else
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

                    MiniFieldMap[i, j] = GameObject.Find("minifield_" + i + "_" + j).GetComponent<MiniField>();
                    MiniFieldMap[i, j].SetMainField(this);
                    MiniFieldMap[i, j].SetStartPosition(new Vector2(j* 5 * TileInterval, i * 5 * TileInterval));
                    MiniFieldMap[i, j].SetInterval(TileInterval);
                    MiniFieldMap[i, j].SetMap(dataMap);
                    MiniFieldMap[i, j].Init();
                }
            }
        }

        CurrentField = MiniFieldMap[CurrentMiniFieldIndexI, CurrentMiniFieldIndexJ];

        CalculatePlayerIndex();
    }

    void CalculatePlayerIndex()
    {
        float x = 0.0f;
        float y = 0.0f;

        if (GameManager.Instance.charChoice == true)
        {
            x = Player1.transform.position.x;
            y = Player1.transform.position.y;
        }
        else
        { 
            x = Player2.transform.position.x;
            y = Player2.transform.position.y;
        }

        PlayerIndexJ = (int)x / (int)(5 * TileInterval);
        PlayerIndexI = (int)y / (int)(5 * TileInterval);

        if (PlayerIndexI >= RangeI)
        {
            PlayerIndexI = RangeI - 1;
        }

        if (PlayerIndexJ >= RangeJ)
        {
            PlayerIndexJ = RangeJ - 1;
        }

        if (CurrentMiniFieldIndexI != PlayerIndexI || CurrentMiniFieldIndexJ != PlayerIndexJ)
        {
            CurrentMiniFieldIndexI = PlayerIndexI;
            CurrentMiniFieldIndexJ = PlayerIndexJ;

            CurrentField = MiniFieldMap[CurrentMiniFieldIndexI, CurrentMiniFieldIndexJ];
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
