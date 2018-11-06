using UnityEngine;
using System.Collections;

public class ParticleScript : MonoBehaviour {

    public GameObject target;
    private ParticleSystem targetpart;
    public bool looping = false;
    public bool animTrigger = false;
    public bool inAnimationTrigger = false;
    private bool triggered = false;
    public bool playSound = false;
    public string sound = "";
    private SoundManager soundMan;

    void Start() {
        targetpart = target.GetComponent<ParticleSystem>();
        if (playSound) {
            soundMan = GameObject.Find("Sound Manager").GetComponent<SoundManager>();
        }
    }

    void Update() {
        if ((looping == false) && (triggered == false)) {
            if (inAnimationTrigger) {
                targetpart.Play();
                inAnimationTrigger = false;
                triggered = true;
            }
        } else if (looping) {
            if (inAnimationTrigger) {
                targetpart.Play();
                inAnimationTrigger = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D coll) {    //  *   *   *   *   *   *   *   *   *   *   *   Players particle system
        if (!animTrigger) {
            switch (coll.gameObject.tag) {
                case "Player":
                    if ((looping == false) && (triggered == false)) {
                        targetpart.Play();
                        inAnimationTrigger = false;
                        triggered = true;
                        if (playSound) {
                            soundMan.PlaySound(sound, false, false, this.transform.position, 0.4f);
                        }
                    } else if (looping) {
                        targetpart.Play();
                        inAnimationTrigger = false;
                    }
                    break;
            }
        }
    }
}
