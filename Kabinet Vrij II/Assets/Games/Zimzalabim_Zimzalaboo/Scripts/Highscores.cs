using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class Highscores
{// Save the full path to the file.
    static string saveFile = "Assets/Games/Zimzalabim_Zimzalaboo/gamedata.data";
    static HighscoreObject highscoreObject;

    public static void SaveHighscores(int newScore)
    {
        // Does it exist?
        if (File.Exists(saveFile))
        {
            GetHighscores();
            CompareScores(newScore);

            string jsonString = JsonUtility.ToJson(highscoreObject);
            File.WriteAllText(saveFile, jsonString);
        }
        else
        {
            highscoreObject = new HighscoreObject();
            highscoreObject.first = newScore;
            string jsonString = JsonUtility.ToJson(highscoreObject);
            File.WriteAllText(saveFile, jsonString);
        }
    }

    public static HighscoreObject GetHighscores()
    {
        // Does it exist?
        if (File.Exists(saveFile))
        {
            string fileContents = File.ReadAllText(saveFile);
            highscoreObject = JsonUtility.FromJson<HighscoreObject>(fileContents);
            // File exists!
        }
        else
        {
            Debug.Log("no savefile yet");
            return null;
        }
        return highscoreObject;
    }

    private static void CompareScores(int newScore)
    {
        if (newScore > highscoreObject.first)
        {
            highscoreObject.third = highscoreObject.second;
            highscoreObject.second = highscoreObject.first;
            highscoreObject.first = newScore;
            return;
        } else if (newScore > highscoreObject.second)
        {
            highscoreObject.third = highscoreObject.second;
            highscoreObject.second = newScore;
            return;
        } else if (newScore > highscoreObject.third)
        {
            highscoreObject.third = newScore;
            return;
        }
    }
}
