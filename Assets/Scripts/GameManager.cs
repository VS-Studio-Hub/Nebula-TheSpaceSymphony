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


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
            if (!theMusic.isPlaying && !resultsScreen.activeInHierarchy)
            {
                resultsScreen.SetActive(true);

                normalsText.text = "" + normalHits;
                goodsText.text = goodHits.ToString();
                perfectsText.text = perfectHits.ToString();
                missesText.text = "" + missedHits;

                float totalHit = normalHits + goodHits + perfectHits;
                float percentHit = (totalHit / totalNotes) * 100f;

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
    }

    public void GoodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplayer;
        NoteHit();
        goodHits++;
    }

    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplayer;
        NoteHit();
        perfectHits++;
    }

    public void NoteMissed()
    {
        Debug.Log("Missed Note");

        currentMultiplayer = 1;
        multiplayerTracker = 0;

        multiText.text = "Multiplier: x" + currentMultiplayer;

        missedHits++;
    }
}
