using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameButton : MonoBehaviour
{
    public string scenepath;

    public void LoadScene()
    {
        SceneManager.LoadScene(scenepath);
    }
}
