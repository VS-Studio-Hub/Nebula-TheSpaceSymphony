using System;
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
    private int note = 0;
    public int purpleNoteValue;

    [Header("UI")]
    public TMP_Text scoreText;
    public TMP_Text multiText;

    public GameObject score;



    public  bool activatePurpleNote = false;
    private bool purpleTimerRunning = false;


    public int emptyPressLimit = 5;
    public int emptyPressCount = 0;


    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        emptyPressCount = 0;
        score.SetActive(false);
    }


    private void Update()
    {
        if (activatePurpleNote && !purpleTimerRunning)
        {
            StartCoroutine(DeactivateNote());
        }

        if (musicSource.isPlaying == false && musicSource.time > 0 && !PauseMenu.gameIsPaused)
        {
            score.SetActive(true);
            ScoreManager.gameOver = true;
        }
    }

    IEnumerator DeactivateNote()
    {
        purpleTimerRunning = true;

        yield return new WaitForSecondsRealtime(5);

        activatePurpleNote = false;
        purpleTimerRunning = false;
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


    public void EmptyPressCount()
    {
        Debug.Log("Empty Pressed");
        if (Time.timeScale == 0) return;

        emptyPressCount++;

        emptyPressCount = Mathf.Min(emptyPressCount, emptyPressLimit);
        if (emptyPressCount >= emptyPressLimit)
        {
            ScoreManager.gameOver = true;
            score.SetActive(true);
            ScoreManager.winState = false;
            emptyPressCount = 0;
        }
    }
    public void ResetEmptyPressCount()
    {
        emptyPressCount = 0;
    }

    public void NoteMissed()
    {
        currentMultiplier = 1;
        multiplierTracker = 0;
        UpdateUI();
    }

    public void PurpleNoteMiss()
    {
        note = 0;
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
       
        purpleNoteValue += value;

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

    public void ScoreBoard()
    {

        score.SetActive(true);
        ScoreManager.gameOver = true;

    }
}