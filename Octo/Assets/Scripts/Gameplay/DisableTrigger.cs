using UnityEngine;
using System.Collections;

public class DisableTrigger : MonoBehaviour {

    public GameObject target;
    public bool disable = true;
    public bool animTrigger = false;
    public bool inAnimationTrigger = false;

    void Start() {
    }

    void Update() {
        if (inAnimationTrigger) {
            if (target.activeSelf == disable) {
                target.SetActive(!disable);
            }
            inAnimationTrigger = false;
        }
    }

    void OnTriggerEnter2D(Collider2D coll) {    //  *   *   *   *   *   *   *   *   *   *   *   Disables/enabls object
        if (!animTrigger) {
            switch (coll.gameObject.tag) {
                case "Player":
                    target.SetActive(!disable);
                    break;
            }
        }
    }
}
