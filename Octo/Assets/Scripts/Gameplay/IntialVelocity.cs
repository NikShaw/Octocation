using UnityEngine;
using System.Collections;

public class IntialVelocity : MonoBehaviour {

    public Vector2 force;
    private GameObject cam;

    // Use this for initialization
    void Start() {
        this.GetComponent<Rigidbody2D>().AddForce(force);
        cam = GameObject.Find("Main Camera");
    }

    void Update() {
        if(cam.GetComponent<Camera>().WorldToScreenPoint(this.transform.position).y < 0) {
            Destroy(this.transform.parent.gameObject);
        }
    }
}
