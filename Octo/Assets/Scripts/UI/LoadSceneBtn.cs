using UnityEngine;
using System.Collections;

public class LoadSceneBtn : MonoBehaviour {
    
    public int sceneIndex = 1;
    private bool pressed = false;
    private int pressedFinger = 0;

    void Start() {
    }

    void OnMouseDown() {
        if (sceneIndex == 0) {
            Application.LoadLevel(sceneIndex);
        }
        pressed = true;
    }

    void OnMouseUp() {
        pressed = false;
    }

    void Update() {
        if (Input.touchCount > 0) {
            for (int i = 0; i < Input.touchCount; i++) {
                Touch t = Input.GetTouch(i);
                Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
                if (t.phase == TouchPhase.Began) {
                    if (this.GetComponent<Collider2D>() == Physics2D.OverlapPoint(new Vector2(wp.x, wp.y))) {
                        pressedFinger = t.fingerId;
                        pressed = true;
                        if (sceneIndex == 0) {
                            Application.LoadLevel(sceneIndex);
                        }
                    } else if (pressedFinger == t.fingerId) {
                        pressed = false;
                    }
                }
            }
        }
    }

    public bool isHeld() {
        return pressed;
    }

    public bool isPressed() {
        if (pressed) {
            pressed = false;
            return true;
        }
        return pressed;
    }
}