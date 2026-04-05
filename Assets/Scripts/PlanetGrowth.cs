using UnityEngine;

public class PlanetGrowth : MonoBehaviour
{
    [SerializeField] private PlanetTransition sourceTransition;

    [SerializeField] private float startHeight = 600f;
    [SerializeField] private float endHeight = 0f;

    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 endPosition;

    [SerializeField] private Vector3 startScale = Vector3.one;
    [SerializeField] private Vector3 endScale = Vector3.one;

    private void Awake()
    {
        transform.localPosition = startPosition;
        transform.localScale = startScale;

    }

    private void Update()
    {
        if (sourceTransition == null)
            return;

        float t = GetProgress(sourceTransition.height);

        transform.localPosition = Vector3.Lerp(startPosition, endPosition, t);
        transform.localScale = Vector3.Lerp(startScale, endScale, t);

    }

    private float GetProgress(float heightValue)
    {
        if (Mathf.Approximately(startHeight, endHeight))
            return 1f;

        float t = Mathf.InverseLerp(startHeight, endHeight, heightValue);
        return Mathf.Clamp01(t);

    }


}
