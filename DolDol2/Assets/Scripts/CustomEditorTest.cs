using UnityEditor;
using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System;

public class CustomEditorTest : EditorWindow
{
  public Rect windowRect = new Rect(100, 100, 200, 200);
  private Vector3 mousepos;
  private string[,] fieldMap = null;
  private GameObject[,] baseObjectFieldMap = null;
  private bool isActive = false;
  private float TileInterval = 1.8f;
  private float minifildInterval = 18.0f;
  private GameObject root = null;
  private GameObject[,] minifield = null;
  private string type = "";
  private bool drawLine = true;
  private Vector2 minifieldNumber = new Vector2(5, 5);
  private Vector2 minifieldSize = new Vector2(10, 10);
  private string loadFileName = "";
  private string saveFileName = "";
  void OnGUI()
  {

    BeginWindows();

    var wallTex = ((Resources.Load("Prefab/Wall") as GameObject).GetComponent<SpriteRenderer>()).sprite.texture;
    if (GUILayout.Button(wallTex, GUILayout.Width(50), GUILayout.Height(40)))
    {
      type = "1";
    }
    // GUILayout.Label("Wall");

    // var player1Tex = ((Resources.Load("Prefab/Player 1") as GameObject).GetComponent<SpriteRenderer>()).sprite.texture;
    if (GUILayout.Button("1P", GUILayout.Width(50), GUILayout.Height(40)))
    {
      type = "1P";
    }
    // GUILayout.Label("Player1");

    // var player2Tex = ((Resources.Load("Prefab/Player 2") as GameObject).GetComponent<SpriteRenderer>()).sprite.texture;
    if (GUILayout.Button("2P", GUILayout.Width(50), GUILayout.Height(40)))
    {
      type = "2P";
    }
    // GUILayout.Label("Player2");

    if (GUILayout.Button("Star", GUILayout.Width(50), GUILayout.Height(40)))
    {
      type = "7";
    }

    if (GUILayout.Button("Portal", GUILayout.Width(50), GUILayout.Height(40)))
    {
      type = "5";
    }

    if (GUILayout.Button("Del", GUILayout.Width(50), GUILayout.Height(40)))
    {
      type = " ";
    }

    if (GUILayout.Button("Clear", GUILayout.Width(50), GUILayout.Height(40)))
    {
      ClearMap();
    }

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
    int rangeI = (int)(minifieldNumber.y * minifieldSize.y);
    int rangeJ = (int)(minifieldNumber.x * minifieldSize.x);

    for (int i = 0; i < rangeI + 2; i++)
    {
      for (int j = 0; j < rangeJ + 2; j++)
      {
        if (baseObjectFieldMap[i, j] == null)
        {
          continue;
        }

        baseObjectFieldMap[i, j].transform.SetParent(null);
        DestroyImmediate(baseObjectFieldMap[i, j]);
        fieldMap[i, j] = " ";
      }
    }
  }

  void Update()
  {
    if (drawLine == true)
    {
      DrawTileGrid();
      DrawMinifieldGrid();
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

        int i = 50 - 1;

        while ((lines = sr.ReadLine()) != null)
        {
          values = lines.Split(',');

          GameObject obj = null;

          for (int j = 0; j < values.Length; j++)
          {
            switch (values[j])
            {
              case "1P":
                obj = Instantiate(Resources.Load("Prefab/Player 1")) as GameObject;
                break;

              case "2P":
                obj = Instantiate(Resources.Load("Prefab/Player 2")) as GameObject;
                break;

              case "1":
                obj = Instantiate(Resources.Load("Prefab/Wall")) as GameObject;
                break;

              case "5":
                obj = Instantiate(Resources.Load("Prefab/Portal")) as GameObject;
                break;

              case "7":
                obj = Instantiate(Resources.Load("Prefab/Star")) as GameObject;
                break;

                default:
                continue;
            }

            if (obj == null)
            {
              continue;
            }

            fieldMap[i + 1, j + 1] = values[j];
            baseObjectFieldMap[i + 1, j + 1] = obj;

            obj.transform.position = new Vector3(j * TileInterval, i * TileInterval, 0.0f);

            int indexI = i / (int)minifieldSize.y;
            int indexJ = j / (int)minifieldSize.x;

            obj.transform.SetParent(minifield[indexI, indexJ].transform);
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

    for (int i = 0; i < 5; i++)
      {
        for (int j = 0; j < 5; j++)
        {
          Vector3 pos = new Vector3(j * minifieldSize.x * TileInterval, i * minifieldSize.y * TileInterval, 0) - new Vector3(TileInterval / 2, TileInterval / 2, 0);
          Vector3 dest = new Vector3((j + 1) * minifieldSize.x * TileInterval, (i + 1) * minifieldSize.y * TileInterval, 0) - new Vector3(TileInterval / 2, TileInterval / 2, 0);
            
          Debug.DrawLine(pos, new Vector3(dest.x, pos.y, 0), new Color(1.0f, 0.0f, 0.0f));
          Debug.DrawLine(pos, new Vector3(pos.x, dest.y, 0), new Color(1.0f, 0.0f, 0.0f));

          Debug.DrawLine(new Vector3(dest.x, pos.y, 0), dest, new Color(1.0f, 0.0f, 0.0f));
          Debug.DrawLine(new Vector3(pos.x, dest.y, 0), dest, new Color(1.0f, 0.0f, 0.0f));
        }
      }
  }
  
  void DrawTileGrid()
  {
    if (fieldMap == null)
    {
      return;
    }

    int rangeI = (int)(minifieldNumber.y * minifieldSize.y);
    int rangeJ = (int)(minifieldNumber.x * minifieldSize.x);
    
    for (int i = 0; i < rangeI; i++)
    {
      for (int j = 0; j < rangeJ; j++)
      {
        Vector3 pos = new Vector3(j * TileInterval, i * TileInterval, 0) - new Vector3(TileInterval / 2, TileInterval / 2, 0);
        Vector3 dest = new Vector3((j + 1) * TileInterval, (i + 1) * TileInterval, 0) - new Vector3(TileInterval / 2, TileInterval / 2, 0);

        Debug.DrawLine(pos, new Vector3(dest.x, pos.y, 0), new Color(1.0f, 1.0f, 0.0f));
        Debug.DrawLine(pos, new Vector3(pos.x, dest.y, 0), new Color(1.0f, 1.0f, 0.0f));

        Debug.DrawLine(new Vector3(dest.x, pos.y, 0), dest, new Color(1.0f, 1.0f, 0.0f));
        Debug.DrawLine(new Vector3(pos.x, dest.y, 0), dest, new Color(1.0f, 1.0f, 0.0f));
      }
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
    Destroy(root);
  }

  void OnFocus()
  {
    isActive = true;
  }

  void OnLostFocus()
  {
    isActive = false;
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
    }
  }

  void OnSceneGUI(SceneView sceneView)
  {
    // if (isActive != true)
    // {
    //     return;
    // }
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
        if (type != " ")
        {
          AddObject(mousepos);
        }
        else
        {
          DeleteObject(mousepos);
        }
      }
    }
  }

  void DeleteObject(Vector3 pos)
  {
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

    if (fieldMap[fieldIndexI, fieldIndexJ] != " ")
    {
      fieldMap[fieldIndexI, fieldIndexJ] = " ";
      
      GameObject current = baseObjectFieldMap[fieldIndexI, fieldIndexJ];
      baseObjectFieldMap[fieldIndexI, fieldIndexJ] = null;

      current.transform.SetParent(null);

      DestroyImmediate(current);
    }

    GameObject obj = null;

    switch (type)
    {
      case "1P":
        obj = Instantiate(Resources.Load("Prefab/Player 1")) as GameObject;
        break;

      case "2P":
        obj = Instantiate(Resources.Load("Prefab/Player 2")) as GameObject;
        break;

      case "1":
        obj = Instantiate(Resources.Load("Prefab/Wall")) as GameObject;
        break;

      case "5":
        obj = Instantiate(Resources.Load("Prefab/Portal")) as GameObject;
        break;

      case "7":
        obj = Instantiate(Resources.Load("Prefab/Star")) as GameObject;
        break;
    }

    if (obj == null)
    {
      return;
    }

    fieldMap[fieldIndexI, fieldIndexJ] = type;
    baseObjectFieldMap[fieldIndexI, fieldIndexJ] = obj;

    obj.transform.position = pos;
    obj.transform.SetParent(minifield[indexI, indexJ].transform);
  }

  [MenuItem("MapEditor/CustomEditorTest")]
  static void Init()
  {
    EditorWindow.GetWindow(typeof(CustomEditorTest));
  }
}