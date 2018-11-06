using UnityEngine;
using System.Collections;

public class ChangeCamTarget : MonoBehaviour {
    public enum direction { top, bottom };

    [Header("Settings")]
    public GameObject target;
    public bool moveDown = false;
    public float camSmoothness = 6.0f;

    [Header("TOGGLING")]
    public bool toggle = false;
    public GameObject exitTarget = null;
    public float camSmoothness2 = 6.0f;
    public direction enterDirection = direction.top;
    private bool entered = false;

    void OnTriggerEnter2D(Collider2D coll) {    //  *   *   *   *   *   *   *   *   *   *   *   Sets target of camera
        switch (coll.gameObject.tag) {
            case "Player":
                if (toggle) {   //  .   .   .   .   .   .   .   .   .   .   .   If togglable start change target based on enter direction
                    if (exitTarget != null) {
                        if (coll.gameObject.transform.position.y > this.transform.position.y) {
                            if (enterDirection == direction.top) {
                                entered = true;
                                Camera.main.GetComponent<CameraScript>().SetTarget(target, camSmoothness, moveDown);
                            } else {
                                if (entered) {
                                    Camera.main.GetComponent<CameraScript>().SetTarget(exitTarget, camSmoothness2, false);
                                    entered = false;
                                }
                            }
                        } else {
                            if (enterDirection == direction.bottom) {
                                entered = true;
                                Camera.main.GetComponent<CameraScript>().SetTarget(target, camSmoothness, moveDown);
                            } else {
                                if (entered) {
                                    Camera.main.GetComponent<CameraScript>().SetTarget(exitTarget, camSmoothness2, false);
                                    entered = false;
                                }
                            }
                        }
                    }
                } else {
                    Camera.main.GetComponent<CameraScript>().SetTarget(target, camSmoothness, moveDown);
                }
                break;
        }
    }
}
