using UnityEngine;

public class PP2AreaMove : MonoBehaviour
{
    float rotationSpeed = 30f;

    float obliquityDeg = 23.5f;

    float precessionSpeed = 5f;

    float nutationAmplitude = 2f;
    float nutationHz = 0.2f;

    private Renderer targetRenderer;
    private int materialIndex = 0;

    static readonly int ID_EmitColour = Shader.PropertyToID("_EmitColour");
    MaterialPropertyBlock _mpb;

    void Update()
    {
        float t = Time.time;

        float precessionDeg = t * precessionSpeed;

        Vector3 h = Quaternion.AngleAxis(precessionDeg, Vector3.up) * Vector3.forward;
        Vector3 tiltAxis = Vector3.Cross(h, Vector3.up).normalized;
        float tiltDeg = obliquityDeg + Mathf.Sin(t * Mathf.PI * 2f * nutationHz) * nutationAmplitude;
        Vector3 spinAxis = (Quaternion.AngleAxis(tiltDeg, tiltAxis) * Vector3.up).normalized;
        transform.Rotate(spinAxis, rotationSpeed * Time.deltaTime, Space.World);

    }

    public void SetSpin(float degPerSec) => rotationSpeed = degPerSec;
    public void SetObliquity(float deg) => obliquityDeg = deg;
    public void SetPrecession(float degPerSec) => precessionSpeed = degPerSec;
    public void SetNutation(float ampDeg, float hz) { nutationAmplitude = ampDeg; nutationHz = hz; }

    void OnEnable()
    {
        if (!targetRenderer) targetRenderer = GetComponent<Renderer>();
        _mpb = new MaterialPropertyBlock();

        ApplyColor(PP1SimpleEmissionDriver.SavedEmitColor);

        PP1SimpleEmissionDriver.EmitColorChanged += ApplyColor;
    }

    void OnDisable()
    {
        PP1SimpleEmissionDriver.EmitColorChanged -= ApplyColor;
    }

    void ApplyColor(Color c)
    {
        if (!targetRenderer) return;
        targetRenderer.GetPropertyBlock(_mpb, materialIndex);
        _mpb.SetColor(ID_EmitColour, c);
        targetRenderer.SetPropertyBlock(_mpb, materialIndex);
    }


}
