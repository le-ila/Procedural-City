using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawnerRandomCreator : EditorWindow
{
    [MenuItem("Penis/Random Spawner Tool")]
    public static void ShowWindow()
    {
        GetWindow<SpawnerRandomCreator>("Generator Creator");
    }

    void OnGUI()
    {
        GUILayout.Label("Create a new Random Spawner", EditorStyles.boldLabel);

        if (GUILayout.Button("Add Procedural Building"))
        {
            CreateRandomSpawnerObject();
        }
    }

    private static void CreateRandomSpawnerObject()
    {
        GameObject randomPositionObject = new GameObject("Random Spawner");
        randomPositionObject.AddComponent<RandomSpawner>();
        Selection.activeGameObject = randomPositionObject;
        SceneView.lastActiveSceneView.FrameSelected();
    }
}
