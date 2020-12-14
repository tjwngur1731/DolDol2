using UnityEditor;
using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System;

public class MapEditor : EditorWindow
{
  public Rect windowRect = new Rect(100, 100, 200, 200);
  private Vector3 mousepos;
  private string[,] fieldMap = null;
  private GameObject[,] baseObjectFieldMap = null;
  private float TileInterval = 0.9f;
  private float minifildInterval = 9.0f;
  private GameObject root = null;
  private GameObject[,] minifield = null;
  private int currentSelectedI = -1;
  private int currentSelectedJ = -1;
  private GameObject currentSelectedObject = null;
  private string type = "";
  private Vector2 minifieldNumber = new Vector2(5, 5);
  private Vector2 minifieldSize = new Vector2(10, 10);
  private string loadFileName = "";
  private string saveFileName = "";
  private int editorType = 0;
  private int dolObjectDir = -1;
  private string strRotateNumber = "";
  private int rotateNumber = 0;
  void OnGUI()
  {

    BeginWindows();

    GUILayout.Label("Palyer", GUILayout.Width(50), GUILayout.Height(30));
    GUILayout.BeginHorizontal("");

    // var player1Tex = ((Resources.Load("Prefab/Player 1") as GameObject).GetComponent<SpriteRenderer>()).sprite.texture;
    if (GUILayout.Button("P1", GUILayout.Width(50), GUILayout.Height(40)))
    {
      type = "P1";
      editorType = 0;
    }
    // GUILayout.Label("Player1");

    // var player2Tex = ((Resources.Load("Prefab/Player 2") as GameObject).GetComponent<SpriteRenderer>()).sprite.texture;
    if (GUILayout.Button("P2", GUILayout.Width(50), GUILayout.Height(40)))
    {
      type = "P2";
      editorType = 0;
    }
    // GUILayout.Label("Player2");

    GUILayout.EndHorizontal();

    GUILayout.Label("DolObject", GUILayout.Width(100), GUILayout.Height(30));
    GUILayout.BeginHorizontal("");
    var wallTex = ((Resources.Load("Prefab/Wall") as GameObject).GetComponent<SpriteRenderer>()).sprite.texture;
    if (GUILayout.Button(wallTex, GUILayout.Width(50), GUILayout.Height(40)))
    {
      type = "1";
      editorType = 0;
    }
    // GUILayout.Label("Wall");

    if (GUILayout.Button("Star", GUILayout.Width(50), GUILayout.Height(40)))
    {
      type = "7";
      editorType = 0;
    }

    if (GUILayout.Button("Trap", GUILayout.Width(50), GUILayout.Height(40)))
    {
      type = "4";
      editorType = 0;
    }

    if (GUILayout.Button("Portal", GUILayout.Width(50), GUILayout.Height(40)))
    {
      type = "0";
      editorType = 0;
    }

    if (GUILayout.Button("Box", GUILayout.Width(50), GUILayout.Height(40)))
    {
      type = "5";
      editorType = 0;
    }
    GUILayout.EndHorizontal();

    GUILayout.BeginHorizontal("");
    if (GUILayout.Button("Key", GUILayout.Width(50), GUILayout.Height(40)))
    {
      type = "8";
      editorType = 0;
    }

    if (GUILayout.Button("Lock", GUILayout.Width(50), GUILayout.Height(40)))
    {
      type = "9";
      editorType = 0;
    }

    if (GUILayout.Button("M-Platform", GUILayout.Width(100), GUILayout.Height(40)))
    {
      type = "10";
      editorType = 0;
    }

    if (GUILayout.Button("M-Platform", GUILayout.Width(50), GUILayout.Height(40)))
    {
      type = "11";
      editorType = 0;
    }

    GUILayout.EndHorizontal();

    GUILayout.Label("Rotation", GUILayout.Width(50), GUILayout.Height(30));
    GUILayout.BeginHorizontal("");
    if (GUILayout.Button("Turn Left", GUILayout.Width(70), GUILayout.Height(30)))
    {
      if (!currentSelectedObject)
      {
        return;
      }

      dolObjectDir--;

      if (dolObjectDir < 0)
      {
        dolObjectDir = 3;
      }

      string obj = fieldMap[currentSelectedI, currentSelectedJ].Split('|')[0];
      currentSelectedObject.transform.eulerAngles = new Vector3(currentSelectedObject.transform.eulerAngles.x, currentSelectedObject.transform.eulerAngles.y, dolObjectDir * -90.0f);

      if (obj == "4")
      {
        float offsetX = 0.0f;
        float offsetY = 0.0f;

        float dir = dolObjectDir * -90.0f;

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

        currentSelectedObject.transform.position = new Vector3((currentSelectedJ - 1) * TileInterval + offsetX, (currentSelectedI - 1) * TileInterval + offsetY, 0.0f);
      }

      fieldMap[currentSelectedI, currentSelectedJ] = obj + "|" + dolObjectDir;
    }

    if (GUILayout.Button("Turn Right", GUILayout.Width(70), GUILayout.Height(30)))
    {
      if (!currentSelectedObject)
      {
        return;
      }

      dolObjectDir++;

      if (dolObjectDir > 3)
      {
        dolObjectDir = 0;
      }

      string obj = fieldMap[currentSelectedI, currentSelectedJ].Split('|')[0];
      currentSelectedObject.transform.eulerAngles = new Vector3(currentSelectedObject.transform.eulerAngles.x, currentSelectedObject.transform.eulerAngles.y, dolObjectDir * -90.0f);

      if (obj == "4")
      {
        float offsetX = 0.0f;
        float offsetY = 0.0f;

        float dir = dolObjectDir * -90.0f;

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

        currentSelectedObject.transform.position = new Vector3((currentSelectedJ -1) * TileInterval + offsetX, (currentSelectedI -1) * TileInterval + offsetY, 0.0f);
      }

      fieldMap[currentSelectedI, currentSelectedJ] = obj + "|" + dolObjectDir;
    }
    GUILayout.EndHorizontal();

    GUILayout.Label("Editor Mode", GUILayout.Width(100), GUILayout.Height(30));
    GUILayout.BeginHorizontal("");
    if (GUILayout.Button("Edit", GUILayout.Width(50), GUILayout.Height(40)))
    {
      editorType = 1;
    }

    if (GUILayout.Button("Del", GUILayout.Width(50), GUILayout.Height(40)))
    {
      editorType = 2;
      type = " ";
    }

    if (GUILayout.Button("Clear", GUILayout.Width(50), GUILayout.Height(40)))
    {
      ClearMap();
    }
    GUILayout.EndHorizontal();

    strRotateNumber = EditorGUILayout.TextField("RotateNumber", strRotateNumber);

    if (GUILayout.Button("R Num", GUILayout.Width(50), GUILayout.Height(40)))
    {
      rotateNumber = Int32.Parse(strRotateNumber);
    }
    GUILayout.Label("Save & Load", GUILayout.Width(150), GUILayout.Height(30));
    saveFileName = EditorGUILayout.TextField("Save File Name", saveFileName);

    if (GUILayout.Button("Save", GUILayout.Width(50), GUILayout.Height(40)))
    {
      SaveToFile(saveFileName);
    }

    loadFileName = EditorGUILayout.TextField("Load File Name", loadFileName);
    if (GUILayout.Button("Load", GUILayout.Width(50), GUILayout.Height(40)))
    {
      ClearMap();
      ReadFromFile(loadFileName);
    }

    EndWindows();
  }

  void ClearMap()
  {
    if (root != null)
    {
      DestroyImmediate(root);
    }

    root = new GameObject();
    root.name = "MapEditorRoot";

    int indexI = (int)minifieldNumber.y;
    int indexJ = (int)minifieldNumber.y;

    minifield = new GameObject[indexI, indexJ];

    for (int i = 0; i < indexI; i++)
    {
      for (int j = 0; j < indexJ; j++)
      {
        if (minifield[i, j] != null)
        {
          DestroyImmediate(minifield[i, j]);
        }

        minifield[i, j] = new GameObject();
        minifield[i, j].name = "minifield_" + i + "_" + j;
        minifield[i, j].transform.position = new Vector2(j * 5 * TileInterval, i * 5 * TileInterval);
        minifield[i, j].transform.SetParent(root.transform);
      }
    }

    if (fieldMap == null)
    {
      int rangeI = (int)(minifieldNumber.y * minifieldSize.y);
      int rangeJ = (int)(minifieldNumber.x * minifieldSize.x);

      fieldMap = new string[rangeI + 2, rangeJ + 2];

      for (int i = 0; i < rangeI + 2; i++)
      {
        for (int j = 0; j < rangeJ + 2; j++)
        {
          fieldMap[i, j] = " ";
        }
      }
    }

    if (baseObjectFieldMap == null)
    {
      int rangeI = (int)(minifieldNumber.y * minifieldSize.y);
      int rangeJ = (int)(minifieldNumber.x * minifieldSize.x);

      baseObjectFieldMap = new GameObject[rangeI + 2, rangeJ + 2];

      for (int i = 0; i < rangeI + 2; i++)
      {
        for (int j = 0; j < rangeJ + 2; j++)
        {
          baseObjectFieldMap[i, j] = null;
        }
      }
    }

  }

  void ReadFromFile(string stage)
  {
    string filename = "CellMap/" + stage + ".csv";

    using (FileStream fs = new FileStream(filename, FileMode.Open))
    {
      using (StreamReader sr = new StreamReader(fs, Encoding.UTF8, false))
      {
        string lines = null;
        string[] values = null;
        string[] dolObjects = null;

        int i = 50 - 1;

        while ((lines = sr.ReadLine()) != null)
        {
          values = lines.Split(',');

          GameObject obj = null;

          for (int j = 0; j < values.Length; j++)
          {
            if (values[j] == "R")
            {
              strRotateNumber = values[j + 1];
              rotateNumber = Int32.Parse(values[j + 1]);
              continue;
            }

            dolObjects = values[j].Split('-');

            for (int k = 0; k < dolObjects.Length; k++)
            {
              string dolObj = dolObjects[k].Split('|')[0];
              float dir = 0;

              float offsetX = 0.0f;
              float offsetY = 0.0f;

              if (dolObjects[k].Split('|').Length > 1)
              {
                dir = float.Parse(dolObjects[k].Split('|')[1]) * -90;
              }

              switch (dolObj)
              {
                case "P1":
                  obj = Instantiate(Resources.Load("Prefab/Player 1")) as GameObject;
                  break;

                case "P2":
                  obj = Instantiate(Resources.Load("Prefab/Player 2")) as GameObject;
                  break;

                case "0":
                  obj = Instantiate(Resources.Load("Prefab/Portal")) as GameObject;
                  break;

                case "1":
                  obj = Instantiate(Resources.Load("Prefab/Wall")) as GameObject;
                  break;

                case "4":
                  obj = Instantiate(Resources.Load("Prefab/Trap")) as GameObject;

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
                  obj = Instantiate(Resources.Load("Prefab/Box")) as GameObject;
                  break;

                case "7":
                  obj = Instantiate(Resources.Load("Prefab/Star")) as GameObject;
                  break;

                case "8":
                  obj = Instantiate(Resources.Load("Prefab/Key")) as GameObject;
                  break;

                case "9":
                  obj = Instantiate(Resources.Load("Prefab/Lock")) as GameObject;
                  break;

                case "10":
                  obj = Instantiate(Resources.Load("Prefab/MovingPlatform")) as GameObject;
                  break;

                case "11":
                  obj = Instantiate(Resources.Load("Prefab/CrossTile")) as GameObject;
                  break;

                default:
                  continue;
              }

              if (obj == null)
              {
                continue;
              }

              obj.transform.position = new Vector3(j * TileInterval + offsetX, i * TileInterval + offsetY, 0.0f);
              obj.transform.eulerAngles = new Vector3(obj.transform.eulerAngles.x, obj.transform.eulerAngles.y, dir);

              if (k <= 0)
              {
                fieldMap[i + 1, j + 1] = values[j];
                baseObjectFieldMap[i + 1, j + 1] = obj;

                int indexI = i / (int)minifieldSize.y;
                int indexJ = j / (int)minifieldSize.x;

                obj.transform.SetParent(minifield[indexI, indexJ].transform);
              }
              else if (k > 0)
              {
                fieldMap[i + 1, j + 1] += '-' + type;
                obj.transform.SetParent(baseObjectFieldMap[i + 1, j + 1].transform);
              }
            }
          }

          i--;
        }
      }
    }
  }

  void DrawMinifieldGrid()
  {
    if (minifield == null)
    {
      return;
    }

    float length = minifieldNumber.x * minifieldSize.x * TileInterval;

    Vector3 up = new Vector3(0, length, 0);
    Vector3 right = new Vector3(length, 0, 0);

    for (int i = 0; i < minifieldNumber.x + 1; i++)
    {
      Vector3 yIntervalVec = new Vector3(0, i * minifieldSize.y * TileInterval, 0) - new Vector3(TileInterval / 2, TileInterval / 2, 0); ;
      Vector3 xIntervalVec = new Vector3(i * minifieldSize.y * TileInterval, 0, 0) - new Vector3(TileInterval / 2, TileInterval / 2, 0); ;

      Debug.DrawLine(yIntervalVec, right + yIntervalVec, new Color(1.0f, 0.0f, 0.0f));
      Debug.DrawLine(xIntervalVec, up + xIntervalVec, new Color(1.0f, 0.0f, 0.0f));
    }
  }

  void DrawTileGrid()
  {
    if (fieldMap == null)
    {
      return;
    }

    float length = minifieldNumber.x * minifieldSize.x * TileInterval;

    Vector3 up = new Vector3(0, length, 0);
    Vector3 right = new Vector3(length, 0, 0);

    int rangeJ = (int)(minifieldNumber.x * minifieldSize.x);

    for (int i = 0; i < rangeJ; i++)
    {
      Vector3 yIntervalVec = new Vector3(0, i * TileInterval, 0) - new Vector3(TileInterval / 2, TileInterval / 2, 0); ;
      Vector3 xIntervalVec = new Vector3(i * TileInterval, 0, 0) - new Vector3(TileInterval / 2, TileInterval / 2, 0); ;

      Debug.DrawLine(yIntervalVec, right + yIntervalVec, new Color(1.0f, 1.0f, 0.0f));
      Debug.DrawLine(xIntervalVec, up + xIntervalVec, new Color(1.0f, 1.0f, 0.0f));
    }
  }

  void OnEnable()
  {
    SceneView.duringSceneGui += OnSceneGUI;
  }

  void OnDisable()
  {
    SceneView.duringSceneGui -= OnSceneGUI;
  }

  void OnDestory()
  {
    DestroyImmediate(root);
  }

  void SaveToFile(string stage)
  {
    int rangeI = (int)(minifieldNumber.y * minifieldSize.y);
    int rangeJ = (int)(minifieldNumber.x * minifieldSize.x);

    string filename = "CellMap/" + stage + ".csv";

    using (StreamWriter outputFile = new StreamWriter(@filename))
    {
      // for (int i = 1; i <= rangeI; i++)
      for (int i = rangeI; i >= 1; i--)
      {
        string line = "";

        for (int j = 1; j <= rangeJ; j++)
        {
          // Debug.Log(fieldMap[i + 1,j + 1]);
          string value = fieldMap[i, j];

          Debug.Log(fieldMap[i, j]);
          // line += ("{" + value + "}");
          line += value;

          if (j != rangeJ)
          {
            line += ",";
          }
        }

        outputFile.WriteLine(line);
      }

      outputFile.WriteLine("R" + "," + rotateNumber);
    }
  }

  void OnSceneGUI(SceneView sceneView)
  {
    DrawTileGrid();
    DrawMinifieldGrid();

    if (Application.isPlaying)
    {
      if (root.activeInHierarchy == true)
      {
        root.SetActive(false);
      }
    }
    else if (Application.isEditor)
    {
      Event current = Event.current;

      if (root == null)
      {
        root = new GameObject();
        root.name = "MapEditorRoot";
      }
      else
      {
        if (root.activeInHierarchy == false)
        {
          root.SetActive(true);
        }
      }

      if (minifield == null)
      {
        int indexI = (int)minifieldNumber.y;
        int indexJ = (int)minifieldNumber.y;

        minifield = new GameObject[indexI, indexJ];

        for (int i = 0; i < indexI; i++)
        {
          for (int j = 0; j < indexJ; j++)
          {
            minifield[i, j] = new GameObject();
            minifield[i, j].name = "minifield_" + i + "_" + j;
            minifield[i, j].transform.position = new Vector2(j * 5 * TileInterval, i * 5 * TileInterval);
            minifield[i, j].transform.SetParent(root.transform);
          }
        }
      }

      if (fieldMap == null)
      {
        int rangeI = (int)(minifieldNumber.y * minifieldSize.y);
        int rangeJ = (int)(minifieldNumber.x * minifieldSize.x);

        fieldMap = new string[rangeI + 2, rangeJ + 2];

        for (int i = 0; i < rangeI + 2; i++)
        {
          for (int j = 0; j < rangeJ + 2; j++)
          {
            fieldMap[i, j] = " ";
          }
        }
      }

      if (baseObjectFieldMap == null)
      {
        int rangeI = (int)(minifieldNumber.y * minifieldSize.y);
        int rangeJ = (int)(minifieldNumber.x * minifieldSize.x);

        baseObjectFieldMap = new GameObject[rangeI + 2, rangeJ + 2];

        for (int i = 0; i < rangeI + 2; i++)
        {
          for (int j = 0; j < rangeJ + 2; j++)
          {
            baseObjectFieldMap[i, j] = null;
          }
        }
      }

      Camera cam = SceneView.lastActiveSceneView.camera;

      mousepos = current.mousePosition;
      mousepos.y = Screen.height - mousepos.y - 36.0f;
      mousepos = cam.ScreenToWorldPoint(mousepos);
      mousepos.z = 0;

      if (current.type == EventType.MouseDown && current.button == 0)
      {
        if (editorType == 0 || editorType == 2)
        {
          if (type != " ")
          {
            AddObject(mousepos);
          }
          else
          {
            DeleteObject(mousepos);
          }
        }
        else if (editorType == 1)
        {
          EditObject(mousepos);
        }
      }
      
    }
  }

  void EditObject(Vector3 pos)
  {
    int indexJ = (int)(Math.Round(pos.x) / minifildInterval);
    int indexI = (int)(Math.Round(pos.y) / minifildInterval);

    if (!((indexJ >= 0 && indexJ <= 5) && (indexI >= 0 && indexI <= 5)))
    {
      return;
    }

    pos.x = (int)((pos.x + TileInterval / 2) / TileInterval) * TileInterval;
    pos.y = (int)((pos.y + TileInterval / 2) / TileInterval) * TileInterval;

    currentSelectedI = ((int)Math.Round(pos.y / TileInterval) + 1);
    currentSelectedJ = ((int)Math.Round(pos.x / TileInterval) + 1);

    currentSelectedObject = baseObjectFieldMap[currentSelectedI, currentSelectedJ];

    if (fieldMap[currentSelectedI, currentSelectedJ].Split('|').Length > 1)
    {
      dolObjectDir = Int32.Parse(fieldMap[currentSelectedI, currentSelectedJ].Split('|')[1]);
    }
    else
    {
      dolObjectDir = 0;
    }
  }

  void DeleteObject(Vector3 pos)
  {
    int indexJ = (int)(Math.Round(pos.x) / minifildInterval);
    int indexI = (int)(Math.Round(pos.y) / minifildInterval);

    if (!((indexJ >= 0 && indexJ <= 5) && (indexI >= 0 && indexI <= 5)))
    {
      return;
    }

    int fieldIndexI = ((int)Math.Round(pos.y / TileInterval) + 1);
    int fieldIndexJ = ((int)Math.Round(pos.x / TileInterval) + 1);

    if (baseObjectFieldMap[fieldIndexI, fieldIndexJ])
    {
      baseObjectFieldMap[fieldIndexI, fieldIndexJ].transform.SetParent(null);
      DestroyImmediate(baseObjectFieldMap[fieldIndexI, fieldIndexJ]);
      fieldMap[fieldIndexI, fieldIndexJ] = " ";
    }
  }

  void AddObject(Vector3 pos)
  {
    int indexJ = (int)(Math.Round(pos.x) / minifildInterval);
    int indexI = (int)(Math.Round(pos.y) / minifildInterval);

    if (!((indexJ >= 0 && indexJ <= 5) && (indexI >= 0 && indexI <= 5)))
    {
      return;
    }

    pos.x = (int)((pos.x + TileInterval / 2) / TileInterval) * TileInterval;
    pos.y = (int)((pos.y + TileInterval / 2) / TileInterval) * TileInterval;

    int fieldIndexI = ((int)Math.Round(pos.y / TileInterval) + 1);
    int fieldIndexJ = ((int)Math.Round(pos.x / TileInterval) + 1);

    GameObject existObj = null;

    if (fieldMap[fieldIndexI, fieldIndexJ] != " ")
    {
      if ((type == "5" || type == "9") && (fieldMap[fieldIndexI, fieldIndexJ] == "7" ||
        fieldMap[fieldIndexI, fieldIndexJ] == "7"))
      {
        existObj = baseObjectFieldMap[fieldIndexI, fieldIndexJ];
      }
      else
      {
        fieldMap[fieldIndexI, fieldIndexJ] = " ";

        GameObject current = baseObjectFieldMap[fieldIndexI, fieldIndexJ];
        baseObjectFieldMap[fieldIndexI, fieldIndexJ] = null;

        current.transform.SetParent(null);

        DestroyImmediate(current);
      }
    }

    GameObject obj = null;

    switch (type)
    {
      case "P1":
        obj = Instantiate(Resources.Load("Prefab/Player 1")) as GameObject;
        break;

      case "P2":
        obj = Instantiate(Resources.Load("Prefab/Player 2")) as GameObject;
        break;

      case "0":
        obj = Instantiate(Resources.Load("Prefab/Portal")) as GameObject;
        break;

      case "1":
        obj = Instantiate(Resources.Load("Prefab/Wall")) as GameObject;
        break;

      case "4":
        obj = Instantiate(Resources.Load("Prefab/Trap")) as GameObject;
        break;

      case "5":
        obj = Instantiate(Resources.Load("Prefab/Box")) as GameObject;
        break;

      case "7":
        obj = Instantiate(Resources.Load("Prefab/Star")) as GameObject;
        break;

      case "8":
        obj = Instantiate(Resources.Load("Prefab/Key")) as GameObject;
        break;

      case "9":
        obj = Instantiate(Resources.Load("Prefab/Lock")) as GameObject;
        break;

      case "10":
        obj = Instantiate(Resources.Load("Prefab/MovingPlatform")) as GameObject;
        break;

      case "11":
        obj = Instantiate(Resources.Load("Prefab/CrossTile")) as GameObject;
        break;
    }

    if (obj == null)
    {
      return;
    }

    //string dirStr = "";

    //if (dolObjectDir != 0)
    //{
    //  dirStr = "|" + dolObjectDir;
    //}

    //fieldMap[fieldIndexI, fieldIndexJ] = type + dirStr;
    if (existObj)
    {
      fieldMap[fieldIndexI, fieldIndexJ] += '-' + type;
      obj.transform.SetParent(existObj.transform);
    }
    else
    {
      fieldMap[fieldIndexI, fieldIndexJ] = type;
      baseObjectFieldMap[fieldIndexI, fieldIndexJ] = obj;
      obj.transform.SetParent(minifield[indexI, indexJ].transform);
    }

    obj.transform.position = pos;
    
  }

  [MenuItem("CustomEditor/MapEditor")]
  static void Init()
  {
    EditorWindow.GetWindow(typeof(MapEditor));
  }
}