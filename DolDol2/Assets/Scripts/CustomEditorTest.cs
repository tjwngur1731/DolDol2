using UnityEditor;
using UnityEngine;
using System.Collections;

public class CustomEditorTest : EditorWindow {

    public Rect windowRect = new Rect(100, 100, 200, 200);
    private Texture texture; //Image_Ground 1_0
    void OnGUI() {
        BeginWindows();
        texture = (Texture)Resources.Load("Assets/Images/Button_Resume.png");
        GUILayout.Button(texture, GUILayout.Width(70), GUILayout.Height(70));
        GUILayout.Label("Test");
        // GUILayout.Button("texture", GUILayout.Width(70), GUILayout.Height(70));
        EndWindows();
    }

    [MenuItem("MapEditor/CustomEditorTest")]
    static void Init() {
        EditorWindow.GetWindow(typeof(CustomEditorTest));
    }
}