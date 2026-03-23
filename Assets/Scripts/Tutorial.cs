using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Tutorial : MonoBehaviour
{
    public GameObject tutorialOne;
    public GameObject tutorialTwo;
    public GameObject tutorialThree;

    public Button next;
    public Button previous;

    private int currentPage = 0;
    private float delay = 5f;

    void Start()
    {
        next.onClick.AddListener(NextPage);
        previous.onClick.AddListener(PreviousPage);

        ShowPage();
        StartCoroutine(AutoNext());
    }

    void ShowPage()
    {
        tutorialOne.SetActive(currentPage == 0);
        tutorialTwo.SetActive(currentPage == 1);
        tutorialThree.SetActive(currentPage == 2);

        previous.gameObject.SetActive(currentPage > 0);
        next.gameObject.SetActive(currentPage < 2);
    }

    IEnumerator AutoNext()
    {
        while (currentPage < 2)
        {
            yield return new WaitForSeconds(delay);
            NextPage();
        }
    }

    void NextPage()
    {
        if (currentPage < 2)
        {
            currentPage++;
            ShowPage();
        }
    }

    void PreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            ShowPage();
        }
    }
}

