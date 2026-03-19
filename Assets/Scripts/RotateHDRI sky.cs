using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
public class RotateHDRIsky : MonoBehaviour
{
    [SerializeField] Volume volume;
    [SerializeField] float rotationSpeed = 10f;
    private HDRISky sky;
    public GameObject[] targets;
    public string animationStateName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (volume.profile.TryGet(out sky))
        {
            // You have access to sky.rotation.value here
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (sky != null)
        {
            // Rotate the skybox over time
            sky.rotation.value += rotationSpeed * Time.deltaTime;
            if (sky.rotation.value >= 360)
            {
                sky.rotation.value = 0f;
            }
        }
        /*if (Input.GetKeyDown(KeyCode.R))
        {
            PlayAnimationOnAll();
        }*/
    }
    public void PlayAnimationOnAll()
    {
        foreach (GameObject target in targets)
        {
            Animator animator = target.GetComponent<Animator>();
            if (animator != null)
            {
                // Plays the state by name without parameters
                animator.Play(animationStateName);
            }
        }
    }
}
