using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BubbleFloat : MonoBehaviour {

    [Header("Possible Sprites")]
    public List<Sprite> sprites = new List<Sprite>();

    [Header("Bubble Settings")]
    public float maxSize = 1.8f;
    public float minSize = 0.6f;
    public float xDirection = 0.0f;
    public float yDirection = 0.0f;
    //private float currXPos = 0.0f;
    //private float currYPos = 0.0f;
    public float velLimit = 0.4f;
    public float yDelOffset = -15.0f;
    public float ySpawnOffset = 30.0f;
    public float xFalloff = 0.8f;
    public float scale = 1.0f;
    private float initGravScale = 0.0f;
    public Vector3 currScale;

    [Header("Timers")]
    public float xClampTime = 10.0f;
    //private float xClampTimer = 0.0f;
    public float scaleTime = 1.2f;
    public float scaleTimer = 0;
    public Vector3 initScale;

    [Header("Unity Objects")]
    private SpriteRenderer sprtRndr;
    private Rigidbody2D rgdBdy;
    private SoundManager soundMan;

    [Header("Misc")]
    public bool inUse = false;
    //private bool alphaIncreasing = true;
    private bool started = false;

    public void Init(Vector2 direction, float velLimits, float xSlow, float ScaleTime, float scle, float ydelo, float yspawno) { //  *   *   *   *   Post initialising (vec2 direction/drift/velocity limit, float alpha timer
        soundMan = GameObject.Find("Sound Manager").GetComponent<SoundManager>();
        NewStart();
        yDelOffset = ydelo;
        ySpawnOffset = yspawno;
        direction *= 10;
        xDirection = direction.x;
        yDirection = direction.y;
        velLimit = velLimits;
        xFalloff = xSlow;
        inUse = true;
        scaleTime = ScaleTime;
        rgdBdy.velocity = (new Vector2(0.0f, 0.0f));
        rgdBdy.AddForce(new Vector2(direction.x, direction.y));
        scale = scle;
        currScale *= scale;
        initGravScale = rgdBdy.gravityScale;
    }

    public void Disable() {
        inUse = false;
    }

    public bool IsInUse() {
        return inUse;
    }

    void NewStart() { //  *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   Pre-initialising
        if (!started) {
            this.transform.SetParent(GameObject.Find("Manyobubbles").transform);
            sprtRndr = this.GetComponent<SpriteRenderer>();
            rgdBdy = this.GetComponent<Rigidbody2D>();
            sprtRndr.sprite = sprites[Random.Range(sprites.Count - 1, 0)];
            initScale = this.gameObject.transform.localScale;
            initScale *= Random.Range(minSize, maxSize);
            this.gameObject.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
            started = true;
        }
        xDirection /= 100;
        yDirection /= 100;
        currScale = initScale;
        scaleTimer = 0;
        soundMan.PlaySound("bubble", false, false, this.transform.position, 1.0f);
        inUse = true;
    }

    void Update() {    //   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   Update
        if (inUse) {    //  .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   Disable object based on position and camera position
            if ((this.transform.position.y < (Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, 0.0f)).y + yDelOffset)) || (this.transform.position.y > (Camera.main.ScreenToWorldPoint(new Vector3(Screen.height, 0.0f, 0.0f)).y + ySpawnOffset))) {
                Disable();
            } else if ((this.transform.position.x > (Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0.0f)).x + sprtRndr.bounds.size.x)) || (this.transform.position.x < (Camera.main.ScreenToWorldPoint(new Vector3(-Screen.width, 0.0f, 0.0f)).x - sprtRndr.bounds.size.x))) {
                Disable();
            } else {    //  .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   Scale object based on time
                if (scaleTimer < scaleTime) {
                    this.gameObject.transform.localScale = (currScale * (scaleTimer / scaleTime));
                    scaleTimer += Time.deltaTime;
                } else if ((currScale.x > this.gameObject.transform.localScale.x) || (currScale.x < this.gameObject.transform.localScale.x)) {
                    this.gameObject.transform.localScale = currScale;
                }
                if (rgdBdy.velocity.y >= velLimit) {
                    rgdBdy.gravityScale = 0.0f;
                } else {
                    rgdBdy.gravityScale = initGravScale;
                }
            }
        }
    }
}
