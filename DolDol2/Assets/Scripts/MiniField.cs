using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniField : MonoBehaviour
{
  string[,] FieldMap;
  Vector2 StartPosition;
  Field MainField;

  float TileInterval = 0.0f;
  bool GenerateField = true;

  // Start is called before the first frame update
  void Start()
  {
    StartPosition = new Vector2();
    FieldMap = new string[10 + 2, 10 + 2];
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
    transform.position = new Vector3(StartPosition.x + (10 / 2 - 0.5f) * TileInterval, StartPosition.y + (10 / 2 - 0.5f) * TileInterval, 0);

    // (Instantiate(MainField.Wall) as DolObject).transform.position = transform.position;

    for (int i = 1; i <= 10; i++)
    {
      for (int j = 1; j <= 10; j++)
      {
        switch (FieldMap[i, j])
        {
          case "1P":
            MainField.Player1.SetSpawnPos(new Vector2(StartPosition.x + (j - 1) * TileInterval, StartPosition.y + (10 + 2 - i - 1 - 1) * TileInterval));
            MainField.Player1.transform.position = MainField.Player1.GetSpawnPos();
            break;

          case "2P":
            MainField.Player2.SetSpawnPos(new Vector2(StartPosition.x + (j - 1) * TileInterval, StartPosition.y + (10 + 2 - i - 1 - 1) * TileInterval));
            MainField.Player2.transform.position = MainField.Player2.GetSpawnPos();
            break;
        }
      }
    }

    if (GenerateField == true)
    {
      for (int i = 1; i <= 10; i++)
      {
        for (int j = 1; j <= 10; j++)
        {
          DolObject obj = null;

          switch (FieldMap[i, j])
          {
            case "1":
              {
                obj = Instantiate(MainField.Wall) as DolObject;
              }
              break;

            case "0":
              obj = (Instantiate(Resources.Load("Prefab/Portal")) as GameObject).GetComponent<Portal>();
              break;

            case "4":
              obj = (Instantiate(Resources.Load("Prefab/Trap")) as GameObject).GetComponent<Portal>();
              break;

            case "5":
              obj = (Instantiate(Resources.Load("Prefab/Box")) as GameObject).GetComponent<Box>();
              break;

            case "7":
              obj = Instantiate(MainField.Star) as DolObject;
              break;

            case "8":
              obj = (Instantiate(Resources.Load("Prefab/Key")) as GameObject).GetComponent<Key>();
              break;

            case "9":
              obj = (Instantiate(Resources.Load("Prefab/Lock")) as GameObject).GetComponent<Lock>(); ;
              break;

            //case "3":
            //  obj = Instantiate(MainField.Floor) as DolObject;
            //  break;

            //case "4":
            //  obj = Instantiate(MainField.Enemy) as DolObject;
            //  break;

            //case "6":
            //  obj = Instantiate(MainField.Portal) as DolObject;
            //  break;

            default:
              continue;
          }

          obj.transform.position = new Vector2(StartPosition.x + (j - 1) * TileInterval, StartPosition.y + (10 + 2 - i - 1 - 1) * TileInterval);
          // obj.transform.position = new Vector2(StartPosition.x + (int)((j - 1) * TileInterval), StartPosition.y + (int)((10 + 2 - i - 1 - 1) * TileInterval));
          obj.transform.SetParent(transform);
        }
      }
    }
  }

  public void SetMap(string[,] fieldMap)
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
