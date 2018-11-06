using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CurrentScript : MonoBehaviour {

    [Header("Current Settings")]
    public float xVLimit = 1.0f;
    public float yVLimit = 1.0f;
    public float strength = 5.0f;
    public float yDir = 0.0f;
    public float xDir = 0.0f;
    private List<Rigidbody2D> rgdsBodies = new List<Rigidbody2D>();
    public GameObject shhh;
    private SoundManager soundMan;

    void Start() {
        soundMan = GameObject.Find("Sound Manager").GetComponent<SoundManager>();
        soundMan.PlaySound("watercurrent", true, true, new Vector2(this.transform.position.x, this.transform.position.y), 1.0f);
        xDir = shhh.transform.position.x - this.transform.position.x;
        yDir = shhh.transform.position.y - this.transform.position.y;
    }

    void OnEnable() {
        rgdsBodies.Clear();
    }

    void Update() { // Loop through rigid bodies in range and apply force
        if(rgdsBodies.Count > 0) {
            for(int i = 0; i < rgdsBodies.Count; i++) {
                Vector2 tempVel = new Vector2(0.0f, 0.0f);
                if (xVLimit > 0) {
                    if (rgdsBodies[i].velocity.x < xVLimit) {
                        tempVel = new Vector2(xDir * strength, tempVel.y);
                    }
                } else {
                    if (rgdsBodies[i].velocity.x > xVLimit) {
                        tempVel = new Vector2(xDir * strength, tempVel.y);
                    }
                }
                if (yVLimit > 0) {
                    if (rgdsBodies[i].velocity.y < yVLimit) {
                        tempVel = new Vector2(tempVel.x, yDir * strength);
                    }
                } else {
                    if (rgdsBodies[i].velocity.y > yVLimit) {
                        tempVel = new Vector2(tempVel.x, yDir * strength);
                    }
                }
            
                rgdsBodies[i].AddForce(tempVel);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D coll) {
        switch (coll.gameObject.tag) {
            case "Plankton":
                rgdsBodies.Add(coll.gameObject.GetComponent<Rigidbody2D>());
                break;
            case "Player":
                rgdsBodies.Add(coll.gameObject.GetComponent<Rigidbody2D>());
                break;
        }
    }

    void OnTriggerExit2D(Collider2D coll) {
        switch (coll.gameObject.tag) {
            case "Plankton":
                rgdsBodies.Remove(coll.gameObject.GetComponent<Rigidbody2D>());
                break;
            case "Player":
                rgdsBodies.Remove(coll.gameObject.GetComponent<Rigidbody2D>());
                break;
        }
    }
}
