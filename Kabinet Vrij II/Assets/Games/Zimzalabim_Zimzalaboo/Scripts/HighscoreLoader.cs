using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighscoreLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        HighscoreObject highscores = Highscores.GetHighscores();
        this.GetComponent<TextMeshProUGUI>().text = "#1: " + highscores.first + "/n#2: " + highscores.second + "/n#3: " + highscores.third;
    }

}
