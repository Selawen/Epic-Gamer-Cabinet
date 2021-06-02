using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.SceneTemplate;
using UnityEngine.SceneManagement;
using System.IO;

public class MenuItems : EditorWindow
{
    string gameName;

    [MenuItem("Tools/Add New Game")]
    private static void AddGame()
    {
        GetWindow<MenuItems>("Add new game");
    }

    private void OnGUI()
    {
        EditorGUILayout.TextField("Name of your Game: ", gameName);

        if (GUILayout.Button("Add Game"))
        {
            
            string path = "Assets/Games/" + gameName;
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            //string guid = AssetDatabase.CreateFolder(path, gameName);
            //string newFolderPath = AssetDatabase.GUIDToAssetPath(guid);
            
            Scene newMenuScene;
        

            SceneTemplateAsset sceneTemplate = (SceneTemplateAsset)AssetDatabase.LoadAssetAtPath("Assets/General Assets/Scenes/Templates/MenuTemplate.scenetemplate", typeof(SceneTemplateAsset));
            if (sceneTemplate == null) Debug.Log("template not found");
            else newMenuScene = SceneTemplateService.Instantiate(sceneTemplate, true).scene;

            var newGameScene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Additive);
            newGameScene.name = gameName;
            AssetDatabase.MoveAsset(newGameScene.path, path+".unity");

            AssetDatabase.SaveAssets();
        }
    }

}
