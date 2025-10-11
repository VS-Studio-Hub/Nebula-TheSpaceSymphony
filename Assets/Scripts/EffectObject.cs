using UnityEngine;

public class EffectObject : MonoBehaviour
{
    public float lifetime = 1f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, lifetime);
    }
}
