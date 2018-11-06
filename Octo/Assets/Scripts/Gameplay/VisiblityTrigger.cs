using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VisiblityTrigger : MonoBehaviour {
    public enum direction {top, bottom, left, right};

    [Header("Unity Objects")]
    public List<GameObject> target = new List<GameObject>();

    [Header("Settings")]
    public bool toShow = false;
    private bool triggered = false;
    private List<VisibilityFade> visibilityFade = new List<VisibilityFade>();
    private bool entered = false;

    [Header("TOGGLING")]
    public bool toggle = false;
    public direction enterDirection = direction.top;

    void Start() {  //  *   *   *   *   *   *   *   *   *   *   *   *   *   *   Get fade scripts to trigger
        for (int i = 0; i < target.Count; i++) {
            visibilityFade.Add(target[i].GetComponent<VisibilityFade>());
        }
    }

    void OnTriggerEnter2D(Collider2D coll) {    //  *   *   *   *   *   *   *   On player enter trigger
        switch (coll.gameObject.tag) {
            case "Player":
                if (toggle) {   //  .   .   .   .   .   .   .   .   .   .   .   If togglable start fade objects if triggerd
                    if(coll.gameObject.transform.position.y > this.transform.position.y) {
                        if (enterDirection == direction.top) {
                            entered = true;
                            for (int i = 0; i < visibilityFade.Count; i++) {
                                visibilityFade[i].StartFade(false);
                            }
                        } else {
                            if (entered) {
                                for (int i = 0; i < visibilityFade.Count; i++) {
                                    visibilityFade[i].StartFade(true);
                                }
                                entered = false;
                            }
                        }
                    } else if (coll.gameObject.transform.position.y < this.transform.position.y) {

                        if (enterDirection == direction.bottom) {
                            entered = true;
                            for (int i = 0; i < visibilityFade.Count; i++) {
                                visibilityFade[i].StartFade(false);
                            }
                        } else {
                            if (entered) {
                                for (int i = 0; i < visibilityFade.Count; i++) {
                                    visibilityFade[i].StartFade(true);
                                }
                                entered = false;
                            }
                        }
                    }
                    if (coll.gameObject.transform.position.x > this.transform.position.x) {
                        if (enterDirection == direction.right) {
                            entered = true;
                            for (int i = 0; i < visibilityFade.Count; i++) {
                                visibilityFade[i].StartFade(false);
                            }
                        } else {
                            if (entered) {
                                for (int i = 0; i < visibilityFade.Count; i++) {
                                    visibilityFade[i].StartFade(true);
                                }
                                entered = false;
                            }
                        }
                    } else if (coll.gameObject.transform.position.x < this.transform.position.x) {
                        if (enterDirection == direction.left) {
                            entered = true;
                            for (int i = 0; i < visibilityFade.Count; i++) {
                                visibilityFade[i].StartFade(false);
                            }
                        } else {
                            if (entered) {
                                for (int i = 0; i < visibilityFade.Count; i++) {
                                    visibilityFade[i].StartFade(true);
                                }
                                entered = false;
                            }
                        }
                    }
                } else {    //  .   .   .   .   .   .   .   .   .   .   .   .   If not togglable fade objects either in or out (toshow)
                    if (!triggered) {
                        for (int i = 0; i < visibilityFade.Count; i++) {
                            visibilityFade[i].StartFade(toShow);
                        }
                        triggered = true;
                    }
                }
                break;
        }
    }
}
