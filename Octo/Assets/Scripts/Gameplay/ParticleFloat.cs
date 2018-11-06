using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleFloat : FloatScript {

    [Header("Possible Sprites")]
    public List<Sprite> sprites = new List<Sprite>();

    [Header("Timers")]
    public float alphaDriftTime = 0.0f;
    public float alphaDriftTimer = 0.0f;
    public int blinkLimit = 5;
    private int blinkTimer = 0;
    private bool alphaIncreasing = true;

    public void Init(Vector2 direction, Vector2 drift, Vector2 velLimit, float alphaTime) { //  *   *   *   *   Post initialising (vec2 direction/drift/velocity limit, float alpha timer
        xDirection = direction.x;
        yDirection = direction.y;
        xDrift = drift.x;
        yDrift = drift.y;
        xVelLimit = velLimit.x;
        yVelLimit = velLimit.y;
        alphaDriftTime = alphaTime;
        SetInUse(true);

        StartPart();
        SetCurrPos(new Vector2(this.transform.position.x, this.transform.position.y));
        SetVel(new Vector2(0.0f, 0.0f));
    }

    public void Disable() {
        SetInUse(false);
    }
    
	void StartPart() { //  *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   Pre-initialising
        this.transform.SetParent(GameObject.Find("Manyoplankton").transform);
        blinkTimer = 0;
        alphaDriftTimer = 0;
        SetColour(new Color(1.0f, 1.0f, 1.0f, 0.0f));
        SetSprite(sprites[Random.Range(sprites.Count - 1, 0)]);
        alphaIncreasing = true;
        
    }

    public override void Update () {    //  *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   Update
        if (IsInUse()) {    //  .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   Disable object based on position and camera position
            if ((this.transform.position.y < (Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, 0.0f)).y - 15.0f)) || (this.transform.position.y > (Camera.main.ScreenToWorldPoint(new Vector3(Screen.height, 0.0f, 0.0f)).y + 70.0f))) {
                SetColour(new Color(1.0f, 1.0f, 1.0f, 0.0f));
                Disable();
            }
            if ((this.transform.position.x > (Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0.0f)).x + 1.0f)) || (this.transform.position.x < (-10.0f))) {
                SetColour(new Color(1.0f, 1.0f, 1.0f, 0.0f));
                Disable();
            }
            if (IsInUse()) {

                if (alphaIncreasing) {  //  .   .   .   .   .   .   .   .   .   .   .   .   .   Fade in/out based on time
                    if (alphaDriftTimer < alphaDriftTime) {
                        alphaDriftTimer += Time.deltaTime;
                    } else {
                        alphaIncreasing = false;
                    }
                } else {
                    if (alphaDriftTimer > 0) {
                        alphaDriftTimer -= Time.deltaTime;
                    } else {
                        if (blinkTimer >= blinkLimit) {
                            SetColour(new Color(1.0f, 1.0f, 1.0f, 0.0f));
                            Disable();
                        }
                        alphaIncreasing = true;
                        blinkTimer++;
                    }
                }
                SetColour(new Color(1.0f, 1.0f, 1.0f, alphaDriftTimer / alphaDriftTime));
                base.Update();
            }
        }
	}
}
