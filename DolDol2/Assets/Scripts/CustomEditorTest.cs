using UnityEditor;
using UnityEngine;
using System.Collections;

public class CustomEditorTest : EditorWindow {

    public Rect windowRect = new Rect(100, 100, 200, 200);
    public Texture texture; //Image_Ground 1_0
    void OnGUI() {
        BeginWindows();
        texture = (Texture)Resources.Load("Assets/Images/Floor/Image_Ground 1_0.asset");
        GUILayout.Button(texture);
        EndWindows();
    }

    [MenuItem("MapEditor/CustomEditorTest")]
    static void Init() {
        EditorWindow.GetWindow(typeof(CustomEditorTest));
    }
}