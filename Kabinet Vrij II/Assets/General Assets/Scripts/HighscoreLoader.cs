using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HighscoreLoader : MonoBehaviour
{
    //public string gameName;

    // Start is called before the first frame update
    void Start()
    {
        HighscoreObject highscores = Highscores.GetHighscores(SceneManager.GetActiveScene().name);
        this.GetComponent<TextMeshProUGUI>().text = "#1: " + highscores.first + "\n#2: " + highscores.second + "\n#3: " + highscores.third;
    }

}
