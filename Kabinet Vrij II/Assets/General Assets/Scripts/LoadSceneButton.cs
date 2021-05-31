using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneButton : MonoBehaviour
{
    public string scenepath;
    [SerializeField] private int sceneBuildIndex = -1;

    public void LoadScene()
    {
        //if a build index had been defined use that to load the scene, else use path if available, else throw error
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
