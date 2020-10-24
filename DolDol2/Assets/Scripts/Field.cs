using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    // BaseObject[,] FieldMap = new BaseObject[5, 5];

    MiniField[,] FieldMap = new MiniField[2,2];
    public MiniField MField;
    public BaseObject PlayerCharacter;
    public BaseObject TestObstacle;

    private MiniField CurrentField;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 2; i++) {
            for (int j = 0; j < 2; j++) {
                FieldMap[i,j] = Instantiate(MField) as MiniField;
                FieldMap[i,j].SetObstacle(TestObstacle);
                FieldMap[i,j].SetStartPosition(new Vector2(j * 5, i * 5));
                FieldMap[i,j].Init();
            }
        }

        CurrentField = FieldMap[1,1];
        PlayerCharacter.transform.position = new Vector2(1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            CurrentField.Rotate(0);
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            CurrentField.Rotate(1);
        }
    }
}
