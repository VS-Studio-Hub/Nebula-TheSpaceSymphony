using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Audio")]
    public AudioSource musicSource;
    public AudioClip musicClip;

    [Header("Score Settings")]
    public int currentScore, totalScore;
    public int currentMultiplier = 1;
    public int[] multiplierThresholds;
    private int multiplierTracker = 0;
    private int note;

    [Header("UI")]
    public TMP_Text scoreText;
    public TMP_Text multiText;



    bool missedNote = false;
    public  bool activatePurpleNote = false;
    private bool purpleTimerRunning = false;

    void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (activatePurpleNote && !purpleTimerRunning)
        {
            StartCoroutine(DeactivateNote());
        }

        if (musicSource.isPlaying == false && musicSource.time > 0)
        {
            StartCoroutine(WaitAndLoadMenu(4f));
        }
    }

    IEnumerator DeactivateNote()
    {
        purpleTimerRunning = true;

        yield return new WaitForSecondsRealtime(5);

        activatePurpleNote = false;
        purpleTimerRunning = false;
    }

    IEnumerator WaitAndLoadMenu(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("MainMenu");
    }

    public void StartMusic(double startTime)
    {
        musicSource.clip = musicClip;
        musicSource.PlayScheduled(startTime);
    }

    public void SmallNoteHit() => AddScore(100);
    public void SmallNoteGood() => AddScore(125);
    public void SmallNotePerfect() => AddScore(150);
    public void LongNoteHitValue() => AddScore(1);
    public void LongNoteHit() => AddScore(10);
    public void LongNoteGood() => AddScore(25);
    public void LongNotePerfect() => AddScore(50);
    public void PurpleNoteValue() => PurpleValue(1);
    public void PurpleActivatedNoteValue() => PurpleValue(0);

    public void NoteMissed()
    {
        currentMultiplier = 1;
        multiplierTracker = 0;
        UpdateUI();
    }

    public void PurpleNoteMiss()
    {
        missedNote = true;
    }

    private void AddScore(int baseScore)
    {
        int finalScore = baseScore * currentMultiplier;

        currentScore += finalScore;
        totalScore += finalScore;

        multiplierTracker++;

        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {
            if (multiplierTracker >= multiplierThresholds[currentMultiplier - 1])
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }

        UpdateUI();
    }

    void PurpleValue(int value)
    {
        note += value;
        if (missedNote)
        {
            note = 0;
            missedNote = false;
        }

        if (note == 5)
        {
            activatePurpleNote = true;
            note = 0;
            Debug.Log("Acttivated");
        }
    }
    void UpdateUI()
    {
        scoreText.text = "Score: " + totalScore;
        multiText.text = "x" + currentMultiplier;
    }
}