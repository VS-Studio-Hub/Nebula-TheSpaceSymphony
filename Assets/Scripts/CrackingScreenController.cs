using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.InputSystem;
public class CrackingScreenController : MonoBehaviour
{
    public CustomPassVolume customPassVolume;
    private Material effectMaterial;

    
    private float MaskSizeID = 1;
    private float ContrastID = 1;
    public float speed = 0.5f;
    void Start()
    {
        if (customPassVolume == null)
            customPassVolume = GetComponent<CustomPassVolume>();

        
        foreach (var pass in customPassVolume.customPasses)
        {
            if (pass is FullScreenCustomPass fullScreenPass)
            {
                
                effectMaterial = fullScreenPass.fullscreenPassMaterial;
                MaskSizeID = 1;
                ContrastID = 1;
                effectMaterial.SetFloat("_MaskSize", MaskSizeID);


                effectMaterial.SetFloat("_Contrast", ContrastID);
                break;

            }
        }
    }

    void Update()
    {
 
    }
    public void EffectVisibility()
    {
 
            if (ContrastID >= 0.5f)
            {
                ContrastID -= 0.06f;
            }
            if (MaskSizeID > 0)
            {
                MaskSizeID -= 0.1f;
            }
        if (effectMaterial != null)
        {

            //float sizeValue = Mathf.PingPong(Time.time, 1.0f);
            effectMaterial.SetFloat("_MaskSize", MaskSizeID);


            effectMaterial.SetFloat("_Contrast", ContrastID);
            //EffectVisibility();
        }
    }
    public void EffectInVisibility()
    {

        if (ContrastID <= 0.5f)
        {
            ContrastID += 0.06f;
        }
        if (MaskSizeID >= 0)
        {
            MaskSizeID += 0.1f;
        }
        if (effectMaterial != null)
        {

            //float sizeValue = Mathf.PingPong(Time.time, 1.0f);
            effectMaterial.SetFloat("_MaskSize", MaskSizeID);


            effectMaterial.SetFloat("_Contrast", ContrastID);
            //EffectVisibility();
        }
    }
}
