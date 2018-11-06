using UnityEngine;
using System.Collections;

public class AreaTrigger : MonoBehaviour {

    public GameObject timerBar;
    private GameObject timerObj;
    public GameObject overlay;
    public float TriggerTime = 4.0f;
    private float TriggerTimer = 0.0f;
    public float AlphaTime = 0.8f;
    private float AlphaTimer = 0.0f;
    private bool playerEntered = false;
    private GameObject player;
    private Vector3 initScale;
    private SpriteRenderer overlaySpr;
    private Vector4 initColour;
    private bool FadeIn = false;
    private bool FadeOut = false;
    private SoundManager soundMan;

    void Start() {
        soundMan = GameObject.Find("Sound Manager").GetComponent<SoundManager>();
        timerObj = timerBar.transform.parent.gameObject;
        initScale = timerBar.transform.localScale;
        overlaySpr = overlay.GetComponent<SpriteRenderer>();
        initColour = overlaySpr.color;
        overlaySpr.color = new Color(initColour.x, initColour.y, initColour.z, 0.0f);
        //timerBar.transform.localScale = new Vector3(0.0f, initScale.y, initScale.z);
        timerBar.SetActive(false);
        overlay.SetActive(false);
        timerObj.SetActive(false);
    }

    void Update() {
        if(TriggerTimer > 0.0f) {
            TriggerTimer += Time.deltaTime;
            timerBar.transform.localScale = new Vector3(initScale.x - ((TriggerTimer / TriggerTime) * initScale.x), initScale.y, initScale.z);
            if (TriggerTimer >= TriggerTime) {
                if (playerEntered) {
                    timerBar.SetActive(false);
                    timerObj.SetActive(false);
                    player.GetComponent<Player>().decLives(100);
                }
                TriggerTimer = 0.0f;
            }
        } else if (playerEntered) {
            TriggerTimer += Time.deltaTime;
        }
        if (FadeIn) {
            AlphaTimer += Time.deltaTime;
            overlaySpr.color = new Color(initColour.x, initColour.y, initColour.z, initColour.w * (AlphaTimer/AlphaTime));
            if (AlphaTimer >= AlphaTime) {
                FadeIn = false;
            }
        } else if (FadeOut) {
            AlphaTimer -= Time.deltaTime;
            overlaySpr.color = new Color(initColour.x, initColour.y, initColour.z, initColour.w * (AlphaTimer / AlphaTime));
            if (AlphaTimer <= 0) {
                FadeOut = false;
                overlay.SetActive(false);
            }
        }
    }

    void StartFadeIn() {
        overlay.SetActive(true);
        FadeIn = true;
        AlphaTimer = 0.0f;
        if (FadeOut) {
            FadeOut = false;
        }
    }

    void StartFadeOut() {
        FadeOut = true;
        AlphaTimer = AlphaTime;
        if (FadeIn) {
            FadeIn = false;
        }
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.transform.tag == "Player") {
            soundMan.PlaySound("volcano", true, true, Vector2.zero, 1.0f);
            player = coll.gameObject;
            playerEntered = true;
            timerBar.SetActive(true);
            timerObj.SetActive(true);
            StartFadeIn();
        }
    }

    void OnTriggerExit2D(Collider2D coll) {
        if (coll.transform.tag == "Player") {
            soundMan.PlaySound("volcano", true, false, Vector2.zero, 1.0f);
            playerEntered = false;
            timerBar.SetActive(false);
            timerObj.SetActive(false);
            StartFadeOut();
        }
    }
}
