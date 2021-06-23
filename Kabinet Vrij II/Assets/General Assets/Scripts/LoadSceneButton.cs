using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[ExecuteAlways]
public class LoadSceneButton : MonoBehaviour
{
    public bool toHighscores = false;
    public bool toGameMenu = false;
    public bool toGame = false;

    public string scenepath;
    [SerializeField] private int sceneBuildIndex = -1;

    private void OnValidate()
    {
        if (toHighscores)
        {
            string path = SceneManager.GetActiveScene().path;
            path = path.Split('~')[0];
            path = path.Split('.')[0];
            string[] splitPath = path.Split('/');
            string gameName = splitPath[splitPath.Length - 1];

            scenepath = "Assets/Games/Highscores/" + gameName + "~Highscores.unity";
        } else if (toGameMenu)
        {
            string path = SceneManager.GetActiveScene().path;
            path = path.Split('~')[0];
            path = path.Split('.')[0];
            string[] splitPath = path.Split('/');
            string gameName = splitPath[splitPath.Length - 1];

            scenepath = "Assets/Games/MainMenus/" + gameName + "~Menu.unity";
        } else if (toGame)
        {
            string path = SceneManager.GetActiveScene().path;
            path = path.Split('~')[0];
            path = path.Split('.')[0];
            string[] splitPath = path.Split('/');
            string gameName = splitPath[splitPath.Length - 1];

            scenepath = "Assets/Games/" + gameName + "/" + gameName +".unity";
        }
    }

    public void LoadScene()
    {
        //if a build index has been defined use that to load the scene, else use path if available, else throw error
        if (sceneBuildIndex != -1)
        {
            SceneManager.LoadScene(sceneBuildIndex);
        }
        else if (scenepath != null)
        {
            SceneManager.LoadScene(scenepath);
        }
        else
        {
            Debug.LogError("No scene defined");
        }
    }
}
