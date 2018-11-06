using UnityEngine;
using System.Collections;

public class FloatScript : MonoBehaviour {
    public enum driftdirs { up, left, down, right, none};
    [Header("Float Settings")]
    public float xDirection = 0.0f;
    public float yDirection = 0.0f;
    public float xDrift = 0.0f;
    public float yDrift = 0.0f;
    private float currXPos = 0.0f;
    private float currYPos = 0.0f;
    public float xVelLimit = 0.4f;
    public float yVelLimit = 0.4f;
    public float spinRate;
    public float xDriftRate = 0.4f;
    public float yDriftRate = 0.4f;

    [Header("Misc")]
    public driftdirs xDriftDir;
    public driftdirs yDriftDir;
    private bool inUse = false;

    [Header("Unity Objects")]
    private SpriteRenderer sprtRndr;
    private Rigidbody2D rgdBdy;


    public void SetInUse(bool use) {
        inUse = use;
    }

    public bool IsInUse() {
        return inUse;
    }

    public void SetCurrPos(Vector2 pos) {
        currXPos = pos.x;
        currYPos = pos.y;
    }

    public void SetVel(Vector2 vel) {
        if (rgdBdy == null) {
            rgdBdy = this.GetComponent<Rigidbody2D>();
        }
        rgdBdy.velocity = vel;
    }

    public void SetSprite(Sprite spr) {
        if (sprtRndr == null) {
            sprtRndr = this.GetComponent<SpriteRenderer>();
        }
        sprtRndr.sprite = spr;
    }

    public void SetColour(Color colo) {
        if(sprtRndr == null) {
            sprtRndr = this.GetComponent<SpriteRenderer>();
        }
        sprtRndr.color = colo;
    }

    // Use this for initialization
    void Start () {
        spinRate = Random.Range(-1.0f, 1.0f) * spinRate;
        if (xDirection != 0) {
            if (xDirection > 0) {   //  .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   Set initial direction
                xDriftDir = driftdirs.right;
            } else if (xDirection < 0) {
                xDriftDir = driftdirs.left;
            } else {
                xDriftDir = driftdirs.right;
            }
        } else {
            xDriftDir = driftdirs.none;
        }
        if (yDirection != 0) {
            float rnd = Random.Range(-1, 1);
            if (rnd > 0) {
                yDriftDir = driftdirs.up;
            } else if (rnd < 0) {
                yDriftDir = driftdirs.down;
            } else {
                yDriftDir = driftdirs.up;
            }
        } else {
            yDriftDir = driftdirs.none;
        }

        currXPos = this.transform.position.x;
        currYPos = this.transform.position.y;
        sprtRndr = this.GetComponent<SpriteRenderer>();
        rgdBdy = this.GetComponent<Rigidbody2D>();
        xDirection /= 100;
        yDirection /= 100;

        rgdBdy.angularVelocity = (spinRate + Random.Range(0.1f, -0.1f));

        inUse = true;
    }

    // Update is called once per frame
    public virtual void Update() {
        float tmpXFrc = 0.0f;
        float tmpYFrc = 0.0f;
        switch (xDriftDir) {    //  .   .   .   .   .   .   .   .   .   .   .   .   .   Move in direction
            case driftdirs.right:
                if (rgdBdy.velocity.x < xVelLimit) {
                    tmpXFrc += Random.Range(xDriftRate, 0);
                }
                break;
            case driftdirs.left:
                if (rgdBdy.velocity.x > (-xVelLimit)) {
                    tmpXFrc -= Random.Range(xDriftRate, 0);
                }
                break;
            default:
                break;
        }
        switch (yDriftDir) {
            case driftdirs.up:
                if (rgdBdy.velocity.y < yVelLimit) {
                    tmpYFrc += Random.Range(yDriftRate, 0);
                }
                break;
            case driftdirs.down:
                if (rgdBdy.velocity.y > (-yVelLimit)) {
                    tmpYFrc -= Random.Range(yDriftRate, 0);
                }
                break;
            default:
                break;
        }

        if (xDriftDir != driftdirs.none) {
            if (this.transform.position.x >= (currXPos + xDrift)) { //  .   .   .   .   .   Change direction based on follow target
                xDriftDir = driftdirs.left;
            } else if (this.transform.position.x <= (currXPos - xDrift)) {
                xDriftDir = driftdirs.right;
            }
        }

        if (yDriftDir != driftdirs.none) {
            if (this.transform.position.y >= (currYPos + yDrift)) {
                yDriftDir = driftdirs.down;
            } else if (this.transform.position.y <= (currYPos - yDrift)) {
                yDriftDir = driftdirs.up;
            }
        }

        if ((yDriftDir != driftdirs.none) && (xDriftDir != driftdirs.none)){ 
            rgdBdy.AddForce(new Vector2(tmpXFrc, tmpYFrc));

            currXPos += xDirection;
            currYPos += yDirection;
        }
    }
}
