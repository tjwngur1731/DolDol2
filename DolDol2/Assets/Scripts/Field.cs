using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    MiniField[,] MiniFieldMap = new MiniField[2, 2];
    public MiniField MField;
    public Player PlayerCharacter;
    public BaseObject TestObstacle;

    private int PlayerIndexI = 0;
    private int PlayerIndexJ = 0;

    private int CurrentMiniFieldIndexI = 0;
    private int CurrentMiniFieldIndexJ = 0;

    private MiniField CurrentField;
    private Vector2 PrevPos;

    private FieldData Data;

    public bool GenerateField = true;
    private float TileInterval = 1.8f;
    // Start is called before the first frame update
    void Start()
    {
        Data = new FieldData();
        PrevPos = new Vector2();

        if (GenerateField == true)
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    MiniFieldMap[i, j] = Instantiate(MField) as MiniField;
                    MiniFieldMap[i,j].name = "minifield_" + i + "_" + j;
                    MiniFieldMap[i, j].SetObstacle(TestObstacle);
                    MiniFieldMap[i, j].SetStartPosition(new Vector2(j * 5 * TileInterval, (2 - i - 1) * 5 * TileInterval));
                    MiniFieldMap[i, j].SetInterval(TileInterval);
                    MiniFieldMap[i, j].SetMap(Data.GetPartialMap(i, j));
                    MiniFieldMap[i, j].Init();
                    MiniFieldMap[i, j].transform.SetParent(transform);
                }
            }
        }
        else
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    MiniFieldMap[i, j] = GameObject.Find("minifield_" + i + "_" + j).GetComponent<MiniField>();
                    MiniFieldMap[i, j].SetStartPosition(new Vector2(j * 5 * TileInterval, (2 - i - 1) * 5 * TileInterval));
                    MiniFieldMap[i, j].SetInterval(TileInterval);
                    MiniFieldMap[i, j].Init();
                }
            }
        }

        CurrentField = MiniFieldMap[CurrentMiniFieldIndexI, CurrentMiniFieldIndexJ];
        PlayerCharacter.transform.position = PlayerCharacter.spawnPos.position;

        CalculatePlayerIndex();
    }

    void CalculatePlayerIndex()
    {
        PlayerIndexJ = (int)PlayerCharacter.transform.position.x / (int)(5 * TileInterval);
        PlayerIndexI = (2 - (int)PlayerCharacter.transform.position.y / (int)(5 * TileInterval) - 1);

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
        if (PrevPos.x != PlayerCharacter.transform.position.x || PrevPos.y != PlayerCharacter.transform.position.y)
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

        PrevPos.x = PlayerCharacter.transform.position.x;
        PrevPos.y = PlayerCharacter.transform.position.y;
    }
}
