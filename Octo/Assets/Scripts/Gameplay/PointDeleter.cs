using UnityEngine;
using System.Collections;

public class PointDeleter : MonoBehaviour {

    public GameObject controllerObj;
    private Controller controllerScript;
    
    void Start() {
        controllerScript = controllerObj.GetComponent<Controller>();
    }

    public void HitPoint() {
        controllerScript.MissedPoint();
    }

    void OnTriggerEnter2D(Collider2D coll) {    //  *   *   *   *   *   *   *   *   *   *   Trigger hit
        switch (coll.gameObject.tag) {
            case "Points":
                break;
        }
    }
}

