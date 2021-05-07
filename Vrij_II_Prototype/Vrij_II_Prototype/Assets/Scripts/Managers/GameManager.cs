using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public UnityEvent deathEvents, winEvents, freeOnEvents, freeOffEvents;
    public int bpm;
    public float beatTime, beatDistance, distanceBetweenBeatLines;
    AudioSource song;
    public float maxDis;

    [Header("UI Stuff")]
    public HealthBar boostBar;
    public Text noteResultText;
    public Text[] scoreText;

    public Color perfectColor, awesomeColor, greatColor, goodColor, mehColor, oofColor, missColor;
    public bool spawnNotes;
    
    float boostValue;
    public float BoostValue
    {
        get { return boostValue; }
        set 
        { 
            boostValue = Mathf.Min(1.0f, value); boostBar.HPValue = boostValue; 
            if (boostValue <= 0.0f) { StopCoroutine(playRoutine); Die(); } 
            if (boostValue >= 1.0f) { FreeRoamMode(); } 
        }
        //Ah yes, readable code
    }

    int score;
    public int Score
    {
        get { return score; }
        set { score = value; foreach (Text t in scoreText) { t.text = score.ToString(); } }
    }

    [Header("Note prefabs")]
    public GameObject redPrefab;
    public GameObject yellowPrefab, greenPrefab, bluePrefab;
    GameObject[] prefabArr;
    Transform[] spawnArr;
    Transform[] playPoints;
    [Header("Spawnpoints")]
    public Transform redSpawn; 
    public Transform yellowSpawn, greenSpawn, blueSpawn;

    int ghostMultiplier;
    
    //Hallo ik ben Mana, ik heb een informatica diploma, en alsnog noem ik m'n variabelen zo *shame*
    [Header("Destroy Points")]
    public Transform redThingyPoint; 
    public Transform yellowThingyPoint, greenThingyPoint, blueThingyPoint;

    Coroutine playRoutine;

    [Header("Ghost Spawning")]
    public GameObject ghost;
    public float spawnTimeMin, spawnTimeMax;


    [Header("gamepad buttons")]
    public bool yPressed = false;
    public bool bPressed = false;
    public bool aPressed = false;
    public bool xPressed = false;



    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        foreach (var joystick in Input.GetJoystickNames())
        //Debug.Log(joystick);


        song = GetComponentInChildren<AudioSource>();
        beatTime = 60.0f / bpm;
        beatDistance = distanceBetweenBeatLines / beatTime;
        prefabArr = new GameObject[] { redPrefab, yellowPrefab, greenPrefab, bluePrefab };
        spawnArr = new Transform[] { redSpawn, yellowSpawn, greenSpawn, blueSpawn };
        playPoints = new Transform[] { redThingyPoint, yellowThingyPoint, greenThingyPoint, blueThingyPoint };
        spawnNotes = true;

        StartGameplay();

        BoostValue = 0.95f;
        Score = 0;

    }

    private void Update()
    {
       
    }

    void StartGameplay()
    {
        song.PlayDelayed(0.0f);
        playRoutine = StartCoroutine(SpawnRandom());
        StartCoroutine(SpawnGhosts());
    }

    IEnumerator SpawnRandom()
    {
        while(song.isPlaying)
        {
            while (!spawnNotes) { yield return null; }
            int i = Random.Range(0, 5);
            if (i < 4)
            { 
                GameObject spawned = Instantiate(prefabArr[i], spawnArr[i]);
                spawned.GetComponent<Note>().Init(playPoints[i]);
                spawned.GetComponent<Note>().enabled = true;
            }
            yield return new WaitForSecondsRealtime(beatTime);
        }
        Win();
    }

    public void PlayNote(Note note)
    {
        float v = Vector3.Distance(note.transform.position, note.destroyPos.position);

        if(v <= 0.1f) { Score += 1000; BoostValue += 0.025f; noteResultText.text = "Perfect!"; noteResultText.color = perfectColor; }
        if (v > 0.1f && v <= 0.25f) { Score += 650; BoostValue += 0.02f; noteResultText.text = "Awesome!"; noteResultText.color = awesomeColor; }
        if (v > 0.25f && v <= 0.50f) { Score += 400; BoostValue += 0.015f; noteResultText.text = "Great"; noteResultText.color = greatColor; }
        if (v > 0.50f && v <= 0.75f) { Score += 100; BoostValue += 0.0f; noteResultText.text = "Good"; noteResultText.color = goodColor; }
        if (v > 0.75f && v<= 1.0f) { Score += 0; BoostValue -= 0.01f; noteResultText.text = "Meh"; noteResultText.color = mehColor; }
        if (v > 1.0f) { BoostValue -= 0.025f; noteResultText.text = "Oof!"; noteResultText.color = oofColor; }

        Destroy(note.gameObject);
    }
    
    public void MissNote(Note note)
    {
        BoostValue -= 0.025f; noteResultText.text = "Miss!"; noteResultText.color = missColor;
        Destroy(note.gameObject);
    }

    void FreeRoamMode()
    {
        
        StartCoroutine(FreeRoamModeC());

    }

    IEnumerator FreeRoamModeC()
    {
        freeOnEvents.Invoke();
        spawnNotes = false;
        foreach(Note p in FindObjectsOfType<Note>())
        {
            Destroy(p.gameObject);
        }

        PlayerMovement.Instance.SetMove(true);
        
        while(BoostValue > 0.5f)
        {
            BoostValue -= Time.deltaTime * 0.05f; //Free roam for 10 seconds
            yield return null;
        }

        PlayerMovement.Instance.SetMove(false);
        spawnNotes = true;
        foreach(Ghost g in FindObjectsOfType<Ghost>())
        {
            Destroy(g.gameObject);
        }
        freeOffEvents.Invoke();
    }

    public void ScoreGhost()
    {
        ghostMultiplier++;
        Score += ghostMultiplier * 500;
        noteResultText.text = (ghostMultiplier * 500).ToString();
    }    

    IEnumerator SpawnGhosts()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(spawnTimeMin, spawnTimeMax));

            Vector3 randomDir = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f)).normalized;
            randomDir = Vector3.Scale(new Vector3(10, 0, 10), randomDir);
            Instantiate(ghost, randomDir, Quaternion.identity);
        }
    }

    #region ManaBasic Functionality
    public void Die()
    {
        deathEvents.Invoke();
    }

    public void Win()
    {
        winEvents.Invoke();
    }
    #endregion

    #region debug

    #endregion
}
