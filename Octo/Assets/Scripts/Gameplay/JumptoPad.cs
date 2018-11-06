using UnityEngine;
using System.Collections;

public class JumptoPad : MonoBehaviour {

    private GameObject player;
    private Animator parentAnim;
    private Collider2D colliderA;
    private bool bounced = false;
    
	void Start () {
        player = GameObject.Find("Player");
        colliderA = this.GetComponent<Collider2D>();
        colliderA.isTrigger = false;
        parentAnim = this.gameObject.GetComponentInParent<Animator>();
        if (parentAnim != null) {
            parentAnim.enabled = false;
        }
    }
	
	void Update () {    //  *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   Update
        if (!bounced) { //  .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   If player hasn't bounced, if player is above, disable trigger properties
            if (player.transform.position.y - (player.GetComponent<Collider2D>().bounds.size.y / 2) > (this.transform.position.y + colliderA.bounds.size.y / 2)) {
                colliderA.isTrigger = false;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D coll) {
        switch (coll.gameObject.tag) {
            case "Player":
                if (parentAnim != null) {
                    parentAnim.enabled = true;
                }
                bounced = true;
                colliderA.isTrigger = true;
                break;
            default:
                break;
        }
    }
}
