using UnityEngine;
using System.Collections;

public class PufferFish : MonoBehaviour {

    public bool puff = false;
    private bool hitPlayer = false;
    private Animator animator;
    private GameObject player;
    private GameObject persistData;
    private PersistentData perDataScript;
    private SoundManager soundMan;
    
    void Start() {
        soundMan = GameObject.Find("Sound Manager").GetComponent<SoundManager>();
        persistData = GameObject.Find("Persistent Data");
        perDataScript = persistData.GetComponent<PersistentData>();
        animator = this.gameObject.GetComponent<Animator>();
    }

    public void Puff() {    // Start pufferfish puffing
        animator.SetTrigger("Puff");
        soundMan.PlaySound("puff", false, false, new Vector2(this.transform.position.x, this.transform.position.y), 0.5f);
        if (!puff) {
            perDataScript.AddNearPuffs(1);
            puff = true;
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Player") {
            col.gameObject.GetComponent<Player>().getHit(this.gameObject);
            if (!hitPlayer) {
                hitPlayer = true;
                perDataScript.AddNearPuffs(-1);
            }
        }
    }
}
