using UnityEngine;
using System.Collections;

public class DisabledIcon : MonoBehaviour {
    public GameObject icon;

    private GameObject persistentObj;
    private PersistentData persistentScript;
    
    void Start() {
        persistentObj = GameObject.Find("Persistent Data");
        persistentScript = persistentObj.GetComponent<PersistentData>();
        if(this.transform.parent.gameObject.tag == "TiltBtn") {
            getTiltStatus();
        } else {
            getMuteStatus();
        }
    }

    public void getTiltStatus() {
        if (persistentScript.getTilt() == 0) {
            icon.SetActive(true);
        } else {
            icon.SetActive(false);
        }
    }

    public void getMuteStatus() {
        if (persistentScript.getMute() == 0) {
            icon.SetActive(true);
        } else {
            icon.SetActive(false);
        }
    }
}
