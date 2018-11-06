using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VisibilityFade : MonoBehaviour {

    [Header("Settings")]
    public float fadeTime = 0.5f;
    public bool playSound = false;
    public string sound = "";
    private float fadeTimer = 0.0f;
    private bool fadeOut = false;
    private bool fadeIn = false;
    private bool soundPlaying = false;
    private SpriteRenderer spriteRenderer;
    private SoundManager soundMan;

	// Use this for initialization
	void Start () {
        if (playSound) {
            soundMan = GameObject.Find("Sound Manager").GetComponent<SoundManager>();
        }
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }
	
    // Fade in/out object
	void Update () {
        if (fadeOut) {
            if (fadeTimer <= fadeTime) {
                spriteRenderer.color = new Vector4(spriteRenderer.color.r, spriteRenderer.color.b, spriteRenderer.color.g, fadeTimer / fadeTime);
                fadeTimer += Time.deltaTime;
            } else {
                fadeOut = false;
                fadeTimer = 0.0f;
            }
        } else if (fadeIn) {
            if (fadeTimer >= 0) {
                spriteRenderer.color = new Vector4(spriteRenderer.color.r, spriteRenderer.color.b, spriteRenderer.color.g, fadeTimer / fadeTime);
                fadeTimer -= Time.deltaTime;
            } else {
                fadeIn = false;
                fadeTimer = fadeTime;
            }
        }
	}

    // Start fade timer
    public void StartFade(bool fOut) {
        if (fOut) {
            if (playSound) {
                if (soundPlaying) {
                    soundMan.PlaySound(sound, true, false, this.transform.position, 0.35f);
                    soundPlaying = false;
                }
            }
            fadeTimer = 0.0f;
            fadeOut = true;
            if (fadeIn) {
                fadeIn = false;
            }
        } else {
            if (playSound) {
                if (!soundPlaying) {
                    soundMan.PlaySound(sound, true, true, this.transform.position, 0.35f);
                    soundPlaying = true;
                }
            }
            fadeTimer = fadeTime;
            fadeIn = true;
            if (fadeOut) {
                fadeOut = false;
            }
        }
    }
}
