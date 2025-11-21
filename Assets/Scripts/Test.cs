using UnityEngine;
using UnityEngine.InputSystem;

public class Test : MonoBehaviour
{

    [SerializeField] private PP1SimpleEmissionDriver emissionDriver;

    void Update()
    {
        if (Keyboard.current == null)
            return;

        // New Input System version of GetKeyDown
        if (Keyboard.current.yKey.wasPressedThisFrame)
        {
            if (emissionDriver != null)
            {
                emissionDriver.Randomize();
            }
        }
    }
}
