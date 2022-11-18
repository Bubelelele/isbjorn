using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private UnityEvent startCutscene;

    private bool cutsceneDone;
    private bool camCheck;
    private bool moveCheck;
    private bool jumpCheck;
    private bool rollCheck;
    private bool sniffCheck;

    private bool canJump;
    private bool canRoll;
    private bool canSniff;

    void Start()
    {
        IconSystem.instance.CustomText("Move mouse to look around");
        startCutscene.Invoke();
    }

    private void Update()
    {
        if (cutsceneDone)
        {
            if (!camCheck && (Input.GetAxis("Mouse X") > 0.9f || Input.GetAxisRaw("Mouse X") < -0.9f || Input.GetAxisRaw("Mouse Y") > 0.9f || Input.GetAxisRaw("Mouse Y") < -0.9f))
            {
                Invoke("MoveDelay", 2);

            }
            if (!moveCheck && camCheck && (Keyboard.current.aKey.wasPressedThisFrame || Keyboard.current.sKey.wasPressedThisFrame || Keyboard.current.dKey.wasPressedThisFrame || Keyboard.current.wKey.wasPressedThisFrame))
            {
                IconSystem.instance.TextEnabled(false);
                moveCheck = true;
            }

            if (canJump && Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                jumpCheck = true;
                canJump = false;
                IconSystem.instance.TextEnabled(false);
            }
            if (canRoll && Mouse.current.rightButton.wasPressedThisFrame)
            {
                rollCheck = true;
                canRoll = false;
                IconSystem.instance.TextEnabled(false);
            }
            if (canSniff && Keyboard.current.qKey.wasPressedThisFrame)
            {
                sniffCheck = true;
                canSniff = false;
                IconSystem.instance.TextEnabled(false);
            }
        }
    }
    private void MoveDelay()
    {
        camCheck = true;
        IconSystem.instance.CustomText("Press WASD to move around");
    }

    public void JumpTutorial()
    {
        if (!jumpCheck)
        {
            IconSystem.instance.CustomText("Press SPACE to jump");
            canJump = true;
        }
    }
    public void RollTutorial()
    {
        if (!rollCheck)
        {
            IconSystem.instance.CustomText("Hold RMB while you're on \nslopes to roll");
            canRoll = true;
        }
    }
    public void SniffTutorial()
    {
        if (!sniffCheck)
        {
            IconSystem.instance.CustomText("Press Q to sense food closeby");
            canSniff = true;
        }
    }
    public void CutsceneDone()
    {
        cutsceneDone = true;
    }
}
