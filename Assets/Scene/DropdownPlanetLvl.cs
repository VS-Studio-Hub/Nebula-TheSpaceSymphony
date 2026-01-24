using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DropdownPlanetLvl : MonoBehaviour
{
    [Header("UI")]
    public Dropdown dropdown;

    [Header("Objects to Switch")]
    public GameObject[] gameObjects;

    void Start()
    {
        // Set default dropdown value (0 = first option)
        dropdown.value = 0;
        dropdown.RefreshShownValue();

        // Activate the correct GameObject at start
        ActivateObject(0);

        // Listen for dropdown changes
        dropdown.onValueChanged.AddListener(ActivateObject);
    }

    public void ActivateObject(int index)
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].SetActive(i == index);
        }
    }

    public void Back()
    {
        SceneManager.LoadScene("Solar System");
    }
}
