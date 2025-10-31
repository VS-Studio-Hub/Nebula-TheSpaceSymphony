using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputAction;

    private InputAction a_ButtonPressAction;
    private InputAction s_ButtonPressAction;
    private InputAction d_ButtonPressAction;
    private InputAction f_ButtonPressAction;

    public GameObject a_Button, s_Button, d_Button, f_Button;

    private Animator a_Animator, s_Animator, d_Animator, f_Animator;

    float a_ButtonInput, s_ButtonInput, d_ButtonInput, f_ButtonInput;

    /*bool upPressed = true;
    bool leftPressed = true;
    bool rightPressed = true;
    bool downPressed = true;*/

    private void OnEnable()
    {
        inputAction.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        inputAction.FindActionMap("Player").Disable();
    }

    private void Awake()
    {
        var playerMap = inputAction.FindActionMap("Player");
        a_ButtonPressAction = playerMap.FindAction("A");
        s_ButtonPressAction = playerMap.FindAction("S");
        d_ButtonPressAction = playerMap.FindAction("D");
        f_ButtonPressAction = playerMap.FindAction("F");
    }

    private void Start()
    {
        s_Animator = s_Button.GetComponent<Animator>(); 
        f_Animator = f_Button.GetComponent<Animator>();
        a_Animator = a_Button.GetComponent<Animator>();
        d_Animator = d_Button.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        HandleButtonPresses();
    }

    void GetInput()
    {
        s_ButtonInput = s_ButtonPressAction.ReadValue<float>();
        a_ButtonInput = a_ButtonPressAction.ReadValue<float>();
        d_ButtonInput = d_ButtonPressAction.ReadValue<float>();
        f_ButtonInput = f_ButtonPressAction.ReadValue<float>();
    }

    void HandleButtonPresses()
    {

        if (s_ButtonInput > 0.1f)
        {
            s_Animator.SetBool("PressedB", true);
            /*leftPressed = false;
            rightPressed = false;
            downPressed = false;*/
        }
        else
        {
            s_Animator.SetBool("PressedB", false);
            /*upPressed = true;
            leftPressed = true;
            rightPressed = true;
            downPressed = true;*/
        }

        if (a_ButtonInput > 0.1f)
        {
            a_Animator.SetBool("PressedB", true);
            /*upPressed = false;
            rightPressed = false;
            downPressed = false;*/
        }
        else
        {
            a_Animator.SetBool("PressedB", false);
            /*upPressed = true;
            leftPressed = true;
            rightPressed = true;
            downPressed = true;*/
        }

        if (d_ButtonInput > 0.1f)
        {
            d_Animator.SetBool("PressedB", true);
            /*upPressed = false;
            leftPressed = false;
            downPressed = false;*/
        }
    
        else
        {
            d_Animator.SetBool("PressedB", false);
            /*upPressed = true;
            leftPressed = true;
            rightPressed = true;
            downPressed = true;*/
        }

        if (f_ButtonInput > 0.1f)
        {
            f_Animator.SetBool("PressedB", true);
            /*upPressed = false;
            leftPressed = false;
            rightPressed = false;*/
        }
        else
        {
            f_Animator.SetBool("PressedB", false);
            /*upPressed = true;
            leftPressed = true;
            rightPressed = true;
            downPressed = true;*/
        }
    }
}
