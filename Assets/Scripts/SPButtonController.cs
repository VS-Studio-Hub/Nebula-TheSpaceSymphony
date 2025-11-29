using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SPButtonController : MonoBehaviour
{
    public static SPButtonController instance;

    private GameControls controls;

    [Header("Button Animators")]
    public Animator aAnimator;
    public Animator sAnimator;
    public Animator dAnimator;
    public Animator fAnimator;

    private bool swappedOne = false;
    //private bool swappedTwo = false;
    //private bool swappedThree = false;
    //private bool swappedFour = false;

    private bool isSwapping = false;

    private int swapType;

    private void Awake()
    {
        instance = this;
        controls = new GameControls();
    }

    private void Start()
    {
        StartCoroutine(SwapLoop());
    }

    IEnumerator SwapLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(15f);
            swapType = 1;
            UpdateSwapState();
        }
    }

    void UpdateSwapState()
    {
        swappedOne = (swapType == 1);
        //swappedTwo = (swapType == 2);
        //swappedThree = (swapType == 3);
        //swappedFour = (swapType == 4);
    }

    private void OnEnable()
    {
        controls.PlayerInput.Enable();

        controls.PlayerInput.HitA.performed += ctx => OnKeyPressed("A", true);
        controls.PlayerInput.HitS.performed += ctx => OnKeyPressed("S", true);
        controls.PlayerInput.HitD.performed += ctx => OnKeyPressed("D", true);
        controls.PlayerInput.HitF.performed += ctx => OnKeyPressed("F", true);

        controls.PlayerInput.HitA.canceled += ctx => OnKeyPressed("A", false);
        controls.PlayerInput.HitS.canceled += ctx => OnKeyPressed("S", false);
        controls.PlayerInput.HitD.canceled += ctx => OnKeyPressed("D", false);
        controls.PlayerInput.HitF.canceled += ctx => OnKeyPressed("F", false);
    }

    private void OnDisable()
    {
        controls.PlayerInput.Disable();
    }

    void OnKeyPressed(string key, bool isPressed)
    {
        Animator anim = null;

        if (swappedOne)
        {
            isSwapping = true;
            if (key == "A") anim = fAnimator;
            if (key == "S") anim = dAnimator;
            if (key == "D") anim = sAnimator;
            if (key == "F") anim = aAnimator;
        }
        //else if (swappedTwo)
        //{
        //    if (key == "A") anim = sAnimator;
        //    if (key == "S") anim = dAnimator;
        //    if (key == "D") anim = fAnimator;
        //    if (key == "F") anim = aAnimator;
        //}
        //else if (swappedThree)
        //{
        //    if (key == "A") anim = dAnimator;
        //    if (key == "S") anim = fAnimator;
        //    if (key == "D") anim = aAnimator;
        //    if (key == "F") anim = sAnimator;
        //}
        //else if (swappedFour)
        //{
        //    if (key == "A") anim = fAnimator;
        //    if (key == "S") anim = aAnimator;
        //    if (key == "D") anim = sAnimator;
        //    if (key == "F") anim = dAnimator;
        //}
        else
        {
            isSwapping = false;
            if (key == "A") anim = aAnimator;
            if (key == "S") anim = sAnimator;
            if (key == "D") anim = dAnimator;
            if (key == "F") anim = fAnimator;
        }

        HoldKey(anim, isPressed);
    }

    void HoldKey(Animator anim, bool isPressed)
    {
        if (anim != null)
            anim.SetBool("Hold", isPressed);
    }

    public bool Swapped()
    {
        return isSwapping;
    }
}
