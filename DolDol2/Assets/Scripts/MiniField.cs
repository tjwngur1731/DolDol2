﻿using System.Collections;
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
    FieldMap = new int[7, 7];
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

    // (Instantiate(MainField.Wall) as BaseObject).transform.position = transform.position;

    for (int i = 1; i <= 5; i++)
    {
      for (int j = 1; j <= 5; j++)
      {
        switch (FieldMap[i, j])
        {
          case 0:
            MainField.Player1.SetSpawnPos(new Vector2(StartPosition.x + (j - 1) * TileInterval, StartPosition.y + (7 - i - 1 - 1) * TileInterval));
            MainField.Player1.transform.position = MainField.Player1.GetSpawnPos();
            break;

          case 1:
            MainField.Player2.SetSpawnPos(new Vector2(StartPosition.x + (j - 1) * TileInterval, StartPosition.y + (7 - i - 1 - 1) * TileInterval));
            MainField.Player2.transform.position = MainField.Player2.GetSpawnPos();
            break;
        }
      }
    }

    if (GenerateField == true)
    {
      for (int i = 1; i <= 5; i++)
      {
        for (int j = 1; j <= 5; j++)
        {
          BaseObject obj = null;

          switch (FieldMap[i, j])
          {
            case 2:
              {
                obj = Instantiate(MainField.Wall) as BaseObject;

                Wall wall = (obj as Wall);
                
                int up = FieldMap[i - 1, j];
                int down = FieldMap[i + 1, j];
                int left = FieldMap[i, j - 1];
                int right = FieldMap[i, j + 1];

                // 0칸
                if ((up == 2 && down == 2) &&
                    (left == 2 && right == 2))
                {
                  wall.SetWallType(10);
                }

                // 1칸
                if ((up != 2 && down == 2) &&
                    (left == 2 && right == 2))
                {
                  wall.SetWallType(0);
                }

                if ((up == 2 && down != 2) &&
                    (left == 2 && right == 2))
                {
                  wall.SetWallType(0);
                  obj.transform.eulerAngles = new Vector3(obj.transform.eulerAngles.x, obj.transform.eulerAngles.y, obj.transform.eulerAngles.z + 180.0f);
                }

                if ((up == 2 && down == 2) &&
                    (left != 2 && right == 2))
                {
                  wall.SetWallType(0);
                  obj.transform.eulerAngles = new Vector3(obj.transform.eulerAngles.x, obj.transform.eulerAngles.y, obj.transform.eulerAngles.z + 90.0f);
                }

                if ((up == 2 && down == 2) &&
                    (left == 2 && right != 2))
                {
                  wall.SetWallType(0);
                  obj.transform.eulerAngles = new Vector3(obj.transform.eulerAngles.x, obj.transform.eulerAngles.y, obj.transform.eulerAngles.z - 90.0f);
                }

                // 2칸
                if ((up == 2 && down == 2) &&
                    (left != 2 && right != 2))
                {
                  wall.SetWallType(9);
                  obj.transform.eulerAngles = new Vector3(obj.transform.eulerAngles.x, obj.transform.eulerAngles.y, obj.transform.eulerAngles.z + 90.0f);
                }

                if ((up != 2 && down != 2) &&
                    (left == 2 && right == 2))
                {
                  wall.SetWallType(9);
                }

                if ((up == 2 && down != 2) &&
                    (left == 2 && right != 2))
                {
                  wall.SetWallType(3);
                  obj.transform.eulerAngles = new Vector3(obj.transform.eulerAngles.x, obj.transform.eulerAngles.y, obj.transform.eulerAngles.z + 180.0f);
                }

                if ((up == 2 && down != 2) &&
                    (left != 2 && right == 2))
                {
                  wall.SetWallType(3);
                  obj.transform.eulerAngles = new Vector3(obj.transform.eulerAngles.x, obj.transform.eulerAngles.y, obj.transform.eulerAngles.z + 90.0f);
                }

                if ((up != 2 && down == 2) &&
                    (left == 2 && right != 2))
                {
                  wall.SetWallType(3);
                  obj.transform.eulerAngles = new Vector3(obj.transform.eulerAngles.x, obj.transform.eulerAngles.y, obj.transform.eulerAngles.z - 90.0f);
                }

                if ((up != 2 && down == 2) &&
                    (left != 2 && right == 2))
                {
                  wall.SetWallType(3);
                }

                // 3개
                if ((up != 2 && down != 2) &&
                    (left != 2 && right == 2))
                {
                  wall.SetWallType(4);
                  obj.transform.eulerAngles = new Vector3(obj.transform.eulerAngles.x, obj.transform.eulerAngles.y, 0);
                }

                if ((up != 2 && down != 2) &&
                    (left == 2 && right != 2))
                {
                  wall.SetWallType(4);
                  obj.transform.eulerAngles = new Vector3(obj.transform.eulerAngles.x, obj.transform.eulerAngles.y, 180);
                }

                if ((up != 2 && down == 2) &&
                    (left != 2 && right != 2))
                {
                  wall.SetWallType(4);
                  obj.transform.eulerAngles = new Vector3(obj.transform.eulerAngles.x, obj.transform.eulerAngles.y, -90.0f);
                }

                if ((up == 2 && down != 2) &&
                    (left != 2 && right != 2))
                {
                  wall.SetWallType(4);
                  obj.transform.eulerAngles = new Vector3(obj.transform.eulerAngles.x, obj.transform.eulerAngles.y, 90.0f);
                }

                // 4개
                if ((up != 2 && down != 2) &&
                    (left != 2 && right != 2))
                {
                  wall.SetWallType(6);
                }

                // wall.SetWallType(6);
                
              }
              break;

            case 3:
              obj = Instantiate(MainField.Floor) as BaseObject;
              break;

            case 4:
              obj = Instantiate(MainField.Enemy) as BaseObject;
              break;

            case 5:
              obj = Instantiate(MainField.Star) as BaseObject;
              break;

            case 6:
              obj = Instantiate(MainField.Portal) as BaseObject;
              break;

            default:
              continue;
          }

          obj.transform.position = new Vector2(StartPosition.x + (j - 1) * TileInterval, StartPosition.y + (7 - i - 1 - 1) * TileInterval);
          // obj.transform.position = new Vector2(StartPosition.x + (int)((j - 1) * TileInterval), StartPosition.y + (int)((7 - i - 1 - 1) * TileInterval));
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

    // Debug.Log("cell x : " + StartPosition.x + " " + "cell y : " + StartPosition.y);
  }

  public void SetMainField(Field mainField)
  {
    MainField = mainField;
  }

  public void Rotate(int dir)
  {
    if (GameManager.Instance.GetIsRotating() == true)
    {
      return;
    }

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

    GameManager.Instance.SetIsRotating(true);

    if (GameManager.Instance.charChoice == true)
    {
      MainField.Player1.transform.SetParent(transform);
      MainField.Player1.SetIsKinematic(true);

      if (GameManager.Instance.GetIsInSameMiniField())
      {
        MainField.Player2.transform.SetParent(transform);
        MainField.Player2.SetIsKinematic(true);  
      }
    }
    else
    {
      MainField.Player2.transform.SetParent(transform);
      MainField.Player2.SetIsKinematic(true);

      if (GameManager.Instance.GetIsInSameMiniField())
      {
        MainField.Player1.transform.SetParent(transform);
        MainField.Player1.SetIsKinematic(true);  
      }
    }

    while (t < duration)
    {
      t += Time.deltaTime;
      float zRoation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;
      transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, zRoation);

      yield return null;
    }

    if (GameManager.Instance.charChoice == true)
    {
      MainField.Player1.transform.SetParent(null);
      MainField.Player1.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.y);
      MainField.Player1.SetIsKinematic(false);
      
      if (GameManager.Instance.GetIsInSameMiniField())
      {
        MainField.Player2.transform.SetParent(null);
        MainField.Player2.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.y);
        MainField.Player2.SetIsKinematic(false);
      }
    }
    else
    {
      MainField.Player2.transform.SetParent(null);
      MainField.Player2.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.y);
      MainField.Player2.SetIsKinematic(false);

      if (GameManager.Instance.GetIsInSameMiniField())
      {
        MainField.Player1.transform.SetParent(null);
        MainField.Player1.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.y);
        MainField.Player1.SetIsKinematic(false);
      }
    }
    
    GameManager.Instance.SetIsRotating(false);

    yield break;
  }
}
