using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonController : MonoBehaviour
{
    private GameControls controls;

    [Header("Button Animators")]
    public Animator aAnimator;
    public Animator sAnimator;
    public Animator dAnimator;
    public Animator fAnimator;

    private void Awake()
    {
        controls = new GameControls();
    }

    private void OnEnable()
    {
        controls.PlayerInput.Enable();

        controls.PlayerInput.HitA.performed += ctx => HoldKey(aAnimator, true);
        controls.PlayerInput.HitS.performed += ctx => HoldKey(sAnimator, true);
        controls.PlayerInput.HitD.performed += ctx => HoldKey(dAnimator, true);
        controls.PlayerInput.HitF.performed += ctx => HoldKey(fAnimator, true);

        controls.PlayerInput.HitA.canceled += ctx => HoldKey(aAnimator, false);
        controls.PlayerInput.HitS.canceled += ctx => HoldKey(sAnimator, false);
        controls.PlayerInput.HitD.canceled += ctx => HoldKey(dAnimator, false);
        controls.PlayerInput.HitF.canceled += ctx => HoldKey(fAnimator, false);
    }

    private void OnDisable()
    {
        controls.PlayerInput.Disable();
    }

    void HoldKey(Animator anim, bool isPressed)
    {
        anim.SetBool("Hold", isPressed);
    }
}



