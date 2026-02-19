using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static bool gameOver = false;

    public TMP_Text scoreText, nebulaScoreText, totalScoreText, highScoreText, gradeText, winLoseText;

    float displayedScore = 0;
    public float scoreUpdateSpeed = 2f;

    public static bool winState = true;


    bool hasUpdatedScore = true;
    void Start()
    {
        gameOver = false;
        winState = true;        
        hasUpdatedScore = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver && hasUpdatedScore)
        {
            hasUpdatedScore = false;

            if (winState)
            {
                winLoseText.text = "You Win!";

            }
            else
            {
                winLoseText.text = "You Lose!";
                gradeText.text = "F";
            }

            StartCoroutine(Score());

        }
    }

    IEnumerator Score()
    {
        yield return new WaitForSeconds(2f);
        displayedScore = 0;
        while (displayedScore < GameManager.instance.currentScore)
        {
            displayedScore = Mathf.Lerp(displayedScore, GameManager.instance.currentScore, Time.deltaTime * scoreUpdateSpeed);


            scoreText.text = Mathf.RoundToInt(displayedScore).ToString();
            if (Mathf.Abs(displayedScore - GameManager.instance.currentScore) < 0.1f)
            {
                displayedScore = GameManager.instance.currentScore;
                break;
            }
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        displayedScore = 0;
        totalScoreText.text = GameManager.instance.currentScore.ToString();

        yield return new WaitForSeconds(2f);

        while (displayedScore < GameManager.instance.purpleNoteValue)
        {
            displayedScore = Mathf.Lerp(displayedScore, GameManager.instance.purpleNoteValue, Time.deltaTime * scoreUpdateSpeed);

            scoreText.text = Mathf.RoundToInt(displayedScore).ToString();
            if (Mathf.Abs(displayedScore - GameManager.instance.purpleNoteValue) < 0.1f)
            {
                displayedScore = GameManager.instance.purpleNoteValue;
                break;
            }
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        int nebulaScore = GameManager.instance.purpleNoteValue * 50;

        while (displayedScore < nebulaScore)
        {
            displayedScore = Mathf.Lerp(displayedScore, nebulaScore, Time.deltaTime * scoreUpdateSpeed);

            scoreText.text = Mathf.RoundToInt(displayedScore).ToString();
            if (Mathf.Abs(displayedScore - nebulaScore) < 0.1f)
            {
                displayedScore = nebulaScore;
                break;
            }
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        int finalHighScore = GameManager.instance.currentScore + nebulaScore;

        totalScoreText.text = finalHighScore.ToString();

        int savedHighScore = PlayerPrefs.GetInt("HighScore", 0);

        if (finalHighScore > savedHighScore)
        {
            savedHighScore = finalHighScore;
            PlayerPrefs.SetInt("HighScore", savedHighScore);
            PlayerPrefs.Save();
        }
        highScoreText.text = savedHighScore.ToString();

        yield return new WaitForSeconds(2f);


        if (winState)
        {
            if (finalHighScore >= 10000)
            {
                gradeText.text = "S+";
            }
            else if (finalHighScore >= 7500)
            {
                gradeText.text = "S";
            }
            else if (finalHighScore >= 5000)
            {
                gradeText.text = "A";
            }
            else if (finalHighScore >= 2500)
            {
                gradeText.text = "B";
            }
            else if (finalHighScore >= 1000)
            {
                gradeText.text = "C";
            }
            else
            {
                gradeText.text = "D";
            }
        }
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}