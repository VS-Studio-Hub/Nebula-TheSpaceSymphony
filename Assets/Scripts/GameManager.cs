using System.Collections;
using TMPro;
using UnityEngine;

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

    public int missvalue;

    [Header("UI")]
    public TMP_Text scoreText;
    public TMP_Text multiText;
    public GameObject score;

    public bool activatePurpleNote = false;
    private bool purpleTimerRunning = false;
    public static bool gameOver;

    public int emptyPressLimit = 5;
    public int emptyPressCount = 0;

    public static bool startGame;
    public CrackingScreenController CrackController;
    public int Counter = 0;
    public GameObject CustomPass;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        if (CrackController == null)
        {
            CustomPass = GameObject.Find("Custom Pass");
            CrackController = GameObject.Find("Custom Pass").GetComponent<CrackingScreenController>();
            CustomPass.SetActive(false);
        }
    }

    void Start()
    {
        startGame = false;
        gameOver = false;
        emptyPressCount = 0;

        if (score != null)
            score.SetActive(false);

        currentScore = 0;
        totalScore = 0;
        currentMultiplier = 1;
        multiplierTracker = 0;
        note = 0;
        purpleNoteValue = 0;
        activatePurpleNote = false;
        purpleTimerRunning = false;

        UpdateUI();
    }

    void Update()
    {
        if (!startGame)
            return;
        if (gameOver)
        {
            musicSource.Stop();
            return;
        }
        if (activatePurpleNote && !purpleTimerRunning)
        {
            StartCoroutine(DeactivateNote());
        }

        if (musicSource != null && !musicSource.isPlaying && musicSource.time > 0 && !PauseMenu.gameIsPaused)
        {
            if (score != null)
                score.SetActive(true);

            ScoreManager.gameOver = true;
        }
    }

    IEnumerator DeactivateNote()
    {
        purpleTimerRunning = true;

        yield return new WaitForSecondsRealtime(5f);

        activatePurpleNote = false;
        purpleTimerRunning = false;
    }

    public void StartMusic()
    {
        if (musicSource == null || musicClip == null)
            return;

        musicSource.clip = musicClip;
        musicSource.time = 0f;
        musicSource.Play();
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

    public void MissNotesValue() => MissNotesValue(1);

    private void MissNotesValue(int value)
    {   
        missvalue += value;
        if (missvalue >= 10)
        {
            ScoreManager.gameOver = true;
            ScoreManager.winState = false;
            gameOver = true;
            if (score != null)
                score.SetActive(true);
        }
        else
        {
            CameraShaking.start = true;
            if (Counter == 0)
            {
                CustomPass.SetActive(true);
                Counter++;
            }
            else
            {
                CrackController.EffectVisibility();
                Counter++;
            }
        }
            

    }
    public void EmptyPressCount()
    {
        Debug.Log("Empty Pressed");

        emptyPressCount++;
        emptyPressCount = Mathf.Min(emptyPressCount, emptyPressLimit);

        if (emptyPressCount >= emptyPressLimit)
        {
            ScoreManager.gameOver = true;
            ScoreManager.winState = false;
            gameOver = true;
            if (score != null)
                score.SetActive(true);

            emptyPressCount = 0;
        }
    }

    public void ResetEmptyPressCount()
    {
        emptyPressCount = 0;
        if (Counter > 0)
        {
            Counter--;
            CrackController.EffectInVisibility();
        }
        else
        {   

            CustomPass.SetActive(false);
        }
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
        missvalue = 0;
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
            Debug.Log("Activated");
        }
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + totalScore;

        if (multiText != null)
            multiText.text = "x" + currentMultiplier;
    }
}