using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    // transform.position = new Vector3(StartPosition.x + (10 / 2 - 0.5f) * TileInterval, StartPosition.y + (10 / 2 - 0.5f) * TileInterval, 0);
    transform.position = new Vector3((float)((decimal)StartPosition.x + (decimal)(10 / 2 - 0.5f) * (decimal)TileInterval), (float)((decimal)StartPosition.y + (decimal)(10 / 2 - 0.5f) * (decimal)TileInterval), 0);

    for (int i = 1; i <= 10; i++)
    {
      for (int j = 1; j <= 10; j++)
      {
        switch (FieldMap[i, j])
        {
          case "P1":
            MainField.Player1.SetSpawnPos(new Vector2(StartPosition.x + (j - 1) * TileInterval, StartPosition.y + (10 + 2 - i - 1 - 1) * TileInterval));
            // MainField.Player1.SetSpawnPos(new Vector2(StartPosition.x + (j - 1) * TileInterval, Mathf.Round(StartPosition.y + (10 + 2 - i - 1 - 1) * TileInterval * 100.0f) * 0.01f));
            MainField.Player1.transform.position = MainField.Player1.GetSpawnPos();
            GameManager.Instance.ArrCalcIndex.Add(MainField.Player1);
            GameManager.Instance.ArrFixNeeded.Add(MainField.Player1);
            GameManager.Instance.ArrReRotateNeeded.Add(MainField.Player1);
            break;

          case "P2":
            MainField.Player2.SetSpawnPos(new Vector2(StartPosition.x + (j - 1) * TileInterval, StartPosition.y + (10 + 2 - i - 1 - 1) * TileInterval));
            // MainField.Player2.SetSpawnPos(new Vector2(StartPosition.x + (j - 1) * TileInterval, Mathf.Round(StartPosition.y + (10 + 2 - i - 1 - 1) * TileInterval * 100.0f) * 0.01f));
            MainField.Player2.transform.position = MainField.Player2.GetSpawnPos();
            GameManager.Instance.ArrCalcIndex.Add(MainField.Player2);
            GameManager.Instance.ArrFixNeeded.Add(MainField.Player2);
            GameManager.Instance.ArrReRotateNeeded.Add(MainField.Player2);
            break;
        }
      }
    }

    string[] dolObjects = null;

    if (GenerateField == true)
    {
      for (int i = 1; i <= 10; i++)
      {
        for (int j = 1; j <= 10; j++)
        {
          DolObject obj = null;

          float offsetX = 0.0f;
          float offsetY = 0.0f;

          if (FieldMap[i, j] == null)
          {
            continue;
          }

          dolObjects = FieldMap[i, j].Split('-');

          for (int k = 0; k < dolObjects.Length; k++)
          {
            string dolObj = dolObjects[k].Split('|')[0];
            float dir = 0;

            if (dolObjects[k].Split('|').Length > 1)
            {
              dir = float.Parse(dolObjects[k].Split('|')[1]) * -90;
            }

            switch (dolObj)
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
                obj = (Instantiate(Resources.Load("Prefab/Trap")) as GameObject).GetComponent<Trap>();

                if (dir == 0)
                {
                  offsetY = -0.225f;
                }
                else if (dir == -270.0f)
                {
                  offsetX = 0.225f;
                }
                else if (dir == -90.0f)
                {
                  offsetX = -0.225f;
                }

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
                obj = (Instantiate(Resources.Load("Prefab/Lock")) as GameObject).GetComponent<Lock>();
                LockManager.Instance.AddLock(obj as Lock);
                break;

              case "10":
                obj = (Instantiate(Resources.Load("Prefab/MovingPlatform")) as GameObject).GetComponent<MovingPlatform>();
                break;

              case "11":
                obj = (Instantiate(Resources.Load("Prefab/CrossTile")) as GameObject).GetComponent<CrossTile>();
                GameManager.Instance.ArrStaticRotated.Add(obj);
                break;

              default:
                continue;
            }

            // obj.transform.position = new Vector2(StartPosition.x + (j - 1) * TileInterval, StartPosition.y + (10 + 2 - i - 1 - 1) * TileInterval);

            decimal x = (decimal)StartPosition.x + (decimal)((j - 1) * (decimal)TileInterval);
            decimal y = (decimal)StartPosition.y + (decimal)((10 + 2 - i - 1 - 1) * (decimal)TileInterval);

            float floatedX = (float)x;
            float floatedY = (float)y;

            obj.transform.position = new Vector2((float)x + offsetX, (float)y + offsetY);
            obj.transform.eulerAngles = new Vector3(obj.transform.eulerAngles.x, obj.transform.eulerAngles.y, dir);

            // 미니필드 인덱스 계산 필요한 오브젝트들 델리게이트 추가
            if (obj.GetIsStaticObject() != true)
            {
              GameManager.Instance.ArrCalcIndex.Add(obj);
            }

            // 회전시 고정시킬 필요 없는 객체들 
            if (obj.GetIsFixNeeded() != true)
            {
              obj.transform.SetParent(transform);
            }
            else
            {
              GameManager.Instance.ArrFixNeeded.Add(obj);
            }

            // 회전후 다시 돌려놔야 할 객체들
            if (obj.GetIsReRotateNeeded() == true)
            {
              GameManager.Instance.ArrReRotateNeeded.Add(obj);
            }
          }
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

    foreach (DolObject obj in GameManager.Instance.ArrFixNeeded)
    {
      obj.FixDolObject(transform, true);
    }

    while (t < duration)
    {
      t += Time.deltaTime;
      float zRoation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;
      transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, zRoation);

      // Static
      foreach (DolObject obj in GameManager.Instance.ArrStaticRotated)
      {
        obj.transform.eulerAngles = new Vector3(obj.transform.eulerAngles.x, obj.transform.eulerAngles.y, 0);
      }

      yield return null;
    }

    foreach (DolObject obj in GameManager.Instance.ArrReRotateNeeded)
    {
      obj.ResetRotation();
    }

    // MainField.GetCurrentPlayer().FixDolObject(null, false);

    foreach (DolObject obj in GameManager.Instance.ArrFixNeeded)
    {
      obj.FixDolObject(null, false);
    }

    transform.parent.GetComponent<Field>().RegenerateCollider();

    // 회전수 차감
    transform.parent.GetComponent<Field>().SubstractCurrentRotate();

    if (transform.parent.GetComponent<Field>().GetCurrentRotateNumber() < 0 && SceneManager.GetActiveScene().buildIndex != 0)
    {
      GameManager.Instance.starCount = 0;
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
      GameManager.Instance.SetIsReloading(true);
      GameManager.Instance.SetCurrentStageName(GameManager.Instance.GetCurrentStageName());

      yield break;
    }

    if (UIManger.Instance)
    {
      // 회전수 세팅
      UIManger.Instance.SetRotateNum(transform.parent.GetComponent<Field>().GetCurrentRotateNumber());
    }

    LockManager.Instance.CheckLocks();

    GameManager.Instance.SetIsRotating(false);

    yield break;
  }
}
