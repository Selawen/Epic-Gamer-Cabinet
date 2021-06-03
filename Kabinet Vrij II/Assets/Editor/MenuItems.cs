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
        gameName = EditorGUILayout.TextField("Name of your Game: ", gameName);

        if (GUILayout.Button("Add Game"))
        {
            //create folder to put new game files in
            string path = "Assets/Games/" + gameName;
            string guid = AssetDatabase.CreateFolder("Assets/Games", gameName);
            string newFolderPath = AssetDatabase.GUIDToAssetPath(guid);

            Scene newMenuScene;

            //create menu for game from template
            SceneTemplateAsset sceneTemplate = (SceneTemplateAsset)AssetDatabase.LoadAssetAtPath("Assets/General Assets/Scenes/Templates/MenuTemplate.scenetemplate", typeof(SceneTemplateAsset));
            if (sceneTemplate == null) {Debug.Log("template not found"); return;}
        else newMenuScene = SceneTemplateService.Instantiate(sceneTemplate, true, "Assets/Games/MainMenus/" + gameName + "Menu.unity").scene;

            GameObject.Find("PlayButton");

            //create scene for actual game
            var newGameScene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Additive);
            newGameScene.name = gameName;
            EditorSceneManager.SaveScene(newGameScene, path+"/"+gameName+".unity");
            Debug.Log(newGameScene.path);
            //add new scenes to build settings

            string[] ScenesList = new string[] {  newMenuScene.path, newGameScene.path};

            EditorBuildSettingsScene[] original = EditorBuildSettings.scenes;
            EditorBuildSettingsScene[] newSettings = new EditorBuildSettingsScene[original.Length + ScenesList.Length];
            System.Array.Copy(original, newSettings, original.Length);

            int index = original.Length;

            for (int i = 0; i < ScenesList.Length; i++)
            {
                EditorBuildSettingsScene sceneToAdd = new EditorBuildSettingsScene(ScenesList[i], true);
                newSettings[index] = sceneToAdd;

                index++;
            }

            EditorBuildSettings.scenes = newSettings;

            AssetDatabase.SaveAssets();
            Debug.Log("Game successfully added");
            Close();
        }
    }

}
