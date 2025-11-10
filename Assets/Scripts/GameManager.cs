using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Audio")]
    public AudioSource musicSource;
    public AudioClip musicClip;

    [Header("Score Settings")]
    public int currentScore;
    public int currentMultiplier = 1;
    public int[] multiplierThresholds;
    private int multiplierTracker = 0;
    private int note;

    [Header("UI")]
    public Text scoreText;
    public Text multiText;

    bool missedNote = false;
    public static bool activatePurpleNote = false;

    void Awake()
    {
        instance = this;
    }

    // Called from NodeSpawnManager
    public void StartMusic(double startTime)
    {
        musicSource.clip = musicClip;
        musicSource.PlayScheduled(startTime);
    }

    public void SmallNoteHit()
    {
        AddScore(100);
    }

    public void SmallNoteGood()
    {
        AddScore(125);
    }

    public void SmallNotePerfect()
    {
        AddScore(150);
    }

    public void LargeNoteHitValue()
    {
        AddScore(1);
    }

    public void PurpleNoteValue()
    {
        PurpleValue(1);
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

    void AddScore(int baseScore)
    {
        currentScore += baseScore * currentMultiplier;
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
        if (missedNote)
        {
            note = 0;
            missedNote = false;
        }
        note += value;
        if (note < 5)
        {
            activatePurpleNote = true;
            note = 0;
        }
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + currentScore;
        multiText.text = "x" + currentMultiplier;
    }




}
