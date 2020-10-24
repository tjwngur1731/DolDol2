using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    // BaseObject[,] FieldMap = new BaseObject[5, 5];

    int[,] FieldMap = new int[5, 5];

    public BaseObject PlayerCharacter;
    public BaseObject TestObstacle;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(2.0f, 2.0f, 0);
        PlayerCharacter.transform.position = new Vector3(1.0f, 1.0f, 0);

        InitMap();
        
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (FieldMap[i, j] == 1)
                {
                    BaseObject obs = Instantiate(TestObstacle) as BaseObject;
                    obs.transform.position = new Vector3(j, i, 0);
                    
                    obs.transform.SetParent(transform);
                }
            }
        }
    }

    void InitMap()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                // if (true) {
                if (i == 0 || i == 4 || j == 0 || j == 4)
                {
                    FieldMap[i, j] = 1;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            transform.Rotate(0.0f, 0.0f, 45.0f);
        }
    }
}
