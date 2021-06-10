using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class Highscores
{// Save the full path to the file.
    static string saveFile = "Assets/Games/Highscores/";
    static HighscoreObject highscoreObject;

    public static void SaveHighscores(int newScore, string gameName)
    {
        // Does it exist?
        if (File.Exists(saveFile))
        {
            GetHighscores(gameName);
            CompareScores(newScore);

            string jsonString = JsonUtility.ToJson(highscoreObject);
            File.WriteAllText(saveFile + gameName, jsonString);
        }
        else
        {
            highscoreObject = new HighscoreObject();
            highscoreObject.first = newScore;
            string jsonString = JsonUtility.ToJson(highscoreObject);
            File.WriteAllText(saveFile + gameName, jsonString);
        }
    }

    public static HighscoreObject GetHighscores(string gameName)
    {
        // Does it exist?
        if (File.Exists(saveFile + gameName))
        {
            string fileContents = File.ReadAllText(saveFile+gameName);
            highscoreObject = JsonUtility.FromJson<HighscoreObject>(fileContents);
            // File exists!
        }
        else
        {
            Debug.Log("no savefile yet");
            return new HighscoreObject();
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
