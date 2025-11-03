using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public AudioSource theMusic;

    public bool startPlaying;

    public NodeMovement theBS;

    public static GameManager instance;

    public int currentScore;
    public int scorePerNote = 100;
    public int scorePerGoodNote = 125;
    public int scorePerPerfectNote = 150;

    public int currentMultiplayer;
    public int multiplayerTracker;
    public int[] multiplierThresholds;

    public Text scoreText;
    public Text multiText;

    public float totalNotes;
    public float normalHits;
    public float goodHits;
    public float perfectHits;
    public float missedHits;

    public GameObject resultsScreen;
    public Text percentHitText, normalsText, goodsText, perfectsText, missesText, rankText, finalScoreText;
    public float MusicTimeLeft;
    public NodeSpawnManager nodeSpawnManager;
    public AudioClip music;

    [SerializeField] public int PurpleNoteHitCounter = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        music = theMusic.clip;
        MusicTimeLeft = music.length;
    }
    void Start()
    {
        
        nodeSpawnManager = GameObject.Find("NodeSpawnManager").GetComponent<NodeSpawnManager>();
        instance = this;
        scoreText.text = "Score: 0";
        currentMultiplayer = 1;

        totalNotes = Object.FindObjectsByType<NoteObject>(FindObjectsSortMode.None).Length;

    }

    // Update is called once per frame
    void Update()
    {
        if (!startPlaying)
        {
                startPlaying = true;
                theBS.hasStarted = true;

                theMusic.Play();
                
                
        }
        else
        {
            MusicTimeLeft -= Time.deltaTime;
            if (MusicTimeLeft <= 7.5f)
            {
                nodeSpawnManager.StopSpawning();
            }
            if (!theMusic.isPlaying && !resultsScreen.activeInHierarchy)
            {
                resultsScreen.SetActive(true);

                normalsText.text = "" + normalHits;
                goodsText.text = goodHits.ToString();
                perfectsText.text = perfectHits.ToString();
                missesText.text = "" + missedHits;

                float totalHit = normalHits + goodHits + perfectHits;
                float percentHit = (totalHit / (totalHit + missedHits)) * 100;
                if (totalHit + missedHits == 0)
                {
                    percentHit = 0;
                }

                percentHitText.text = percentHit.ToString("F1") + "%";

                string rankVal = "F";

                if (percentHit > 40)
                {
                    rankVal = "D";
                    if (percentHit > 55)
                    {
                        rankVal = "C";
                        if (percentHit > 70)
                        {
                            rankVal = "B";
                            if (percentHit > 85)
                            {
                                rankVal = "A";
                                if (percentHit > 95)
                                {
                                    rankVal = "S";
                                }
                            }
                        }
                    }
                }

                rankText.text = rankVal;

                finalScoreText.text = currentScore.ToString();
            }
        }
        if (PurpleNoteHitCounter == 1)
        {
            nodeSpawnManager.DoublePointState = true;
            PurpleNoteHitCounter = 0;
            if (nodeSpawnManager.DoublePointState)
            {
                StartCoroutine(EndDoublePointState());
            }
        }
    }

    public void NoteHit()
    {
        Debug.Log("Hit On Time");
        if (currentMultiplayer - 1 < multiplierThresholds.Length)
        {
            multiplayerTracker++;

            if (multiplierThresholds[currentMultiplayer - 1] <= multiplayerTracker)
            {
                multiplayerTracker = 0;
                currentMultiplayer++;
            }
        }

        multiText.text = "Multiplier: x" + currentMultiplayer;

        //currentScore += scorePerNote * currentMultiplayer;
        scoreText.text = "Score: " + currentScore;
    }
    public void NormalHit()
    {
        currentScore += scorePerNote * currentMultiplayer;
        NoteHit();
        normalHits++;
        if (nodeSpawnManager.DoublePointState)
        {
            currentScore += scorePerNote * currentMultiplayer * 2;
        }
    }

    public void GoodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplayer;
        NoteHit();
        goodHits++;
        if (nodeSpawnManager.DoublePointState)
        {
            currentScore += scorePerNote * currentMultiplayer * 2;
        }
    }

    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplayer;
        NoteHit();
        perfectHits++;
        if (nodeSpawnManager.DoublePointState)
        {
            currentScore += scorePerNote * currentMultiplayer * 2;
        }
    }

    public void NoteMissed()
    {
        Debug.Log("Missed Note");

        currentMultiplayer = 1;
        multiplayerTracker = 0;

        multiText.text = "Multiplier: x" + currentMultiplayer;

        missedHits++;
    }
    IEnumerator EndDoublePointState()
    {
        yield return new WaitForSecondsRealtime(5);
        nodeSpawnManager.DoublePointState = false;
        
    }
}
