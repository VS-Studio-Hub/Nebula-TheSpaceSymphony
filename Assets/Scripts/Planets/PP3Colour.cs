using UnityEngine;

public class PP3Colour : MonoBehaviour
{
    
    private Renderer targetRenderer;
    private int materialIndex = 0;

    static readonly int ID_EmitColour = Shader.PropertyToID("_EmitColour");
    MaterialPropertyBlock _mpb;

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
