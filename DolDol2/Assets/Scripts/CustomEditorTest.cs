using UnityEditor;
using UnityEngine;
using System.Collections;

public class CustomEditorTest : EditorWindow {

    public Rect windowRect = new Rect(100, 100, 200, 200);
    private GameObject obj;
    private SpriteRenderer spriteRenderer;
    void OnGUI() {
        BeginWindows();
        obj = Resources.Load("Prefab/Wall") as GameObject;
        spriteRenderer = obj.GetComponent<SpriteRenderer>();
        // Debug.Log(spriteRenderer.sprite.texture);
        GUILayout.Button(spriteRenderer.sprite.texture, GUILayout.Width(70), GUILayout.Height(70));
        // Wall baseObj = obj.GetComponent<Wall>();
        // GUILayout.Button(baseObj.wallSprite[6].texture, GUILayout.Width(70), GUILayout.Height(70));
        GUILayout.Label("Test");
        EndWindows();
    }

    [MenuItem("MapEditor/CustomEditorTest")]
    static void Init() {
        EditorWindow.GetWindow(typeof(CustomEditorTest));
    }
}