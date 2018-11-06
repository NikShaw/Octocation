using UnityEngine;
using System.Collections;

public class SetPerData : MonoBehaviour {

    public bool TILT = false;
    public bool MUTE = false;
    public Sprite img;
    public Sprite notImg;
    private ButtonScript btnScript;
    private GameObject perData;
    private PersistentData perDataScript;
    private SpriteRenderer sprRndr;
    private SoundManager soundMan;
    
    // Set tilt/mute values
	void Start () {
        sprRndr = this.GetComponent<SpriteRenderer>();
        btnScript = this.gameObject.GetComponent<ButtonScript>();
        perData = GameObject.Find("Persistent Data");
        perDataScript = perData.GetComponent<PersistentData>();
        soundMan = GameObject.Find("Sound Manager").GetComponent<SoundManager>();
        if (TILT) {
            if (perDataScript.getTilt() == 1) {
                sprRndr.sprite = img;
            } else {
                sprRndr.sprite = notImg;
            }
        }
        if (MUTE) {
            if (perDataScript.getMute() == 1) {
                sprRndr.sprite = img;
                soundMan.Unmute();
            } else {
                sprRndr.sprite = notImg;
                soundMan.Mute();
            }
        }
    }
	
	void Update () {
        if (btnScript.isPressed()) {
            if (TILT) {
                perDataScript.setTilt();
                if (perDataScript.getTilt() == 1) {
                    sprRndr.sprite = img;
                } else {
                    sprRndr.sprite = notImg;
                }
            }
            if (MUTE) {
                perDataScript.setMute();
                if (perDataScript.getMute() == 1) {
                    sprRndr.sprite = img;
                    soundMan.Unmute();
                } else {
                    sprRndr.sprite = notImg;
                    soundMan.Mute();
                }
            }
        }
	}
}
