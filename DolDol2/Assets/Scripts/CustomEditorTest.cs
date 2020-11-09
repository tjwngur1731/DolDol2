using UnityEditor;
using UnityEngine;
using System.Collections;
using System;

public class CustomEditorTest : EditorWindow {
    public Rect windowRect = new Rect(100, 100, 200, 200);
    private Vector3 mousepos;
    private int[,] fieldMap = null;
    private bool isActive = false;
    void OnGUI() {
        if (fieldMap == null)
        {
            fieldMap = new int[25,25];
        }

        BeginWindows();
        
        GUILayout.Button("", GUILayout.Width(50), GUILayout.Height(40));
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

    void OnSceneGUI(SceneView sceneView) {
        // if (isActive != true)
        // {
        //     return;
        // }

        Event current = Event.current;

        if (fieldMap == null)
        {
            fieldMap = new int[25,25];
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
        GameObject wall = Instantiate(Resources.Load("Prefab/Wall")) as GameObject;

        Debug.Log(pos);
        pos.x = (float)Math.Round(pos.x);
        pos.y = (float)Math.Round(pos.y);

        Debug.Log(pos);

        wall.transform.position = pos;
    }

    [MenuItem("MapEditor/CustomEditorTest")]
    static void Init() {
        EditorWindow.GetWindow(typeof(CustomEditorTest));
    }
}