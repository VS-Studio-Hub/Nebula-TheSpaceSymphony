using UnityEngine;

public class Settings : MonoBehaviour
{
    public GameObject settingsMenu;


    public void CloseSettings()
    {
        settingsMenu.SetActive(false);
    }
}
