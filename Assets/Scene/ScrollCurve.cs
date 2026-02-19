using UnityEngine;
using UnityEngine.UI;

public class ScrollCurve : MonoBehaviour
{
    [Header("Scroll View")]
    public ScrollRect scrollRect;   // Drag your Scroll View here

    [Header("Target UI")]
    public RectTransform target;    // UI element to change

    [Header("Your Spline (Curve)")]
    public AnimationCurve curve = AnimationCurve.Linear(0, 0, 1, 1);

    [Header("Scale Settings")]
    public float minScale = 1f;
    public float maxScale = 2f;

    void Update()
    {
        if (scrollRect == null || target == null) return;

        // Get scroll value (0  1)
        float t = scrollRect.verticalNormalizedPosition;

        // Run through curve (this is your spline)
        float curveValue = curve.Evaluate(t);

        // Convert to scale range
        float scale = Mathf.Lerp(minScale, maxScale, curveValue);

        // Apply scale
        target.localScale = Vector3.one * scale;
    }
}
