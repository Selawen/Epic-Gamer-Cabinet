using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int bpm = 93;

    [Header("UI")]
    public TextMeshProUGUI noteResultText;
    public TextMeshProUGUI scoreText;

    public Color perfectColor, awesomeColor, greatColor, goodColor, mehColor, oofColor, missColor;
    public CurvedProgressBar boostBar;

    public GameObject failPanel;
    public GameObject EndPanel;

    [Header("Boost")]
    [SerializeField] float boostValue = 0.7f;
    public float BoostValue
    {
        get { return boostValue; }
        set { boostValue = Mathf.Min(1.0f, value); boostBar.FillState = boostValue; if (BoostValue <= 0) Fail(); }
    }
    public float beatTime, beatDistance, distanceBetweenBeatLines;
    AudioSource song;
    public float maxDis;

    

    int score;
    public int Score
    {
        get { return score; }
        set { score = value; scoreText.text = "Score: " + score.ToString(); }
    }

    [Header("Notes")]
    public GameObject redPrefab;
    public GameObject yellowPrefab, greenPrefab, bluePrefab;
    GameObject[] prefabArr;
    Transform[] spawnArr;
    Transform[] playPoints;

    List<Note> activeNotes;

    [Header("Spawnpoints")]
    public Transform redSpawn;
    public Transform yellowSpawn, greenSpawn, blueSpawn;


    [Header("Destroy Points")]
    public Transform redHitPoint;
    public Transform yellowHitPoint, greenHitPoint, blueHitPoint;

    Coroutine playRoutine;
    private bool redPressed, greenPressed, yellowPressed, bluePressed;

    // Start is called before the first frame update
    void Start()
    {
        song = Camera.main.GetComponentInChildren<AudioSource>();
        beatTime = 60.0f / bpm;
        beatDistance = distanceBetweenBeatLines / beatTime;
        maxDis = 2 * beatDistance;
        prefabArr = new GameObject[] { redPrefab, yellowPrefab, greenPrefab, bluePrefab };
        spawnArr = new Transform[] { redSpawn, yellowSpawn, greenSpawn, blueSpawn };
        playPoints = new Transform[] { redHitPoint, yellowHitPoint, greenHitPoint, blueHitPoint };

        activeNotes = new List<Note>();

        StartGameplay();

        Score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        while (activeNotes.Count < 1)
        {
            return;
        }

        if (Vector3.Distance(activeNotes[0].transform.position, new Vector3(0, 0, 0)) < 1)
        {
            MissNote(activeNotes[0]);
            activeNotes.RemoveAt(0);
        }

        switch (activeNotes[0].noteColour)
        {
            case (Note.Type.RED):
                if (Joystick.current[ArcadeInputs.Red()].IsPressed() && !redPressed)
                {
                    redPressed = true;
                    PlayNote(activeNotes[0]);
                } else if (!Joystick.current[ArcadeInputs.Red()].IsPressed())
                {
                    redPressed = false;
                }
                break;
            case (Note.Type.YELLOW):
                if (Joystick.current[ArcadeInputs.Yellow()].IsPressed() && !yellowPressed)
                {
                    yellowPressed = true;
                    PlayNote(activeNotes[0]);
                } else if (!Joystick.current[ArcadeInputs.Yellow()].IsPressed())
                {
                    yellowPressed = false;
                }
                break;
            case (Note.Type.GREEN):
                if (Joystick.current[ArcadeInputs.Green()].IsPressed() && !greenPressed)
                {
                    greenPressed = true;
                    PlayNote(activeNotes[0]);
                }
                else if (!Joystick.current[ArcadeInputs.Green()].IsPressed())
                {
                    greenPressed = false;
                }
                break;
            case (Note.Type.BLUE):
                if (Joystick.current[ArcadeInputs.Blue()].IsPressed() && !bluePressed)
                {
                    bluePressed = true;
                    PlayNote(activeNotes[0]);
                }
                else if (!Joystick.current[ArcadeInputs.Blue()].IsPressed())
                {
                    bluePressed = false;
                }
                break;
        }
    }

    void StartGameplay()
    {
        song.PlayDelayed(0.0f);
        StartCoroutine(SpawnWait());
        //StartCoroutine(SpawnGhosts());
    }
    IEnumerator SpawnWait()
    {
        yield return new WaitForSecondsRealtime(beatTime * 4);
        playRoutine = StartCoroutine(SpawnRandom());
    }
    IEnumerator SpawnRandom()
    {
        while (song.isPlaying)
        {
            int i = Random.Range(0, 5);
            if (i < 4)
            {
                GameObject spawned = Instantiate(prefabArr[i], spawnArr[i]);
                spawned.GetComponent<Note>().moveSpeed = beatDistance;
                activeNotes.Add(spawned.GetComponent<Note>()) ;
            }
            yield return new WaitForSecondsRealtime(beatTime);
        }
        EndScreen();
    }

    public void PlayNote(Note note)
    {
        float d = 0;

        switch (note.noteColour)
        {
            case (Note.Type.RED):
                d = Vector3.Distance(note.transform.position, redHitPoint.position);
                break;
            case (Note.Type.YELLOW):
                d = Vector3.Distance(note.transform.position, yellowHitPoint.position);
                break;
            case (Note.Type.GREEN):
                d = Vector3.Distance(note.transform.position, greenHitPoint.position);
                break;
            case (Note.Type.BLUE):
                d = Vector3.Distance(note.transform.position, blueHitPoint.position);
                break;
            default:
                return;
        }

        if (d <= 0.1f) { Score += 1000; BoostValue += 0.025f; noteResultText.text = "Perfect!"; noteResultText.color = perfectColor; }
        if (d > 0.1f && d <= 0.25f) { Score += 650; BoostValue += 0.02f; noteResultText.text = "Awesome!"; noteResultText.color = awesomeColor; }
        if (d > 0.25f && d <= 0.50f) { Score += 400; BoostValue += 0.015f; noteResultText.text = "Great"; noteResultText.color = greatColor; }
        if (d > 0.50f && d <= 0.75f) { Score += 100; BoostValue += 0.0f; noteResultText.text = "Good"; noteResultText.color = goodColor; }
        if (d > 0.75f && d <= 1.0f) { Score += 0; BoostValue -= 0.01f; noteResultText.text = "Meh"; noteResultText.color = mehColor; }
        if (d > 1.0f && d<= maxDis) { BoostValue -= 0.025f; noteResultText.text = "Oof!"; noteResultText.color = oofColor; }
        if (d > maxDis) { return; }

        activeNotes.RemoveAt(0);
        Destroy(note.gameObject);
    }

    public void MissNote(Note note)
    {
        BoostValue -= 0.125f;
        noteResultText.text = "Miss!"; noteResultText.color = missColor;
        Destroy(note.gameObject);
    }

    public void EndScreen()
    {
        EndPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Fail()
    {
        failPanel.SetActive(true);
        Time.timeScale = 0;
    }
}
