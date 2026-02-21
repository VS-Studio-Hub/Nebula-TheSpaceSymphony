using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlanetSelectionManager : MonoBehaviour
{
    [SerializeField] GameObject frdBtn, bckBtn;
    [SerializeField] Button OpenLevel;

    [SerializeField] RectTransform planetsTransform;
    [SerializeField] float moveDistance = 1795f;
    [SerializeField] float moveSpeed = 4f;

    Vector2 planetPosition;
    bool moving;

    [SerializeField] GameObject[] planets;
    int currentPlanetIndex = 0;

    [SerializeField] TMP_Text planetNameText;
    string[] planetNames = { "Mercury", "Venus", "Earth" };
    int currentPlanetNameIndex = 0;


    void Start()
    {
        planetPosition = planetsTransform.anchoredPosition;
        planetNameText.text = planetNames[0];
    }

    void Update()
    {
        if (!moving)
        {
            frdBtn.SetActive(true);
            bckBtn.SetActive(true);
            return;
        }


        planetsTransform.anchoredPosition = Vector2.Lerp(planetsTransform.anchoredPosition, planetPosition, moveSpeed * Time.deltaTime);
        frdBtn.SetActive(false);
        bckBtn.SetActive(false);
        // SNAP to exact target when close
        if (Vector2.Distance(planetsTransform.anchoredPosition, planetPosition) < 0.5f)
        {
            planetsTransform.anchoredPosition = planetPosition;
            moving = false;
        }
    }

    public void PlanetMovementLeft()
    {
        if (moving) return;

        if (currentPlanetIndex >= planets.Length - 1)
        {

            return;
        }
        planetPosition += Vector2.left * moveDistance;
        moving = true;

        currentPlanetNameIndex++;
        PlanetScaleDown();
        currentPlanetIndex++;
    }

    public void PlanetMovementRight()
    {
        if (moving) return;
        if (currentPlanetIndex <= 0) return;
        planetPosition += Vector2.right * moveDistance;
        moving = true;

        currentPlanetNameIndex--;
        currentPlanetIndex--;
        PlanetScaleUp();
    }

    void PlanetScaleDown()
    {
        planets[currentPlanetIndex].transform.localScale = Vector2.one * .5f;
        planetNameText.text = planetNames[currentPlanetNameIndex];
    }

    void PlanetScaleUp()
    {
        planets[currentPlanetIndex].transform.localScale = Vector2.one;
        planetNameText.text = planetNames[currentPlanetNameIndex];
    }

    public void PlanetOne()
    {
        if (currentPlanetIndex == 0)
        {
            SceneManager.LoadScene("Level_01");
            OpenLevel.SetEnabled(true);
        }
        else
        {
            OpenLevel.SetEnabled(false);
        }
    }


    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
