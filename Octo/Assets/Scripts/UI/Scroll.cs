using UnityEngine;
using System.Collections;

public class Scroll : MonoBehaviour {
    public Vector3 newPosition = new Vector3(0.0f, 0.0f, 0.0f);
    public Vector3 initPosition = new Vector3(0.0f, 0.0f, 0.0f);
    public Vector3 touchPosition = new Vector3(0.0f, 0.0f, 0.0f);
    public Vector3 prevPosition = new Vector3(0.0f, 0.0f, 0.0f);
    public float posVel = 0.0f;
    public float posStart = 0.0f;
    public float posEnd = 0.0f;
    public float offset = 0.0f;
    public bool touching = false;
    public bool started = false;
    public GameObject parentobj;

    void Start() {  // Inits
        if (!started) { 
            newPosition = this.transform.position;
            initPosition = this.transform.position;
            posVel = 0.0f;
            posStart = 0.0f;
            posEnd = 0.0f;
            offset = 0.0f;
            started = true;
        }
    }

    
    void OnEnable() {   // Sets positions
        if (started) {
            this.transform.position = initPosition;
            newPosition = this.transform.position;
            posVel = 0.0f;
            posStart = 0.0f;
            posEnd = 0.0f;
            offset = 0.0f;
        } else {
            Start();
            OnEnable();
        }
    }

    void OnMouseDown() {
        if (started) {
            touching = true;
            posStart = this.transform.position.y;
            touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            offset = touchPosition.y - this.transform.position.y;
        }
    }

    void OnMouseDrag() {
        prevPosition = this.transform.position;
        newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = transform.position.z;
        newPosition.x = initPosition.x;
        newPosition.y -= offset;
        this.transform.position = newPosition;
    }

    void OnMouseUp() {
        if (started) {
            if (touching) {
                touching = false;
                posEnd = this.transform.position.y;
                posVel = posEnd - prevPosition.y;
                if ((posVel > 0.01f) || (posVel < -0.01f)) {
                    posVel *= 1750.0f;
                }
                this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, posVel));
            }
        }
    }
    void Update() {
        if (started) {
            if (parentobj) {
                parentobj.transform.position = this.transform.position;
            }
            if (touching) {
            }
        }
    }
}
