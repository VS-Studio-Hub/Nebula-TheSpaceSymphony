using System;
using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject[] background;
    public GameObject[] tutorialImg;
    public TMP_Text description;
    private int progressCnt;
    public string[] text;
    
    void Start()
    {
        progressCnt = 0;
        ShowPage(progressCnt);
    }


    void Update()
    {
        if(Input.anyKeyDown)
            NextPage();
    }

    private void NextPage()
    {
        progressCnt++;
        if (progressCnt >= background.Length)
        {
            EndTutorial();
            return;
        }

        ShowPage(progressCnt);
    }

    private void ShowPage(int index)
    {
        for (int i = 0; i < background.Length; i++)
        {
            background[i].gameObject.SetActive(false);
            tutorialImg[i].gameObject.SetActive(false);
        }

        background[index].gameObject.SetActive(true);
        tutorialImg[index].gameObject.SetActive(true);
        description.text = text[index];
    }

    private void EndTutorial()
    {
        gameObject.SetActive(false);
        GameManager.startGame = true;
        GameManager.instance.StartMusic();
    }
}
