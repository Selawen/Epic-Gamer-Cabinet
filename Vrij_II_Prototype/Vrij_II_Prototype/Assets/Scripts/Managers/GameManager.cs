using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public UnityEvent deathEvents, winEvents;
    public int bpm;
    public float beatTime, beatDistance, distanceBetweenBeatLines;
    AudioSource song;
    public float maxDis;

    [Header("Note prefabs")]
    public GameObject redPrefab;
    public GameObject yellowPrefab, greenPrefab, bluePrefab;
    GameObject[] prefabArr;
    Transform[] spawnArr;
    Transform[] playPoints;
    [Header("Spawnpoints")]
    public Transform redSpawn; 
    public Transform yellowSpawn, greenSpawn, blueSpawn;
    
    //Hallo ik ben Mana, ik heb een informatica diploma, en alsnog noem ik m'n variabelen zo *shame*
    [Header("Destroy Points")]
    public Transform redThingyPoint; 
    public Transform yellowThingyPoint, greenThingyPoint, blueThingyPoint;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        song = GetComponentInChildren<AudioSource>();
        beatTime = 60.0f / bpm;
        beatDistance = distanceBetweenBeatLines / beatTime;
        prefabArr = new GameObject[] { redPrefab, yellowPrefab, greenPrefab, bluePrefab };
        spawnArr = new Transform[] { redSpawn, yellowSpawn, greenSpawn, blueSpawn };
        playPoints = new Transform[] { redThingyPoint, yellowThingyPoint, greenThingyPoint, blueThingyPoint };
        StartGameplay();
    }

    void StartGameplay()
    {
        song.PlayDelayed(0.45f);
        StartCoroutine(SpawnRandom());
    }

    IEnumerator SpawnRandom()
    {
        while(song.isPlaying)
        {
            int i = Random.Range(0, 5);
            if (i < 4)
            { 
                GameObject spawned = Instantiate(prefabArr[i], spawnArr[i]);
                spawned.GetComponent<ProjectileMovement>().Init(playPoints[i]);
                spawned.GetComponent<ProjectileMovement>().enabled = true;
            }
            yield return new WaitForSecondsRealtime(beatTime);
        }
    }

    public void PlayNote(ProjectileMovement note)
    {
        Destroy(note.gameObject);
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
