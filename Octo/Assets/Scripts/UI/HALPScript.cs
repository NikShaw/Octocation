using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HALPScript : MonoBehaviour {

    public List<GameObject> halpObjs = new List<GameObject>();
    private ButtonScript btnScript;
    private int currHalp = 0;
    private bool done = false;
    private SoundManager soundMan;

    void Start() {
        soundMan = GameObject.Find("Sound Manager").GetComponent<SoundManager>();
        btnScript = this.GetComponent<ButtonScript>();
    }

    public bool ShowNext() {    // Shows next help screen
        if (currHalp != halpObjs.Count) {
            soundMan = GameObject.Find("Sound Manager").GetComponent<SoundManager>();
            soundMan.PlaySound("chalkboard", false, false, Vector2.zero, 1.0f);
            halpObjs[currHalp].SetActive(true);
            if(currHalp > 0) {
                halpObjs[currHalp - 1].SetActive(false);
            }
            currHalp++;
            return true;
        } else {
            halpObjs[currHalp - 1].SetActive(false);
            done = true;
            this.gameObject.SetActive(false);
        }

        return false;
    }

    void Update() {
        if (btnScript.isPressed()) {
            ShowNext();
        }
    }

    public bool Done() {
        return done;
    }

    public void Reset() {
        currHalp = 0;
        done = false;
        this.gameObject.SetActive(true);
    }
}
