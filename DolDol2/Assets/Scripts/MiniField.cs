using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniField : MonoBehaviour
{
    int[,] FieldMap, TempBuffer;
    Vector2 StartPosition;
    Field MainField;

    float TileInterval = 0.0f;
    bool GenerateField = true;
    // Start is called before the first frame update
    void Start()
    {
        StartPosition = new Vector2();
        FieldMap = new int[5, 5];
    }

    public void SetInterval(float interval)
    {
        TileInterval = interval;
    }

    public void SetGenerateField(bool generateField)
    {
        GenerateField = generateField;
    }

    public void Init()
    {
        transform.position = new Vector3(StartPosition.x + 2 * TileInterval, StartPosition.y + 2 * TileInterval, 0);

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                BaseObject obj = null;

                switch (FieldMap[i, j])
                {
                    case 0:
                        // obj = Instantiate(MainField.Player1) as BaseObject;
                        // (obj as Player).SetSpawnPos(new Vector2(StartPosition.x + j * TileInterval, StartPosition.y + (5 - i - 1) * TileInterval));
                        // obj.transform.position = (obj as Player).GetSpawnPos();
                        MainField.Player1.SetSpawnPos(new Vector2(StartPosition.x + j * TileInterval, StartPosition.y + (5 - i - 1) * TileInterval));
                        MainField.Player1.transform.position = MainField.Player1.GetSpawnPos();
                        break;

                    case 1:
                        // obj = Instantiate(MainField.Player2) as BaseObject;
                        // (obj as Player).SetSpawnPos(new Vector2(StartPosition.x + j * TileInterval, StartPosition.y + (5 - i - 1) * TileInterval));
                        // obj.transform.position = (obj as Player).GetSpawnPos();
                        MainField.Player2.SetSpawnPos(new Vector2(StartPosition.x + j * TileInterval, StartPosition.y + (5 - i - 1) * TileInterval));
                        MainField.Player2.transform.position = MainField.Player2.GetSpawnPos();
                        break;
                }
            }
        }

        if (GenerateField == true)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    BaseObject obj = null;

                    switch(FieldMap[i, j])
                    {
                        case 2:
                            obj = Instantiate(MainField.Wall) as BaseObject;     
                        break;

                        case 3:
                            obj = Instantiate(MainField.Floor) as BaseObject;     
                        break;

                        case 4:
                            obj = Instantiate(MainField.Enemy) as BaseObject;
                        break;

                        // case 5:
                        //     obj = Instantiate(MainField.Star) as BaseObject;
                        // break;

                        default:
                        continue;
                    }

                    obj.transform.position = new Vector2(StartPosition.x + j * TileInterval, StartPosition.y + (5 - i - 1) * TileInterval);
                    obj.transform.SetParent(transform);
                }
            }
        }
    }

    public void SetMap(int[,] fieldMap)
    {
        FieldMap = fieldMap;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetStartPosition(Vector2 startPosition)
    {
        StartPosition = startPosition;

        
    }

    public void SetMainField(Field mainField)
    {
        MainField = mainField;
    }

    public void Rotate(int dir)
    {
        if (dir == 0)
        {
            StartCoroutine(RoateField(1.0f, -90.0f));
        }
        else if (dir == 1)
        {
            StartCoroutine(RoateField(1.0f, 90.0f));
        }
    }

    IEnumerator RoateField(float duration, float endAngle)
    {
        float startRotation = transform.eulerAngles.z;
        float endRotation = startRotation + endAngle;
        float t = 0.0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float zRoation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, zRoation);

            yield return null;
        }
    }
}
