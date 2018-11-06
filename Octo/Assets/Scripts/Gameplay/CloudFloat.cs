using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CloudFloat : MonoBehaviour {
    private enum driftdirs { up, left, down, right, none };

    [Header("Possible Sprites")]
    public List<Sprite> sprites = new List<Sprite>();

    [Header("Plankton Settings")]
    public float xDirection = 0.0f;
    public float yDirection = 0.0f;
    public float xDrift = 0.0f;
    public float yDrift = 0.0f;
    private float currXPos = 0.0f;
    private float currYPos = 0.0f;
    public float xVelLimit = 0.4f;
    public float yVelLimit = 0.4f;
    private float spinRate;
    private float maxAlpha = 0.5f;
    private Vector2 speeds = new Vector2(0.2f, 0.2f);

    [Header("Timers")]
    public float alphaDriftTime = 0.0f;
    public float alphaDriftTimer = 0.0f;
    public int blinkLimit = 5;
    //private int blinkTimer = 0;

    [Header("Unity Objects")]
    private SpriteRenderer sprtRndr;
    private Rigidbody2D rgdBdy;

    [Header("Misc")]
    private driftdirs xDriftDir;
    private driftdirs yDriftDir;
    private bool inUse = false;
    private bool alphaIncreasing = true;

    public void Init(Vector2 speed, Vector2 direction, Vector2 drift, Vector2 velLimit, float alphaTime, float MaxAlpha) { //  *   *   *   *   Post initialising (vec2 direction/drift/velocity limit, float alpha timer
        xDirection = direction.x;
        yDirection = direction.y;
        xDrift = drift.x;
        yDrift = drift.y;
        xVelLimit = velLimit.x;
        yVelLimit = velLimit.y;
        alphaDriftTime = alphaTime;
        inUse = true;
        speeds = speed;
        maxAlpha = MaxAlpha; 

        Start();
        currXPos = this.transform.position.x;
        currYPos = this.transform.position.y;
        rgdBdy.velocity = new Vector2(0.0f, 0.0f);
    }

    public void Disable() {
        inUse = false;
    }

    public bool IsInUse() {
        return inUse;
    }

    void Start() { //  *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   Pre-initialising
        this.transform.SetParent(GameObject.Find("Manyoclouds").transform);
        spinRate = Random.Range(-0.1f, 0.1f);

        if (xDirection > 0) {   //  .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   Set initial direction
            xDriftDir = driftdirs.right;
        } else if (xDirection < 0) {
            xDriftDir = driftdirs.left;
        } else {
            xDriftDir = driftdirs.right;
        }
        float rnd = Random.Range(-1, 1);
        if (rnd > 0) {
            yDriftDir = driftdirs.up;
        } else if (rnd < 0) {
            yDriftDir = driftdirs.down;
        } else {
            yDriftDir = driftdirs.up;
        }

        xDirection /= 100;
        yDirection /= 100;
        
        alphaDriftTimer = 0;

        currXPos = this.transform.position.x;
        currYPos = this.transform.position.y;
        sprtRndr = this.GetComponent<SpriteRenderer>();
        rgdBdy = this.GetComponent<Rigidbody2D>();

        rgdBdy.angularVelocity = (spinRate + Random.Range(0.1f, -0.1f));
        
        sprtRndr.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        alphaIncreasing = true;
        sprtRndr.sprite = sprites[Random.Range(sprites.Count - 1, 0)];
        inUse = true;
    }

    void Update() {    //  *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   Update
        if (inUse) {    //  .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   Disable object based on position and camera position
            if (((this.transform.position.y - sprtRndr.bounds.size.y) < (Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, 0.0f)).y - 15.0f)) || ((this.transform.position.y + sprtRndr.bounds.size.y) > (Camera.main.ScreenToWorldPoint(new Vector3(Screen.height, 0.0f, 0.0f)).y + 70.0f))) {
                sprtRndr.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                Disable();
            }
            if (((this.transform.position.x - sprtRndr.bounds.size.x) > (Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0.0f)).x + 1.0f)) || ((this.transform.position.x + sprtRndr.bounds.size.x) < (-10.0f))) {
                sprtRndr.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                Disable();
            }
            if (inUse) {
                float tmpXFrc = 0.0f;
                float tmpYFrc = 0.0f;

                if (alphaIncreasing) {  //  .   .   .   .   .   .   .   .   .   .   .   .   .   Fade in/out based on time
                    if (alphaDriftTimer < alphaDriftTime) {
                        alphaDriftTimer += Time.deltaTime;
                    } else {
                        alphaIncreasing = false;
                    }
                    sprtRndr.color = new Color(alphaDriftTimer / alphaDriftTime, alphaDriftTimer / alphaDriftTime, 1.0f, alphaDriftTimer / alphaDriftTime * maxAlpha);
                } else {
                }

                switch (xDriftDir) {    //  .   .   .   .   .   .   .   .   .   .   .   .   .   Move in direction
                    case driftdirs.right:
                        if (rgdBdy.velocity.x < xVelLimit) {
                            tmpXFrc += Random.Range(speeds.x, 0);
                        }
                        break;
                    case driftdirs.left:
                        if (rgdBdy.velocity.x > (-xVelLimit)) {
                            tmpXFrc -= Random.Range(speeds.x, 0);
                        }
                        break;
                }
                switch (yDriftDir) {
                    case driftdirs.up:
                        if (rgdBdy.velocity.y < yVelLimit) {
                            tmpYFrc += Random.Range(speeds.y, 0);
                        }
                        break;
                    case driftdirs.down:
                        if (rgdBdy.velocity.y > (-yVelLimit)) {
                            tmpYFrc -= Random.Range(speeds.y, 0);
                        }
                        break;
                }

                if (this.transform.position.x >= (currXPos + xDrift)) { //  .   .   .   .   .   Change direction based on follow target
                    xDriftDir = driftdirs.left;
                } else if (this.transform.position.x <= (currXPos - xDrift)) {
                    xDriftDir = driftdirs.right;
                }
                if (this.transform.position.y >= (currYPos + yDrift)) {
                    yDriftDir = driftdirs.down;
                } else if (this.transform.position.y <= (currYPos - yDrift)) {
                    yDriftDir = driftdirs.up;
                }

                rgdBdy.AddForce(new Vector2(tmpXFrc, tmpYFrc));

                currXPos += xDirection;
                currYPos += yDirection;
            }
        }
    }
}
