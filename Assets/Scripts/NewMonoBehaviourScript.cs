using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public InputActionAsset InputActions;

    private InputAction hitAAction;
    private InputAction hitSAction;
    private InputAction hitDAction;
    private InputAction hitFAction;

   // bool emptyPress = true;

    //bool emptyPressLaneOne, emptyPressLaneTwo, emptyPressLaneThree, emptyPressLaneFour = true;

    bool emptyPressLaneOne = true;
    bool emptyPressLaneTwo = true;
    bool emptyPressLaneThree = true;
    bool emptyPressLaneFour = true;

    bool laneOne, laneTwo, laneThree, laneFour;

    private void OnEnable()
    {
        InputActions.FindActionMap("PlayerInput");
    }

    private void Awake()
    {
        hitAAction = InputSystem.actions.FindAction("HitA");
        hitSAction = InputSystem.actions.FindAction("HitS");
        hitDAction = InputSystem.actions.FindAction("HitD");
        hitFAction = InputSystem.actions.FindAction("HitF");
    }

    private void Update()
    {
        if (hitAAction.WasPressedThisFrame() && emptyPressLaneOne && laneOne)
        {
            GameManager.instance.EmptyPressCount();
        }
        else if (hitSAction.WasPressedThisFrame() && emptyPressLaneTwo && laneTwo)
        {
            GameManager.instance.EmptyPressCount();
        }
        else if (hitDAction.WasPressedThisFrame() && emptyPressLaneThree && laneThree)
        {
            GameManager.instance.EmptyPressCount();
        }
        else if (hitFAction.WasPressedThisFrame() && emptyPressLaneFour && laneFour)
        {
            GameManager.instance.EmptyPressCount();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LaneOne"))
        {
            laneOne = true;
        }
        else if (other.CompareTag("LaneTwo"))
        {
            laneTwo = true;
        }
        else if (other.CompareTag("LaneThree"))
        {
            laneThree = true;
        }
        else if (other.CompareTag("LaneFour"))
        {
            laneFour = true;
        }

        if (other.CompareTag("ShortNote") && laneOne)
        {
            emptyPressLaneOne = false;
        }
        else if(other.CompareTag("ShortNote") && laneTwo)
        {
            emptyPressLaneTwo = false;
        }
        else if(other.CompareTag("ShortNote") && laneThree)
        {
            emptyPressLaneThree = false;
        }
        else if(other.CompareTag("ShortNote") && laneFour)
        {
            emptyPressLaneFour = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ShortNote") && !emptyPressLaneOne && laneOne)
        {
            emptyPressLaneOne = true;
        }
        else if (other.CompareTag("ShortNote") && !emptyPressLaneTwo && laneTwo)
        {
            emptyPressLaneTwo = true;
        }
        else if (other.CompareTag("ShortNote") && !emptyPressLaneThree && laneThree)
        {
            emptyPressLaneThree = true;
        }
        else if (other.CompareTag("ShortNote") && !emptyPressLaneFour && laneFour)
        {
            emptyPressLaneFour = true;
        }
    }
}
