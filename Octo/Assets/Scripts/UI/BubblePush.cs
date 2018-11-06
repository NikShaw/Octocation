using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BubblePush : MonoBehaviour {

    public List<GameObject> bubbles = new List<GameObject>();
    public float distForce = 1.0f;
    public float pushForce = 1.0f;

    // Push bubbles away from where the user is touching
    void Update() {
        if (Input.touchCount > 0) {
            for (int i = 0; i < Input.touchCount; i++) {
                Touch t = Input.GetTouch(i);
                Vector3 wp = Camera.main.ScreenToWorldPoint(t.position);
                if (t.phase == TouchPhase.Began) {
                    for(int j = 0; j < bubbles.Count; j++) {
                        Vector3 heading = wp - bubbles[j].transform.position;
                        heading.z = 0;
                        float distance = heading.magnitude;
                        Vector3 direction = heading / distance;
                        direction.z = 0;
                        if (distance < distForce) {
                            bubbles[j].GetComponent<Rigidbody2D>().AddForce(direction * (distForce - distance) * pushForce);
                        }
                    }
                }
            }
        }
        if (Input.GetMouseButton(0)) {
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            for (int j = 0; j < bubbles.Count; j++) {
                Vector3 heading = wp - bubbles[j].transform.position;
                heading.z = 0;
                float distance = heading.magnitude;
                Vector3 direction = heading / distance;
                direction.z = 0;
                if (distance < distForce) {
                    bubbles[j].GetComponent<Rigidbody2D>().AddForce(direction * (distForce - distance) * pushForce);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if(coll.transform.tag == "Plankton") {
            bubbles.Add(coll.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D coll) {
        if (coll.transform.tag == "Plankton") {
            bubbles.Remove(coll.gameObject);
        }
    }
}
