using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Controls : MonoBehaviour {

    public enum eMouse {clickLeft, clickRight, clickMiddle};

    [Header("Keyboard Controls")]
    public string[] keyJump = { "space" };
    public string[] keyLeft = { "a" };
    public string[] keyRight = { "d" };

    [Header("Mouse Controls")]
    public eMouse mouseJump = eMouse.clickMiddle;
    public eMouse mouseLeft = eMouse.clickMiddle;
    public eMouse mouseRight = eMouse.clickMiddle;

    [Header("Unity Objects")]
    public GameObject leftButton;
    public GameObject rightButton;
    public GameObject screenButton;
    public GameObject pauseButton;
    public List<GameObject> pauseScreen = new List<GameObject>();
    private ButtonScript pauseButtonScript;
    private ButtonScript screenButtonScript;
    private ButtonScript leftButtonScript;
    private ButtonScript rightButtonScript;
    private GameObject persistentObj;
    private PersistentData persistentScript;

    [Header("Misc")]
    private bool bJump = false;
    private float fLeft = 0.0f;
    private float fRight = 0.0f;
    public float xSpeed = 3.0f;
    public float tiltDeadzone = 0.1f;
    public float tiltSpeed = 3.0f;
    private bool paused = false;
    private bool useTilt = true;
    
    void Start () { //  *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   Initialising                      
        leftButtonScript = leftButton.GetComponent<ButtonScript>();
        rightButtonScript = rightButton.GetComponent<ButtonScript>();
        screenButtonScript = screenButton.GetComponent<ButtonScript>();
        pauseButtonScript = pauseButton.GetComponent<ButtonScript>();
        persistentObj = GameObject.Find("Persistent Data");
        if (persistentObj != null) {    //  .   .   .   .   .   .   .   .   .   .   .   .   .   .   Load persistent data if exists (from main menus)
            persistentScript = persistentObj.GetComponent<PersistentData>();
            if (persistentScript.getTilt() == 1) {
                useTilt = true;
            } else {
                useTilt = false;
            }
        }
        Input.gyro.enabled = true;
    }
    
    void Update () {    //  *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   Update

        if (pauseButtonScript.isPressed()) {    //  .   .   .   .   .   .   .   .   .   .   .   .   Detect pause press and show pause screen
            if (paused) {
                for (int i = 0; i < pauseScreen.Count; i++) {
                    pauseScreen[i].SetActive(false);
                }
                paused = false;
            } else {
                for (int i = 0; i < pauseScreen.Count; i++) {
                    pauseScreen[i].SetActive(true);
                }
                paused = true;
            }
        }
        for (int iJ = 0; iJ < keyJump.Length; iJ++) {   //  .   .   .   .   .   .   .   .   .   .   Detect jump trigger
            if ((Input.GetKeyDown(keyJump[iJ])) || (screenButtonScript.isPressed())) {
                bJump = true;
            }
        }
        for (int iR = 0; iR < keyRight.Length; iR++) {  //  .   .   .   .   .   .   .   .   .   .   Detect right trigger
            if ((Input.GetKey(keyRight[iR])) || (rightButtonScript.isHeld())){
                fRight = 1.0f;
                fLeft = 0.0f;
            } else {
                fRight = 0.0f;
            }
        }

        for (int iL = 0; iL < keyLeft.Length; iL++) {   //  .   .   .   .   .   .   .   .   .   .   Detect left trigger
            if ((Input.GetKey(keyLeft[iL])) || (leftButtonScript.isHeld())) {
                fLeft = 1.0f;
                fRight = 0.0f;
            } else {
                fLeft = 0.0f;
            }
        }
        if (useTilt) {
            if ((fLeft == 0.0f) && (fRight == 0.0f)) {
                float phoneTiltx = Input.acceleration.x * tiltSpeed;   //  .   .   .   .   .   .   .   .   .   .   Tilt controls
                float phoneTilty = -Input.acceleration.x * tiltSpeed;
                if (fLeft == 0.0f) {
                    if (phoneTiltx > tiltDeadzone) {
                        fRight = phoneTiltx;
                    }
                }
                if (fRight == 0.0f) {
                    if (phoneTilty > tiltDeadzone) {
                        fLeft = phoneTilty;
                    }
                }
            }
        }
    }

    public bool PausePressed() {
        return paused;
    }

    public bool JumpPressed() {
        if (bJump) {
            bJump = false;
            return true;
        }
        return false;
    }

    public float LeftPressed() {
        return fLeft;
    }

    public float RightPressed() {
        return fRight;
    }
}
