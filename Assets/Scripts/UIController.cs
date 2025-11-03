using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class UIController : MonoBehaviour
{   
    public GameManager Gamemanager;
    public GameObject HighScoreBoard;
    public Text First, Second, Third, Fourth, Fifth, Sixth;
    private List<float> Records = new List<float>(6);
    public GameObject ResultScreen, Spawner;
    public NodeSpawnManager NodeSpawnmanager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Gamemanager == null)
        {
            Gamemanager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        }
        for (int i = 0; i < 6; i++)
        {
            Records.Add(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ToHighScore()
    {   
        ResultScreen.SetActive(false);
        Gamemanager.resultsScreen.SetActive(false);
        Records.Add(Gamemanager.currentScore);
        Records.Sort((a, b) => b.CompareTo(a)); // sort descending
        if (Records.Count > 6) Records.RemoveAt(6); // keep top 6 only

        Text[] slots = { First, Second, Third, Fourth, Fifth, Sixth };
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < Records.Count)
                slots[i].text = Records[i].ToString("F2");
            else
                slots[i].text = "--";
        }
        HighScoreBoard.SetActive(true);
        Gamemanager.currentScore = 0;

    }
    public void Retry()
    {   
        ResultScreen.SetActive(false );
        HighScoreBoard.SetActive(false);
        //Gamemanager.nodeSpawnManager.SetActive(true);
        Spawner.SetActive(true);
        Gamemanager.startPlaying = false;
        Gamemanager.MusicTimeLeft = Gamemanager.music.length;
        NodeSpawnmanager = Spawner.GetComponent<NodeSpawnManager>();
        NodeSpawnmanager.StartSpawning();
    }
}
