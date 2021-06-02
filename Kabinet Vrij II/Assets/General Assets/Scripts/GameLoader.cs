using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;

public class GameLoader : MonoBehaviour
{
    public List<string> gameMenuScenes = new List<string>();
    [SerializeField] private GameObject gameButtonPrefab;
    [SerializeField] private GameObject gameSelectPanel;

    string path = "Assets/Games/MainMenus"; //Main menus of new games are placed in this folder

    /*
    private void OnValidate()
    {
        GetGames();
        RemoveOldButtons();
        AddButtons();
    }
    */
    private void Awake()
    {
        //DontDestroyOnLoad(this);
        GetGames();
        RemoveOldButtons();
        AddButtons();
    }
    

    public void GetGames()
    {
        //if the list has not been created/filled yet, fill it
        if (gameMenuScenes.Count < 1)
        {

            //get all scenes from folder
            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] info = dir.GetFiles("*.unity");
            if (info.Length < 1)
            {
                Debug.Log("no scenes found");
            }
            //save path to scenes
            foreach (FileInfo f in info)
            {
                //string s = path + "/" + f.Name;
                string s = f.Name;
                gameMenuScenes.Add(s);
            }
        }
    }

    public void RemoveOldButtons()
    {
        foreach (LoadSceneButton button in gameSelectPanel.GetComponentsInChildren<LoadSceneButton>())
        {
            Destroy(button.gameObject);
        }
    }
    
    public void AddButtons()
    {
        foreach (string gamePath in gameMenuScenes)
        {
            GameObject button = Instantiate(gameButtonPrefab) as GameObject;
            button.transform.SetParent(gameSelectPanel.transform, false);

            string gameTitle = gamePath;
            gameTitle = gamePath.Split('.')[0];
            gameTitle = gameTitle.Replace('_', ' ');

            button.GetComponent<LoadSceneButton>().scenepath = path + "/" + gamePath;
            button.name = gameTitle;
            button.GetComponentInChildren<TextMeshProUGUI>().text = gameTitle;
        }
    }

}
