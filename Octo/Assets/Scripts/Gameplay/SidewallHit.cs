using UnityEngine;
using System.Collections;

public class SidewallHit : MonoBehaviour {
    SoundManager soundManScript;
    
    void Start () {
        soundManScript = GameObject.Find("Sound Manager").GetComponent<SoundManager>();
    }
	
    void OnCollisionEnter2D(Collision2D coll) { //  *   *   *   *   *   *   *   *   *   *   Collider hit
        switch (coll.transform.tag) {
            case "Player":
                if (soundManScript != null) {
                    soundManScript.PlaySound("wallbump", false, false, Vector3.zero, 1.0f);
                }
                break;
        }
    }
}
