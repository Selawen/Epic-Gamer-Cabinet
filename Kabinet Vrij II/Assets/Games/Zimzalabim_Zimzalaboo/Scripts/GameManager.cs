using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using FMODUnity;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int bpm = 140;

    [Header("UI")]
    public TextMeshProUGUI noteResultText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI failScore;
    public TextMeshProUGUI endScore;

    public Color perfectColor, awesomeColor, greatColor, goodColor, mehColor, oofColor, missColor;
    public CurvedProgressBar boostBar;

    public GameObject failPanel;
    public GameObject endPanel;

    [Header("Boost")]
    [SerializeField] float boostValue = 0.7f;
    public float BoostValue
    {
        get { return boostValue; }
        set { boostValue = Mathf.Min(1.0f, value); boostBar.FillState = boostValue; if (BoostValue <= 0) Fail(); }
    }
    public float beatTime, beatDistance, distanceBetweenBeatLines;
    // AudioSource song;
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


    FMOD.Studio.EventInstance playerState;
    FMOD.Studio.EventInstance maintheme;
    FMOD.Studio.PLAYBACK_STATE playbackState;

    // Start is called before the first frame update
    void Start()
    {
        maintheme = FMODUnity.RuntimeManager.CreateInstance("event:/level1_theme");
        maintheme.start();
        //song = Camera.main.GetComponentInChildren<AudioSource>();
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


    void OnDestroy()
    {
        //StopAllPlayerEvents();

        //--------------------------------------------------------------------
        // 6: This shows how to release resources when the unity object is 
        //    disabled.
        //--------------------------------------------------------------------
        playerState.release();
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
                if (ArcadeInputs.Red() && !redPressed)
                {
                    redPressed = true;
                    PlayNote(activeNotes[0]);
                } else if (!ArcadeInputs.Red())
                {
                    redPressed = false;
                }
                break;
            case (Note.Type.YELLOW):
                if (ArcadeInputs.Yellow() && !yellowPressed)
                {
                    yellowPressed = true;
                    PlayNote(activeNotes[0]);
                } else if (!ArcadeInputs.Yellow())
                {
                    yellowPressed = false;
                }
                break;
            case (Note.Type.GREEN):
                if (ArcadeInputs.Green() && !greenPressed)
                {
                    greenPressed = true;
                    PlayNote(activeNotes[0]);
                }
                else if (!ArcadeInputs.Green())
                {
                    greenPressed = false;
                }
                break;
            case (Note.Type.BLUE):
                if (ArcadeInputs.Blue() && !bluePressed)
                {
                    bluePressed = true;
                    PlayNote(activeNotes[0]);
                }
                else if (!ArcadeInputs.Blue())
                {
                    bluePressed = false;
                }
                break;
        }
    }

    void StartGameplay()
    {
        //song.PlayDelayed(0.0f);
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
        if (maintheme.isValid())
        {
            maintheme.getPlaybackState(out playbackState);
            while (playbackState == FMOD.Studio.PLAYBACK_STATE.PLAYING)
            {
                maintheme.getPlaybackState(out playbackState);

                //while (song.isPlaying)
                //{
                int i = Random.Range(0, 5);
                if (i < 4)
                {
                    GameObject spawned = Instantiate(prefabArr[i], spawnArr[i]);
                    spawned.GetComponent<Note>().moveSpeed = beatDistance;
                    activeNotes.Add(spawned.GetComponent<Note>());
                }
                yield return new WaitForSecondsRealtime(beatTime);
                //}
            }
        }
        EndScreen();
    }

    private void PlayNote(Note note)
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

    private void MissNote(Note note)
    {
        BoostValue -= 0.125f;
        noteResultText.text = "Miss!"; noteResultText.color = missColor;
        Destroy(note.gameObject);
    }

    private void EndScreen()
    {
        if (!failPanel.activeSelf)
        {
            Highscores.SaveHighscores(score);
            endScore.text = "score: " + score.ToString();
            endPanel.SetActive(true);
            Time.timeScale = 0;
            maintheme.release();
            maintheme.clearHandle();
        }
    }

    private void Fail()
    {
        Highscores.SaveHighscores(score);
        failScore.text = "score: " + score.ToString();
        failPanel.SetActive(true);
        Time.timeScale = 0;
        maintheme.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        maintheme.release();
        maintheme.clearHandle();
    }

    public void Retry()
    {
        score = 0;
        BoostValue = 0.7f;
        failPanel.SetActive(false);
        endPanel.SetActive(false);
        Time.timeScale = 1;
        maintheme = RuntimeManager.CreateInstance("event:/level1_theme");
        maintheme.start();
    }

    void StopAllPlayerEvents()
    {
        FMOD.Studio.Bus musicBus = FMODUnity.RuntimeManager.GetBus("bank:/Music");
        FMOD.Studio.Bus hitBus = FMODUnity.RuntimeManager.GetBus("bank:/Hit");
        musicBus.stopAllEvents(FMOD.Studio.STOP_MODE.IMMEDIATE);
        hitBus.stopAllEvents(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}