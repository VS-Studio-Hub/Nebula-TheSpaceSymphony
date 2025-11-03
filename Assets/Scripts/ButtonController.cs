using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputAction;

    private InputAction upButtonPressAction;
    private InputAction downButtonPressAction;
    private InputAction leftButtonPressAction;
    private InputAction rightButtonPressAction;

    public GameObject upButton;
    public GameObject downButton;
    public GameObject leftButton;
    public GameObject rightButton;

    private Animator upAnimator;
    private Animator downAnimator;
    private Animator leftAnimator;
    private Animator rightAnimator;

    float upButtonInput;
    float leftButtonInput;
    float rightButtonInput;
    float downButtonInput;

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
        upButtonPressAction = playerMap.FindAction("UpButton");
        leftButtonPressAction = playerMap.FindAction("LeftButton");
        rightButtonPressAction = playerMap.FindAction("RightButton");
        downButtonPressAction = playerMap.FindAction("DownButton");
    }

    private void Start()
    {
        upAnimator = upButton.GetComponent<Animator>();
        downAnimator = downButton.GetComponent<Animator>();
        leftAnimator = leftButton.GetComponent<Animator>();
        rightAnimator = rightButton.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        HandleButtonPresses();
    }

    void GetInput()
    {
        upButtonInput = upButtonPressAction.ReadValue<float>();
        leftButtonInput = leftButtonPressAction.ReadValue<float>();
        rightButtonInput = rightButtonPressAction.ReadValue<float>();
        downButtonInput = downButtonPressAction.ReadValue<float>();
    }

    void HandleButtonPresses()
    {
        

        if (upButtonInput > 0.1f)
        {
            upAnimator.SetBool("PressedB",true);
            /*leftPressed = false;
            rightPressed = false;
            downPressed = false;*/
        }
        else
        {
            upAnimator.SetBool("PressedB", false);
            /*upPressed = true;
            leftPressed = true;
            rightPressed = true;
            downPressed = true;*/
        }

        if (leftButtonInput > 0.1f)
        {
            leftAnimator.SetBool("PressedB", true);
            /*upPressed = false;
            rightPressed = false;
            downPressed = false;*/
        }
        else
        {
            leftAnimator.SetBool("PressedB", false);
            /*upPressed = true;
            leftPressed = true;
            rightPressed = true;
            downPressed = true;*/
        }

        if (rightButtonInput > 0.1f)
        {
            rightAnimator.SetBool("PressedB", true);
            /*upPressed = false;
            leftPressed = false;
            downPressed = false;*/
        }
    
        else
        {
            rightAnimator.SetBool("PressedB", false);
            /*upPressed = true;
            leftPressed = true;
            rightPressed = true;
            downPressed = true;*/
        }

        if (downButtonInput > 0.1f)
        {
            downAnimator.SetBool("PressedB", true);
            /*upPressed = false;
            leftPressed = false;
            rightPressed = false;*/
        }
        else
        {
            downAnimator.SetBool("PressedB", false);
            /*upPressed = true;
            leftPressed = true;
            rightPressed = true;
            downPressed = true;*/
        }
    }
}
