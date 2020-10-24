using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    MiniField[,] FieldMap = new MiniField[2,2];
    public MiniField MField;
    public BaseObject PlayerCharacter;
    public BaseObject TestObstacle;

    private int PlayerIndexI = 0;
    private int PlayerIndexJ = 0;

    private int CurrentMiniFieldIndexI = 0;
    private int CurrentMiniFieldIndexJ = 0;

    private MiniField CurrentField;
    private Vector2 PrevPos;
    // Start is called before the first frame update
    void Start()
    {
        PrevPos = new Vector2();
        for (int i = 0; i < 2; i++) {
            for (int j = 0; j < 2; j++) {
                FieldMap[i,j] = Instantiate(MField) as MiniField;
                FieldMap[i,j].SetObstacle(TestObstacle);
                FieldMap[i,j].SetStartPosition(new Vector2(j * 5 * 1.2f, i * 5 * 1.2f));
                FieldMap[i,j].Init();
            }
        }

        CurrentField = FieldMap[CurrentMiniFieldIndexI,CurrentMiniFieldIndexJ];
        PlayerCharacter.transform.position = new Vector2(1.0f, 1.0f);

        CalculatePlayerIndex();
    }

    void CalculatePlayerIndex()
    {
        PlayerIndexJ = (int)PlayerCharacter.transform.position.x / (int)(5 * 1.2f);
        PlayerIndexI = (int)PlayerCharacter.transform.position.y / (int)(5 * 1.2f);

        if (CurrentMiniFieldIndexI != PlayerIndexI || CurrentMiniFieldIndexJ != PlayerIndexJ)
        {
            CurrentMiniFieldIndexI = PlayerIndexI;
            CurrentMiniFieldIndexJ = PlayerIndexJ;

            CurrentField = FieldMap[CurrentMiniFieldIndexI,CurrentMiniFieldIndexJ];
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
