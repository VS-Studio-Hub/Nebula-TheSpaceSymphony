using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlanetLevelSelection : MonoBehaviour
{
    public Button[] button;

    public GameObject planetOne, planetTwo, planetThree;
    public TMP_Text planetLvlTxt;

    private void Start()
    {
        planetOne.SetActive(true);
        planetTwo.SetActive(false);
        planetThree.SetActive(false);

        planetLvlTxt.text = "Planet Level\n1";
        EventSystem.current.SetSelectedGameObject(button[0].gameObject);
    }
    public void PlanetLevelOne()
    {
        planetOne.SetActive(true);
        planetTwo.SetActive(false);
        planetThree.SetActive(false);
        planetLvlTxt.text = "Planet Level\n1";
        EventSystem.current.SetSelectedGameObject(button[0].gameObject);
    }

    public void PlanetLevelTwo()
    {
        planetOne.SetActive(false);
        planetTwo.SetActive(true);
        planetThree.SetActive(false);
        planetLvlTxt.text = "Planet Level\n2";
        EventSystem.current.SetSelectedGameObject(button[1].gameObject);
    }

    public void PlanetLevelThree()
    {
        planetOne.SetActive(false);
        planetTwo.SetActive(false);
        planetThree.SetActive(true);
        planetLvlTxt.text = "Planet Level\n3";
        EventSystem.current.SetSelectedGameObject(button[2].gameObject);
    }

    public void Back()
    {
        SceneManager.LoadScene("Solar System");
    }
}
