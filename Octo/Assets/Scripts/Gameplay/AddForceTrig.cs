using UnityEngine;
using System.Collections;

public class AddForceTrig : MonoBehaviour {

    public Vector2 force = new Vector2(0.0f, 0.0f);
    public bool ForcePush = false;
    private bool ForcePushed = false;

    // Disable obj
    void Start () {
        this.gameObject.SetActive(false);
	}
	
	// Add force on animation
	void Update () {
        if (ForcePush) {
            if (!ForcePushed) {
                this.gameObject.SetActive(true);
                this.GetComponent<Rigidbody2D>().AddForce(force);
                ForcePushed = true;
            }
        }
	}
}
