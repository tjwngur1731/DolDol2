using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniField : MonoBehaviour
{
    int[,] FieldMap, TempBuffer;
    Vector2 StartPosition;
    BaseObject TestObstacle;

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

        if (GenerateField == true)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (FieldMap[i, j] == 2)
                    {
                        BaseObject obs = Instantiate(TestObstacle) as BaseObject;
                        obs.transform.position = new Vector2(StartPosition.x + j * TileInterval, StartPosition.y + (5 - i - 1) * TileInterval);

                        obs.transform.SetParent(transform);
                    }
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

    public void SetObstacle(BaseObject obs)
    {
        TestObstacle = obs;
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
