using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniField : MonoBehaviour
{
    int[,] FieldMap, TempBuffer;
    Vector2 StartPosition;
    BaseObject TestObstacle;

    // Start is called before the first frame update
    void Start()
    {
        StartPosition = new Vector2();
    }

    public void Init()
    {
        FieldMap = new int[5, 5];
        TempBuffer = new int[5, 5];
        // transform.position = new Vector3(2.0f, 2.0f, 0);
        transform.position = new Vector3(StartPosition.x + 2 * 1.2f, StartPosition.y + 2 * 1.2f, 0);

        InitMap();

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (FieldMap[i, j] == 1)
                {
                    BaseObject obs = Instantiate(TestObstacle) as BaseObject;
                    obs.transform.position = new Vector2(StartPosition.x + j * 1.2f, StartPosition.y + i * 1.2f);

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
                if (i == 0 || i == 4 || j == 0 || j == 4)
                {
                    FieldMap[i, j] = 1;
                }

                if ((i == 1 || i == 2) && (j == 2))
                {
                    FieldMap[i, j] = 1;
                }
            }
        }

        FieldMap[1,0] = FieldMap[1,4] = 0;
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
            float zRoation = Mathf.Lerp(startRotation, endRotation, t / duration) % 90.0f;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, zRoation);

            // if (endAngle < 0)
            // {
            //     if (zRoation <= endAngle)
            //     {
            //         yield break;
            //     }
            // }
            // else
            // {
            //     if (zRoation >= endAngle)
            //     {
            //         yield break;
            //     }
            // }

            yield return null;
        }
    }
}
