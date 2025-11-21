using System;
using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class PP1SimpleEmissionDriver : MonoBehaviour
{
    [SerializeField] private Renderer targetRenderer;
    [SerializeField] private int materialIndex = 1;

    const float EdgeSoft_min = 0.1f;
    const float EdgeSoft_max = 1f;
    const float EdgeSoft_default = 0.1f;

    const float EmitInt_min = 0.5f;
    const float EmitInt_max = 7f;
    const float EmitInt_default = 1f;

    const float RimExp_min = 1f;
    const float RimExp_max = 8f;
    const float RimExp_default = 3f;

    const float RimStr_min = 0.5f;
    const float RimStr_max = 4f;
    const float RimStr_default = 1f;

    private const float PulseRange = 3f;

    [SerializeField, Range(EdgeSoft_min, EdgeSoft_max)] private float edgeSoftness = EdgeSoft_default;
    [SerializeField, Range(EmitInt_min, EmitInt_max)] private float emissionIntensity = EmitInt_default;
    [SerializeField, Range(RimExp_min, RimExp_max)] private float rimExponent = RimExp_default;
    [SerializeField, Range(RimStr_min, RimStr_max)] private float rimStrenght = RimStr_default;
    [SerializeField] private Color emitColour = Color.red;

    // Other phases subscribe to this
    public static event Action<Color> EmitColorChanged;

    public static Color SavedEmitColor { get; private set; } = Color.red;

    // Shader property IDs
    static readonly int ID_EdgeSoftness = Shader.PropertyToID("_EdgeSoftness");
    static readonly int ID_EmissionIntensity = Shader.PropertyToID("_EmissionIntensity");
    static readonly int ID_RimExponent = Shader.PropertyToID("_RimExponent");
    static readonly int ID_RimStrenght = Shader.PropertyToID("_RimStrenght");
    static readonly int ID_EmitColour = Shader.PropertyToID("_EmitColour");

    MaterialPropertyBlock _mpb;

    Coroutine _pulseCo;
    bool _pulseUp = true;

    void Awake()
    {
        if (!targetRenderer) targetRenderer = GetComponent<Renderer>();
        _mpb = new MaterialPropertyBlock();
        ApplyAll();

        // On load, broadcast current color once
        SavedEmitColor = emitColour;
        EmitColorChanged?.Invoke(SavedEmitColor);

    }

    public void Update()
    {
        SavedEmitColor = emitColour;

    }

    /// Randomize all editable values within specified ranges and save the picked colour
    public void Randomize(int? seed = null)
    {
        var rng = seed.HasValue ? new System.Random(seed.Value) : new System.Random();

        edgeSoftness = Lerp(EdgeSoft_min, EdgeSoft_max, rng);
        emissionIntensity = Lerp(EmitInt_min, EmitInt_max, rng);
        rimExponent = Lerp(RimExp_min, RimExp_max, rng);
        rimStrenght = Lerp(RimStr_min, RimStr_max, rng);

        // Pick a random color
        float h = (float)rng.NextDouble();
        float s = Mathf.Lerp(0.6f, 1.0f, (float)rng.NextDouble());
        float v = Mathf.Lerp(0.8f, 1.2f, (float)rng.NextDouble());
        emitColour = Color.HSVToRGB(h, s, 1f);
        emitColour *= v;

        ApplyAll();

        SavedEmitColor = emitColour; // Colour needs to be available for next 2 phases
        EmitColorChanged?.Invoke(SavedEmitColor);

        Pulse();
    }


    public void Pulse(float intervalSeconds = 0.2f)
    {
        if (_pulseCo != null) StopCoroutine(_pulseCo);
        _pulseCo = StartCoroutine(PulseRoutine(intervalSeconds, emissionIntensity));
    }


    IEnumerator PulseRoutine(float intervalSeconds, float baseValue)
    {
        float t = 0f;
        float pulseSpeed = 2f;
        var wait = new WaitForSeconds(intervalSeconds);

        while (true)
        {
            t += intervalSeconds * pulseSpeed;

            float sine = Mathf.Sin(t);

            float newValue = baseValue + (sine * PulseRange);

            newValue = Mathf.Clamp(newValue, EmitInt_min, EmitInt_max);

            emissionIntensity = newValue;

            if (!targetRenderer) yield break;
            targetRenderer.GetPropertyBlock(_mpb, materialIndex);
            _mpb.SetFloat(ID_EmissionIntensity, emissionIntensity);
            targetRenderer.SetPropertyBlock(_mpb, materialIndex);

            yield return wait;
        }
    }


    public void ApplyAll()
    {
        if (!targetRenderer) return;
        targetRenderer.GetPropertyBlock(_mpb, materialIndex);

        _mpb.SetFloat(ID_EdgeSoftness, Mathf.Clamp(edgeSoftness, EdgeSoft_min, EdgeSoft_max));
        _mpb.SetFloat(ID_EmissionIntensity, Mathf.Clamp(emissionIntensity, EmitInt_min, EmitInt_max));
        _mpb.SetFloat(ID_RimExponent, Mathf.Clamp(rimExponent, RimExp_min, RimExp_max));
        _mpb.SetFloat(ID_RimStrenght, Mathf.Clamp(rimStrenght, RimStr_min, RimStr_max));
        _mpb.SetColor(ID_EmitColour, emitColour);

        targetRenderer.SetPropertyBlock(_mpb, materialIndex);
    }


#if UNITY_EDITOR
    void OnValidate()
    {
        if (!targetRenderer) targetRenderer = GetComponent<Renderer>();
        if (targetRenderer != null && _mpb != null) ApplyAll();
    }
#endif


    static float Lerp(float a, float b, System.Random rng)
        => Mathf.Lerp(a, b, (float)rng.NextDouble());
}