using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonScript : MonoBehaviour {

    private bool pressed = false;
    private bool held = false;
    private bool fingerDown = false;
    private int pressedFinger = 0;
    public Sprite pressedSprite;
    public List<string> sounds = new List<string>();
    private Sprite initSprite;
    private SpriteRenderer sprtRndr;
    private SoundManager soundMan;

    void Start() {
        soundMan = GameObject.Find("Sound Manager").GetComponent<SoundManager>();
        sprtRndr = this.GetComponent<SpriteRenderer>();
        if (sprtRndr != null) {
            initSprite = sprtRndr.sprite;
        }
    }

    public void PressedSprite() {
        sprtRndr.sprite = pressedSprite;
    }

    public void normalSprite() {
        sprtRndr.sprite = initSprite;
    }

    public void OnMouseDown() {
        if (pressedSprite != null) {
            sprtRndr.sprite = pressedSprite;
        }
        if (!fingerDown) {
            held = true;
        }
    }

    public void DoMouseUp() {
        pressed = true;
    }

    public void OnMouseUp() {
        if (!fingerDown) {
            held = false;
            pressed = true;
        }
    }

    void Update() { // Set pressed it touch
        if (Input.touchCount > 0) {
            for (int i = 0; i < Input.touchCount; i++) {
                Touch t = Input.GetTouch(i);
                Vector3 wp = Camera.main.ScreenToWorldPoint(t.position);
                if (t.phase == TouchPhase.Began) {
                    if(this.GetComponent<Collider2D>() == Physics2D.OverlapPoint(new Vector2(wp.x, wp.y))) {
                        pressedFinger = t.fingerId;
                        held = true;
                        fingerDown = true;
                    }
                } else if (t.phase == TouchPhase.Ended) {
                    if (pressedFinger == t.fingerId) {
                        if (this.GetComponent<Collider2D>() == Physics2D.OverlapPoint(new Vector2(wp.x, wp.y))) {
                            pressed = true;
                        }
                        held = false;
                        sprtRndr.sprite = initSprite;
                        pressedFinger = -10;
                    }
                }
            }
        }
    }

    public bool isHeld() {
        return held;
    }

    public bool isPressed() {
        if (pressed) {
            for (int i = 0; i < sounds.Count; i++) {
                soundMan.PlaySound(sounds[i], false, false, Vector3.zero, 1.0f);
            }
            pressed = false;
            return true;
        }
        return pressed;
    }
}