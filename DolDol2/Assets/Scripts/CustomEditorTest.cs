using UnityEditor;
using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class CustomEditorTest : EditorWindow {
    public Rect windowRect = new Rect(100, 100, 200, 200);
    private Vector3 mousepos;
    private int[,] fieldMap = null;
    private bool isActive = false;
    private float TileInterval = 1.8f;
    private float minifildInterval = 9.0f;
    private GameObject root = null;
    private GameObject[,] minifield = null;
    private int type = -1;
    void OnGUI() {

        BeginWindows();
        
        if (GUILayout.Button("", GUILayout.Width(50), GUILayout.Height(40)))
        {
            type = 2;
        }
        GUILayout.Label("Wall");

        if (GUILayout.Button("", GUILayout.Width(50), GUILayout.Height(40)))
        {
            type = 0;
        }
        GUILayout.Label("Player1");
        
        if (GUILayout.Button("", GUILayout.Width(50), GUILayout.Height(40)))
        {
            type = 1;
        }
        GUILayout.Label("Player2");

        if (GUILayout.Button("Save", GUILayout.Width(50), GUILayout.Height(40)))
        {
            SaveToFile("SaveTest");
        }

        GUILayout.Label("Wall");

        EndWindows();
    }

    void OnEnable() {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    void OnDisable() {
        SceneView.duringSceneGui -= OnSceneGUI;
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
        string filename = "CellMap/" + stage + ".cellmap";

        using (StreamWriter outputFile = new StreamWriter(@filename))
        {
            for (int i = 25; i >= 1; i--)
            {
                string line = "";
                for (int j = 1; j <= 25; j++)
                {
                    // Debug.Log(fieldMap[i + 1,j + 1]);
                    int ch = fieldMap[i,j];
                    if (ch == ' ')
                    {
                        line += (char)ch;
                    }
                    else 
                    {
                        line += ch;
                    }
                }

                outputFile.WriteLine(line);
            }
        }
    }

    void OnSceneGUI(SceneView sceneView) {
        // if (isActive != true)
        // {
        //     return;
        // }

        Event current = Event.current;

        if (root == null)
        {
            root = new GameObject();
            root.name = "MapEditorRoot";
        }

        if (minifield == null)
        {
            minifield = new GameObject[5, 5];

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    minifield[i,j] = new GameObject();
                    minifield[i,j].name = "minifield_" + i + "_" + j;
                    minifield[i,j].transform.position = new Vector2(j * 5 * TileInterval, i * 5 * TileInterval);
                    minifield[i,j].transform.SetParent(root.transform);
                }   
            }
        }

        if (fieldMap == null)
        {
            fieldMap = new int[27,27];

            for (int i = 0; i < 27; i++)
            {
                for (int j = 0; j < 27; j++)
                {
                    fieldMap[i,j] = ' ';
                }   
            }
        }

        if (current.type == EventType.MouseDown && current.button == 0) {
            Camera cam = SceneView.lastActiveSceneView.camera;
 
            mousepos = current.mousePosition;
            mousepos.y = Screen.height - mousepos.y - 36.0f;
            mousepos = cam.ScreenToWorldPoint (mousepos);
            mousepos.z = 0;

            AddObject(mousepos);
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

        GameObject obj = null;

        switch(type)
        {
            case 0:
                obj = Instantiate(Resources.Load("Prefab/Player 1")) as GameObject;
            break;

            case 1:
                obj = Instantiate(Resources.Load("Prefab/Player 2")) as GameObject;
            break;

            case 2:
                obj = Instantiate(Resources.Load("Prefab/Wall")) as GameObject;
            break;
        }

        if (obj == null)
        {
            return;
        }

        // BaseObject baseObj = obj.GetComponent<BaseObject>();

        int fieldIndexI = (int)(Math.Round(pos.y) / TileInterval) + 1;
        int fieldIndexJ = (int)(Math.Round(pos.x) / TileInterval) + 1;

        pos.x = (int)((pos.x + TileInterval / 2) / TileInterval) * TileInterval;
        pos.y = (int)((pos.y + TileInterval / 2) / TileInterval) * TileInterval;
        
        // if (fieldMap[fieldIndexI, fieldIndexJ] != ' ')
        // {
        //     fieldMap[fieldIndexI, fieldIndexJ] = ' ';
        // }

        Debug.Log("fieldIndexI : " + fieldIndexI);
        Debug.Log("fieldIndexJ : " + fieldIndexI);

        Debug.Log("pos.y : " + pos.y);
        Debug.Log("pos.x : " + pos.x);

        fieldMap[fieldIndexI, fieldIndexJ] = type;
        
        obj.transform.position = pos;
        obj.transform.SetParent(minifield[indexI, indexJ].transform);
    }

    [MenuItem("MapEditor/CustomEditorTest")]
    static void Init() {
        EditorWindow.GetWindow(typeof(CustomEditorTest));
    }
}